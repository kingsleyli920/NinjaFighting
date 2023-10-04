using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public GameObject attackCollider, kunaiPrefab;
    private float mySpeed;
    private float kunaiDist;
    public float jumpForce;

    Rigidbody2D myRigi;
    SpriteRenderer mySR;
    [HideInInspector]
    public Animator myAni;
    [HideInInspector]
    public bool canJump, isJumpPressed, isAttack, isHurt, canBeHurt;

    public AudioClip[] myClips;
    AudioSource myAudio;
    [HideInInspector]
    public int playerLife, playerKunai;
    CanvasScript myCanvas;

    InputAction playerMove, playerJump, playerAttack, playerThrow;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        myAni = GetComponent<Animator>();
        myRigi = GetComponent<Rigidbody2D>();
        mySR = GetComponent<SpriteRenderer>();
        myAudio = GetComponent<AudioSource>();
        myCanvas = GameObject.Find("/Canvas").GetComponent<CanvasScript>();
        playerMove = GetComponent<PlayerInput>().currentActionMap["Move"];
        playerJump = GetComponent<PlayerInput>().currentActionMap["Jump"];
        playerAttack = GetComponent<PlayerInput>().currentActionMap["Attack"];
        playerThrow = GetComponent<PlayerInput>().currentActionMap["Kunai"];
        isJumpPressed = false;
        canJump = true;
        isHurt = false;
        canBeHurt = true;
        playerLife = PlayerPrefs.GetInt("PlayerLife");
        playerKunai = PlayerPrefs.GetInt("KunaiNum");
    }

    // Start is called before the first frame update
    void Start()
    {
        mySpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerJump.triggered && canJump == true && !isHurt)
        {
            isJumpPressed = true;
            canJump = false;
        }

        if (playerAttack.triggered && !isHurt)
        {
            myAni.SetTrigger("Attack");
            isAttack = true;
            canJump = false;
        }

        if (playerThrow.triggered && !isHurt
        && !myAni.GetCurrentAnimatorStateInfo(0).IsName("Player_Throw")
        && !myAni.GetCurrentAnimatorStateInfo(0).IsName("Player_Attack"))
        {
            if (playerKunai > 0)
            {
                playerKunai--;
                PlayerPrefs.SetInt("KunaiNum", playerKunai);
                myCanvas.KunaiUpdate();
                myAni.SetTrigger("AttackThrow");
                isAttack = true;
                canJump = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // float directionHorizontal = Input.GetAxisRaw("Horizontal");
        float directionHorizontal = playerMove.ReadValue<Vector2>().x;
        if (isAttack || isHurt)
        {
            directionHorizontal = 0;
        }

        if (directionHorizontal > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (directionHorizontal < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        myAni.SetFloat("Run", Mathf.Abs(directionHorizontal));

        if (isJumpPressed)
        {
            myRigi.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            myAni.SetBool("Jump", true);
            isJumpPressed = false;
        }

        if (!isHurt)
        {
            myRigi.velocity = new Vector2(directionHorizontal * mySpeed, myRigi.velocity.y);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TriggerHurt(other);
        if (other.tag == "Item")
        {
            myAudio.PlayOneShot(myClips[3]);
        }
    }

    IEnumerator SetIsHurtFalse()
    {
        yield return new WaitForSeconds(0.7f);
        myAni.SetBool("Hurt", false);
        isHurt = false;
        yield return new WaitForSeconds(1.0f);
        canBeHurt = true;
        mySR.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, 1.0f);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        TriggerHurt(other);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.name == "BoundBottom")
        {
            playerLife = 0;
            PlayerPrefs.SetInt("PlayerLife", 0);
            myCanvas.LifeUpdate();
            PlayerDead();
        }
    }
    private void TriggerHurt(Collider2D other)
    {
        if (other.tag == "Enemy" && !isHurt && canBeHurt)
        {

            playerLife--;
            PlayerPrefs.SetInt("PlayerLife", playerLife);
            myCanvas.LifeUpdate();
            if (playerLife >= 1)
            {
                myAudio.PlayOneShot(myClips[0]);
                isHurt = true;
                canBeHurt = false;
                mySR.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, 0.5f);
                myAni.SetBool("Hurt", true);
                float tempDirection = transform.localScale.x < 0 ? 1.0f : -1.0f;
                myRigi.velocity = new Vector2(5.0f * tempDirection, 10.0f);
                StartCoroutine("SetIsHurtFalse");
            }
            else
            {
                PlayerDead();

            }

        }
    }

    public void SetIsAttackFalse()
    {
        isAttack = false;
        canJump = true;
        myAni.ResetTrigger("Attack");
        myAni.ResetTrigger("AttackThrow");
    }

    public void IsHurtSetting()
    {
        isAttack = false;
        myAni.ResetTrigger("Attack");
        myAni.ResetTrigger("AttackThrow");
        attackCollider.SetActive(false);
    }
    public void setAttackColliderOn()
    {
        attackCollider.SetActive(true);
    }

    public void setAttackColliderOff()
    {
        attackCollider.SetActive(false);
    }

    public void playerSwordEffect()
    {
        myAudio.PlayOneShot(myClips[1]);
    }

    public void playerKunaiEffect()
    {
        myAudio.PlayOneShot(myClips[2]);
    }

    public void KunaiInstantiate()
    {
        kunaiDist = transform.localScale.x;
        Vector3 temp = new Vector3(transform.position.x + kunaiDist, transform.position.y, transform.position.z);
        Instantiate(kunaiPrefab, temp, Quaternion.identity);
    }
    private void PlayerDead()
    {
        myAni.SetBool("Dead", true);
        isHurt = true;
        isAttack = true;
        myRigi.velocity = new Vector2(0f, 0f);
        myAudio.PlayOneShot(myClips[4]);
        PlayerPrefs.SetInt("PlayerLife", 5);
        StartCoroutine("BackToLevelSelect");
    }

    IEnumerator BackToLevelSelect()
    {
        yield return new WaitForSeconds(1.5f);
        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }
}

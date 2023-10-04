using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPumpkinMan : MonoBehaviour
{
    private bool isAlive, isIdle, jumpAttack, isJumpUp, slideAttack, isHurt, canBeHurt;
    GameObject player;
    Animator myAni;
    Vector3 slideTargetPosition;
    BoxCollider2D myCollider;
    SpriteRenderer mySR;
    AudioSource myAudio;

    public int life;
    public float attackDistance, jumpDistance, jumpUpSpeed, jumpDownSpeed, slideSpeed, fallDownSpeed;
    // Start is called before the first frame update
    private void Awake()
    {
        isAlive = true;
        isIdle = true;
        jumpAttack = false;
        isJumpUp = true;
        slideAttack = false;
        isHurt = false;
        canBeHurt = true;
        player = GameObject.Find("Player");
        myAni = GetComponent<Animator>();
        myCollider = GetComponent<BoxCollider2D>();
        mySR = GetComponent<SpriteRenderer>();
        myAudio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        EnemyAttack();
    }

    void EnemyAttack()
    {
        if (isAlive)
        {

            if (isIdle)
            {
                lookAtPlayer();
                if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance)
                {
                    isIdle = false;
                    StartCoroutine("IdleToSlideAttack");
                }
                else
                {
                    isIdle = false;
                    StartCoroutine("IdleToJumpAttack");
                }
            }
            else if (jumpAttack)
            {
                lookAtPlayer();
                if (isJumpUp)
                {
                    Vector3 myTarget = new Vector3(player.transform.position.x, jumpDistance, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, myTarget, jumpUpSpeed * Time.deltaTime);
                    myAni.SetBool("JumpUp", true);
                }
                else
                {
                    myAni.SetBool("JumpUp", false);
                    myAni.SetBool("JumpDown", true);
                    Vector3 myTarget = new Vector3(transform.position.x, -1.59f, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, myTarget, jumpDownSpeed * Time.deltaTime);
                }

                if (transform.position.y == jumpDistance)
                {
                    isJumpUp = false;
                }
                else if (transform.position.y == -1.59f)
                {
                    jumpAttack = false;
                    StartCoroutine("JumpDownToIdle");

                }
            }
            else if (slideAttack)
            {
                myAni.SetBool("Slide", true);
                transform.position = Vector3.MoveTowards(transform.position, slideTargetPosition, slideSpeed * Time.deltaTime);

                if (transform.position == slideTargetPosition)
                {
                    myCollider.offset = new Vector2(-0.2047148f, -0.04094303f);
                    myCollider.size = new Vector2(1.497143f, 2.642571f);
                    myAni.SetBool("Slide", false);
                    slideAttack = false;
                    isIdle = true;

                }
            }
            else if (isHurt)
            {
                Vector3 targetPosition = new Vector3(transform.position.x, -1.59f, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, fallDownSpeed * Time.deltaTime);
            }
        }
        else
        {
            Vector3 targetPosition = new Vector3(transform.position.x, -1.59f, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, fallDownSpeed * Time.deltaTime);
        }
    }
    IEnumerator JumpDownToIdle()
    {
        yield return new WaitForSeconds(.7f);
        isIdle = true;
        myAni.SetBool("JumpUp", false);
        myAni.SetBool("JumpDown", false);
        isJumpUp = true;
    }

    IEnumerator IdleToJumpAttack()
    {
        yield return new WaitForSeconds(0.7f);
        jumpAttack = true;
    }

    IEnumerator IdleToSlideAttack()
    {
        yield return new WaitForSeconds(0.7f);
        myCollider.offset = new Vector2(-0.2047148f, -0.4087726f);
        myCollider.size = new Vector2(1.497143f, 1.906912f);
        lookAtPlayer();
        slideAttack = true;
        slideTargetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
    }

    IEnumerator SetHurtToFalse()
    {
        yield return new WaitForSeconds(.7f);
        myAni.SetBool("Hurt", false);
        myAni.SetBool("JumpUp", false);
        myAni.SetBool("JumpDown", false);
        myAni.SetBool("Slide", false);
        myCollider.enabled = true;
        myCollider.offset = new Vector2(-0.2047148f, -0.04094303f);
        myCollider.size = new Vector2(1.497143f, 2.642571f);
        isHurt = false;
        isIdle = true;
        mySR.material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        yield return new WaitForSeconds(2.0f);
        canBeHurt = true;
        mySR.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

    void lookAtPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canBeHurt)
        {
            if (other.tag == "PlayerAttack")
            {
                myAudio.PlayOneShot(myAudio.clip);
                life--;
                if (life >= 1)
                {
                    isIdle = false;
                    jumpAttack = false;
                    slideAttack = false;
                    isHurt = true;
                    myCollider.enabled = false;
                    StopCoroutine("JumpDownToIdle");
                    StopCoroutine("IdleToJumpAttack");
                    StopCoroutine("IdleToSlideAttack");
                    myAni.SetBool("Hurt", true);
                    StartCoroutine("SetHurtToFalse");
                }
                else
                {
                    isAlive = false;
                    StopAllCoroutines();
                    myAni.SetBool("Dead", true);
                    myCollider.enabled = false;
                    Time.timeScale = 0.5f;
                    StartCoroutine("BackToLevelSelect");
                }
                canBeHurt = false;
            }
        }
    }

    IEnumerator BackToLevelSelect() {
        yield return new WaitForSeconds(3.0f);
        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }
}

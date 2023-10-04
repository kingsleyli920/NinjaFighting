using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaleZombie : MonoBehaviour
{
    protected GameObject myPlayer;
    public Vector3 targetPosition;
    public float mySpeed;
    public GameObject attackCollider;
    protected bool isFirtIdle, justLeavePlayer;

    protected Vector3 origionPosition, turnPoint;

    protected Animator myAni;
    public int enemyLife;
    protected BoxCollider2D mycollider;
    protected SpriteRenderer mySR;
    [SerializeField]
    protected AudioClip[] myClips;
    protected AudioSource myAudio;
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        myAni = GetComponent<Animator>();
        origionPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        isFirtIdle = true;
        myPlayer = GameObject.Find("Player");
        justLeavePlayer = false;
        mycollider = GetComponent<BoxCollider2D>();
        mySR = GetComponent<SpriteRenderer>();
        myAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        EnemyBehaviours();
    }

    protected virtual void EnemyBehaviours()
    {
        if (enemyLife < 1)
        {
            return;
        }

        if (Vector3.Distance(myPlayer.transform.position, transform.position) <= 1.3f)
        {
            if (myPlayer.transform.position.x <= transform.position.x)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            if (!myAni.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack") && !myAni.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Attack_Rest"))
            {
                myAni.SetTrigger("Attack");
                myAudio.PlayOneShot(myClips[1]);
            }
            justLeavePlayer = true;
            return;
        }
        else
        {
            if (justLeavePlayer)
            {

                if (turnPoint == targetPosition)
                {
                    float turnParam = targetPosition.x > origionPosition.x ? 1.0f : -1.0f;
                    StartCoroutine(TurnBody(turnParam));
                }
                else
                {
                    float turnParam = targetPosition.x > origionPosition.x ? -1.0f : 1.0f;
                    StartCoroutine(TurnBody(turnParam));
                }
                justLeavePlayer = false;
            }
        }

        if (transform.position.x == targetPosition.x)
        {
            turnPoint = origionPosition;
            myAni.SetTrigger("Idle");
            isFirtIdle = false;
            float turnParam = targetPosition.x > origionPosition.x ? -1.0f : 1.0f;
            StartCoroutine(TurnBody(turnParam));
        }
        else if (transform.position.x == origionPosition.x)
        {
            turnPoint = targetPosition;
            if (!isFirtIdle)
            {
                myAni.SetTrigger("Idle");
            }
            float turnParam = targetPosition.x > origionPosition.x ? 1.0f : -1.0f;
            StartCoroutine(TurnBody(turnParam));

        }
        if (myAni.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Walk"))
        {
            transform.position = Vector3.MoveTowards(transform.position, turnPoint, mySpeed * Time.deltaTime);
        }

    }

    protected IEnumerator TurnBody(float turnDirection)
    {
        yield return new WaitForSeconds(2.0f);
        transform.localScale = new Vector3(turnDirection, 1.0f, 1.0f);
    }

    public void setAttackColliderOn()
    {
        attackCollider.SetActive(true);
    }
    public void setAttackColliderOff()
    {
        attackCollider.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerAttack")
        {
            enemyLife--;
            myAudio.PlayOneShot(myClips[0]);
            if (enemyLife >= 1)
            {
                myAni.SetTrigger("Hurt");
            }
            else
            {
                myAni.SetTrigger("Dead");
                mycollider.enabled = false;
                StartCoroutine("AfterDead");
            }
        }
    }
    IEnumerator AfterDead()
    {
        yield return new WaitForSeconds(1.0f);
        mySR.material.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, 0.5f);
        yield return new WaitForSeconds(1.0f);
        mySR.material.color = new Color(mySR.color.r, mySR.color.g, mySR.color.b, 0.2f);
        yield return new WaitForSeconds(1.0f);
        Destroy(this.gameObject);
    }
}

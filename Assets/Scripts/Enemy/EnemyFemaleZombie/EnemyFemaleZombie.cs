using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyFemaleZombie : EnemyMaleZombie
{
    public float runSpeedRate;
    bool isBattleMode;

    protected override void Awake()
    {
        base.Awake();
        isBattleMode = true;
    }
    protected override void EnemyBehaviours()
    {
        if (enemyLife < 1)
        {
            return;
        }

        if (!isBattleMode)
        {
            if (transform.position.x > turnPoint.x)
            {
                transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }

            if (myAni.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Walk"))
            {
                transform.position = Vector3.MoveTowards(transform.position, turnPoint, mySpeed * Time.deltaTime);
            }

            if (transform.position == turnPoint)
            {
                isBattleMode = true;
            }
            return;
        }
        else
        {
            if (Vector3.Distance(myPlayer.transform.position, transform.position) <= 5.0f && Mathf.Abs(myPlayer.transform.position.y - transform.position.y) < 0.5f)
            {
                if (myPlayer.transform.position.x <= transform.position.x)
                {
                    transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
                }
                else
                {
                    transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                }

                if (myAni.GetCurrentAnimatorStateInfo(0).IsName("Enemy_Walk"))
                {
                    Vector3 newTarget = new Vector3(myPlayer.transform.position.x, transform.position.y, transform.position.z);
                    transform.position = Vector3.MoveTowards(transform.position, newTarget, mySpeed * runSpeedRate * Time.deltaTime);
                }
                justLeavePlayer = true;
                return;
            }
            else
            {
                if (justLeavePlayer)
                {
                    float turnX = transform.position.x > turnPoint.x ? -1.0f : 1.0f;
                    transform.localScale = new Vector3(turnX, transform.localScale.y, transform.localScale.z);
                    justLeavePlayer = false;
                }
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
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.tag == "StopPoint")
        {
            isBattleMode = false;
        }
        if (other.tag == "PlayerAttack")
        {
            isBattleMode = true;
        }
    }
}

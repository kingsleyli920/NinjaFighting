using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Button_Collider : MonoBehaviour
{
    Player playerScript;
    // Start is called before the first frame update
    void Awake()
    {
        playerScript = GetComponentInParent<Player>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Floor")
        {
            setCanJump();
        }

        if (other.tag == "FloatingPlatform")
        {
            setCanJump();
            playerScript.transform.parent = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "FloatingPlatform")
        {
            playerScript.transform.parent = null;
        }
    }

    private void setCanJump()
    {
        playerScript.canJump = true;
        playerScript.myAni.SetBool("Jump", false);
    }
}

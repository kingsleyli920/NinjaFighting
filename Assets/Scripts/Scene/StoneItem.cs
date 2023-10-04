using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneItem : MonoBehaviour
{
    CanvasScript canvasScript;
    private void Awake()
    {
        canvasScript = GameObject.Find("Canvas").GetComponent<CanvasScript>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            int tempStone = PlayerPrefs.GetInt("StoneNum");
            PlayerPrefs.SetInt("StoneNum", tempStone + 1);
            canvasScript.StoneUpdate();
            Destroy(this.gameObject);
        }
    }
}

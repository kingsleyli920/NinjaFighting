using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeItem : MonoBehaviour
{

Player myPlayer;
CanvasScript canvasScript;
private void Awake() {
    myPlayer = GameObject.Find("Player").GetComponent<Player>();
    canvasScript = GameObject.Find("Canvas").GetComponent<CanvasScript>();
}
    private void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Player") {
            int tempLife = PlayerPrefs.GetInt("PlayerLife");
            PlayerPrefs.SetInt("PlayerLife", tempLife + 1);
            myPlayer.playerLife = tempLife + 1;
            canvasScript.LifeUpdate();
            Destroy(this.gameObject);
        }
    }
}

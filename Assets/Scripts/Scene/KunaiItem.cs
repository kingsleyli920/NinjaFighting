using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KunaiItem : MonoBehaviour
{
    Player myPlayer;
    CanvasScript canvasScript;
    private void Awake() {
        myPlayer = GameObject.Find("Player").GetComponent<Player>();
        canvasScript = GameObject.Find("Canvas").GetComponent<CanvasScript>();
    }
  private void OnTriggerEnter2D(Collider2D other) {
    if (other.name == "Player") {
        int tempKunai = PlayerPrefs.GetInt("KunaiNum");
        PlayerPrefs.SetInt("KunaiNum", tempKunai + 1);
        myPlayer.playerKunai = tempKunai + 1;
        canvasScript.KunaiUpdate();
        Destroy(this.gameObject);
    }
  }
}

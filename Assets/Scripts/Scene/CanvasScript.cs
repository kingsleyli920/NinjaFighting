using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CanvasScript : MonoBehaviour
{
    public TextMeshProUGUI lifeText, KunaiText, StoneText;

    private void Awake()
    {

        LifeUpdate();
        KunaiUpdate();
        StoneUpdate();

    }

    public void LifeUpdate()
    {
        lifeText.text = " X " + PlayerPrefs.GetInt("PlayerLife").ToString();
    }

    public void KunaiUpdate()
    {
        KunaiText.text = " X " + PlayerPrefs.GetInt("KunaiNum").ToString();
    }

    public void StoneUpdate()
    {
        StoneText.text = " X " + PlayerPrefs.GetInt("StoneNum").ToString();
    }
}

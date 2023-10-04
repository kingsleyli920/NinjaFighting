using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsFirstTimePlayCheck : MonoBehaviour
{
    private void Awake()
    {
        FirstTimePlayState();
    }

    public void FirstTimePlayState()
    {
        if (!PlayerPrefs.HasKey("IsFirstTimePlay"))
        {
            PlayerPrefs.SetInt("IsFirstTimePlay", 1);
            PlayerPrefs.SetInt("PlayerLife", 5);
            PlayerPrefs.SetInt("KunaiNum", 2);
            PlayerPrefs.SetInt("StoneNum", 0);
            PlayerPrefs.SetInt("ClearedLevel", 0);
        }
    }
}

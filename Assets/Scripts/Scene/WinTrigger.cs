using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            string levelName = SceneManager.GetActiveScene().name;
            String levelNum = levelName[6..];
            int levelInt = int.Parse(levelNum);
            int savedLevel = PlayerPrefs.GetInt("clearedLevel", 0);
            if (levelInt > savedLevel)
            {
                PlayerPrefs.SetInt("clearedLevel", levelInt);
            }
            Time.timeScale = 0.0f;
            FadeInOut.instance.SceneFadeInOut("LevelSelect");
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMController : MonoBehaviour
{
    public AudioClip[] myBGMClips, myButtonClips;
    [HideInInspector]
    public AudioSource myAudioSource;
    private void Awake()
    {
        myAudioSource = GetComponent<AudioSource>();
        String sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "MainMenu":
                myAudioSource.clip = myBGMClips[0];
                break;
            case "LevelSelect":
                myAudioSource.clip = myBGMClips[1];
                break;
            case "Level 1":
            case "Level 2":
                myAudioSource.clip = myBGMClips[2];
                break;
            case "Level 3":
                myAudioSource.clip = myBGMClips[3];
                break;
            default:
                myAudioSource.clip = myBGMClips[0];
                break;
        }
        myAudioSource.loop = true;
        myAudioSource.Play();
        myAudioSource.volume = 0.5f;
    }
}

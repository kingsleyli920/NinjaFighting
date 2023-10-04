using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelButtonScript : MonoBehaviour
{
    public GameObject optionsPanel;
    public GameObject pauseButton, levelSelectButton, mainMenuButton, replayButton;
    public void SetOptiosPanelOn()
    {
        optionsPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void SetOptiosPanelOff()
    {
        optionsPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void SetPauseButtonOn()
    {
        pauseButton.SetActive(true);
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
    }

    public void SetPauseButtonOff()
    {
        pauseButton.SetActive(false);
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
    }

    public void mainMenuPlayButton()
    {
        GameObject mainMenuPlayer = GameObject.Find("MainMenuPlayer");
        Animator mainMenuPlayerAni = mainMenuPlayer.GetComponent<Animator>();
        mainMenuPlayerAni.SetBool("Run", true);
        GameObject playButton = GameObject.Find("Canvas/SafeAreaPanel/PlayButton");
        playButton.SetActive(false);
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
        FadeInOut.instance.SceneFadeInOut("LevelSelect");
    }

    public void DataDeleteButton()
    {
        RectTransform dataDeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteImage.anchoredPosition = new Vector2(0f, -100f);
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
    }

    public void YesButton()
    {
        PlayerPrefs.DeleteAll();
        IsFirstTimePlayCheck checkScript = GameObject.Find("IsFirstTimePlayCheck").GetComponent<IsFirstTimePlayCheck>();
        checkScript.FirstTimePlayState();
        RectTransform dataDeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteImage.anchoredPosition = new Vector2(0f, 1600f);
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
    }

    public void NoButton()
    {
        RectTransform dataDeleteImage = GameObject.Find("Canvas/SafeAreaPanel/DataDeleteImage").GetComponent<RectTransform>();
        dataDeleteImage.anchoredPosition = new Vector2(0f, 1600f);
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
    }
    public void LevelSelectButton()
    {
        FadeInOut.instance.SceneFadeInOut("LevelSelect");
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
    }
    public void MainMenuButton()
    {
        FadeInOut.instance.SceneFadeInOut("MainMenu");
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
    }
    public void ReplayButton()
    {
        String currentScene = SceneManager.GetActiveScene().name;
        FadeInOut.instance.SceneFadeInOut(currentScene);
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
    }
}

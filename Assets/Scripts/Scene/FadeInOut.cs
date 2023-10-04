using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut instance;
    public GameObject fadeInOutImage;
    public Animator fadeInOutAni;
    private void Awake() {
        if (instance !=  null) {
            Destroy(this.gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SceneFadeInOut(String levelName) {
        StartCoroutine(SceneFadeInOutAsync(levelName));
    }
    IEnumerator SceneFadeInOutAsync(String levelName) {
        fadeInOutImage.SetActive(true);
        yield return new WaitForSecondsRealtime(1.5f);
        SceneManager.LoadScene(levelName);
        fadeInOutAni.Play("FadeOut");
        yield return new WaitForSecondsRealtime(1.0f);
        fadeInOutImage.SetActive(false);
        Time.timeScale = 1.0f;
    }
}

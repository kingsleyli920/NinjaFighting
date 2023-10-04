using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevelButtonScript : MonoBehaviour
{
    public Sprite buttonSprite;
    Image[] levelBtnImgs;
    int clearedLevel;
    private void Awake()
    {

        AddLevels();
        clearedLevel = PlayerPrefs.GetInt("clearedLevel", 0);
        for (int i = 0; i <= clearedLevel; i++)
        {
            levelBtnImgs[i].sprite = buttonSprite;
        }
    }

    private void AddLevels()
    {
        Image btnLv1 = GameObject.Find("Canvas/SafeAreaPanel/SelectLevelBgImg/LevelOneButton").GetComponent<Image>();
        Image btnLv2 = GameObject.Find("Canvas/SafeAreaPanel/SelectLevelBgImg/LevelTwoButton").GetComponent<Image>();
        Image btnLv3 = GameObject.Find("Canvas/SafeAreaPanel/SelectLevelBgImg/LevelThreeButton").GetComponent<Image>();

        levelBtnImgs = new Image[] { btnLv1, btnLv2, btnLv3 };
    }

    public void GoToLevelOne()
    {
        FadeInOut.instance.SceneFadeInOut("Level 1");
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
    }

    public void GoToLevelTwo()
    {
        if (clearedLevel >= 1)
        {
            FadeInOut.instance.SceneFadeInOut("Level 2");
            BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
            myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
        }
        else
        {
            BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
            myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[1]);
        }

    }

    public void GoToLevelThree()
    {
        if (clearedLevel >= 2)
        {
            FadeInOut.instance.SceneFadeInOut("Level 3");
            BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
            myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
        }
        else
        {
            BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
            myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[1]);
        }
    }

    public void GoToMainMenu()
    {
        FadeInOut.instance.SceneFadeInOut("MainMenu");
        BGMController myBgm = GameObject.Find("BGMController").GetComponent<BGMController>();
        myBgm.myAudioSource.PlayOneShot(myBgm.myButtonClips[0]);
    }
}

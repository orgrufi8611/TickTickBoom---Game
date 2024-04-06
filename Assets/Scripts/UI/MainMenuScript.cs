using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] Slider bgm;
    [SerializeField] Slider sfx;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeBGM(bgm.value);
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeSFX(sfx.value);
        settingsMenu.SetActive(false);
    }

    public void StartButton()
    {
        GameObject.Find("SceneController").GetComponent<SceneController>().SceneLoad(SceneController.sceneNames[1]);
    }

    public void SettingButton()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void SetBGMVolume()
    {
        float value = bgm.value;
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeBGM(value);
    }

    public void SetSFXVolume()
    {
        float value = sfx.value;
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeSFX(value);
    }

    public void Exit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

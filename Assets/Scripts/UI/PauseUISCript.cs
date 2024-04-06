using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseUISCript : MonoBehaviour
{
    [SerializeField] GameLogic gameLogic;
    [SerializeField] Slider sfx;
    [SerializeField] Slider bgm;

    private void Start()
    {
        sfx.value = VolumeSettings.SFXVolume;
        bgm.value = VolumeSettings.BGMVolume;
    }
    private void Update()
    {
        
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        GameObject.Find("SceneController").GetComponent<SceneController>().ImidiateLoad(SceneController.sceneNames[0]);
    }

    public void ResumeButton() 
    {
        Time.timeScale = 1;
        Debug.Log("Resume");
        gameObject.SetActive(false);
        gameLogic.Resume();
    }

    public void SetBGMVolume()
    {
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeBGM(bgm.value);
    }

    public void SetSFXVolume()
    {
        GameObject.Find("SoundController").GetComponent<VolumeSettings>().ChangeVolumeSFX(sfx.value);
    }

}

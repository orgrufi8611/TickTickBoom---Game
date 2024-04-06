using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    public static float SFXVolume { get; private set; }
    public static float BGMVolume { get; private set; }

    private void Start()
    {
        SFXVolume = 1.0f;
        BGMVolume = 1.0f;
    }
    public void ChangeVolumeBGM(float value)
    {
        mixer.SetFloat("BGM", Mathf.Log10(value)* 20);
        BGMVolume = value;
    }

    public void ChangeVolumeSFX(float value)
    {
        mixer.SetFloat("SFX", Mathf.Log10(value)* 20);
        SFXVolume = value;
    }
}

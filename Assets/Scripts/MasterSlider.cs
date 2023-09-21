using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MasterSlider : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetVolume(float sliderValue)
    {
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(sliderValue) * 20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FxSlider : MonoBehaviour
{
    public AudioMixer masterMixer;

    public void SetVolume(float sliderValue)
    {
        masterMixer.SetFloat("FxVolume", Mathf.Log10(sliderValue) * 20);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioMixer audiomix;
    public AudioSource clickSound;
    public Slider bgmSlider;
    public Slider effectSlider;

    void Awake()
    {
        var obj = FindObjectsOfType<SoundManager>();
        if(obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void SetBGMVolume()
    {
        audiomix.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
    }

    public void SetEffectVolume()
    {
        audiomix.SetFloat("Effect", Mathf.Log10(effectSlider.value) * 20);
    }

    public void ClostSoundBox(GameObject soundbox)
    {
        soundbox.SetActive(false);
    }

    public void ClickSound()
    {
        clickSound.Play();
    }
}

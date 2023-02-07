using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingMusic : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMusic;



    private void Update()
    {
        textMusic.text = AudioManager.instance.masterVolume.ToString();
    }
    public void UpMusic(int value)
    {
        if(AudioManager.instance.masterVolume >= 10)
        {
            AudioManager.instance.masterVolume = 10;
        }
        else
        {
            AudioManager.instance.masterVolume += value;
        }
    }
    
    public void DownMusic(int value)
    {
        if (AudioManager.instance.masterVolume <= 0)
        {
            AudioManager.instance.masterVolume = 0;
        }
        else
        {
            AudioManager.instance.masterVolume -= value;
        }
    }
}

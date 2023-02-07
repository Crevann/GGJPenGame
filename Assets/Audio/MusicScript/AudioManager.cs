using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{

    [Header("Volume")]
    [Range(0, 10)]
    public int masterVolume = 10;
    [Range(0, 10)]
    public int musicVolume = 10;
    [Range(0, 10)]
    public int ambienceVolume = 10;
    [Range(0, 10)]
    public int SFXVolume = 10;



    public static AudioManager instance { get; private set; }

    private EventInstance ambienceEventInstance;
    private EventInstance musicMenuInstance;
    Bus masterBus;
    Bus musicBus;
    Bus ambienceBus;
    Bus SFXBus;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("AudioManager already exist in the scene");

        }
        instance = this;
        masterBus = RuntimeManager.GetBus("Bus:/");
        musicBus = RuntimeManager.GetBus("Bus:/MUSIC GROUP");
        ambienceBus = RuntimeManager.GetBus("Bus:/AMBIENCE GROUP");
        SFXBus = RuntimeManager.GetBus("Bus:/SFX GROUP");
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume / 10f);
        musicBus.setVolume(musicVolume / 10f);
        ambienceBus.setVolume(ambienceVolume /10f);
        SFXBus.setVolume(SFXVolume /10f);
    }

    public void SetMusicArea(GameState area)
    {
        musicMenuInstance.setParameterByName("GameState", (float)area);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
        
    }

    public void StopMusic()
    {
        masterBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
    private void Start()
    {
        InizializeAmbience(FMODEvents.instance.ambienceSound);
        InizializeMusicMenu(FMODEvents.instance.musicMenu);
    }
    private void InizializeAmbience(EventReference ambienceEvent)
    {
        ambienceEventInstance = CreateInstance(ambienceEvent);
        ambienceEventInstance.start();
    }

    private void InizializeMusicMenu(EventReference menuMusic)
    {
        musicMenuInstance = CreateInstance(menuMusic);
        musicMenuInstance.start();
    }

    public EventInstance CreateInstance(EventReference eventeReference)
    {
        EventInstance myEvent = RuntimeManager.CreateInstance(eventeReference);
        return myEvent;
    }
}

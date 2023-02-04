using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }

    private EventInstance ambienceEventInstance;
    private EventInstance musicMenuInstance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("AudioManager already exist in the scene");

        }
        instance = this;
    }


    public void SetMusicArea(GameState area)
    {
        musicMenuInstance.setParameterByName("GameState", (float)area);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
        
    }

    public void StopMusic(bool value)
    {
        RuntimeManager.PauseAllEvents(value);
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

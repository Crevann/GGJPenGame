using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using UnityEngine.InputSystem;
using System.Linq;
public class JumpTimelineOnClick : MonoBehaviour
{
    private PlayableDirector playableDirector;
    //public GameObject controlPanel;


    public int markerNum;
    void Start() {
        playableDirector = GetComponent<PlayableDirector>();
    }
    void OnMouseUp() {
        Debug.Log("CLick");
        TimelineAsset timelineAsset = playableDirector.playableAsset as TimelineAsset;
        var markers = timelineAsset.markerTrack.GetMarkers().ToArray();
        Debug.Log(markers.Length);
        
        playableDirector.Play();
        playableDirector.time = markers[markerNum].time;
    }
    private void Update() {
        if (Mouse.current.leftButton.wasPressedThisFrame) {
            OnMouseUp();
        }
    }
}

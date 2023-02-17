using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;
using Cinemachine;
using Unity.VisualScripting;

public enum Cameras {
    BookView,
    FollowPlayer,
    Cover,
    BackCover,
    FollowFight,
    CutScene
}
public class CameraMgr : Singleton<CameraMgr>
{
    [System.Serializable]
    public class KeyValuePair {
        public EndlessBook.StateEnum key;
        public CinemachineVirtualCamera vCam;
    }
    [SerializeField] List<KeyValuePair> MyList = new List<KeyValuePair>();
    [SerializeField] CinemachineVirtualCamera currentCamera;
    
    [SerializeField] private CinemachineVirtualCamera[] allCameras;
    EndlessBook.StateEnum currentState;
    public EndlessBook.StateEnum CurrentState => currentState;

    Cameras transitTo;

    CinemachineBrain cinemachineBrain;

    Dictionary<EndlessBook.StateEnum, CinemachineVirtualCamera> cameras = new Dictionary<EndlessBook.StateEnum, CinemachineVirtualCamera>();

    void Awake() {
        foreach (var kvp in MyList) {
            cameras[kvp.key] = kvp.vCam;
            kvp.vCam.Priority = 0;
        }
        currentCamera.Priority = 1;
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
    }
    public void ChangeCamera(int val) {
        currentState = (EndlessBook.StateEnum)val;
        currentCamera.Priority = 0;
        cameras[currentState].Priority = 1;
        currentCamera = cameras[currentState];
        
            
    }

    public void ChooseCamera(Cameras camera) {
        for (int i = 0; i < allCameras.Length; i++) {
            if(i == (int)camera) {
                allCameras[i].Priority = 2;
            }
            else {
                allCameras[i].Priority = 0;
            }
        }
    }

    public void TransitTo(Cameras camera) {
        cinemachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 1f);
        transitTo = camera;
        ChooseCamera(Cameras.BookView);
    }
    public void TransitNow() {
        ChooseCamera(transitTo);
        cinemachineBrain.m_DefaultBlend = new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 2);
    }
}

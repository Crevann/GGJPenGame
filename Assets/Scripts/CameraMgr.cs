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
        public CinemachineVirtualCamera val;
    }
    [SerializeField] List<KeyValuePair> MyList = new List<KeyValuePair>();
    [SerializeField] CinemachineVirtualCamera currentCamera;
    [SerializeField] float bookmarkDelay = 1;
    [SerializeField] private CinemachineVirtualCamera[] allCameras;
    float currentDelay = 0;
    int val;

    Dictionary<EndlessBook.StateEnum, CinemachineVirtualCamera> cameras = new Dictionary<EndlessBook.StateEnum, CinemachineVirtualCamera>();

    void Awake() {
        foreach (var kvp in MyList) {
            cameras[kvp.key] = kvp.val;
            kvp.val.Priority = 0;
        }
        currentCamera.Priority = 1;
    }
    public void ChangeCamera(int val) {
        this.val = val;
        if (val >= 3) BookmarksMgr.Instance.negative = true;
        else BookmarksMgr.Instance.negative = false;
        currentCamera.Priority = 0;
        cameras[(EndlessBook.StateEnum)val].Priority = 1;
        currentCamera = cameras[(EndlessBook.StateEnum)val];
        
            currentDelay = bookmarkDelay;
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
    public void Update() {
        if(currentDelay > 0) {
            currentDelay -= Time.deltaTime;
        } else if (currentDelay < 0){
            currentDelay = 0;
            if (val != 2)
                BookmarksMgr.Instance.Activate();
        }
    }

}

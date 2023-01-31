using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;
using Cinemachine;

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

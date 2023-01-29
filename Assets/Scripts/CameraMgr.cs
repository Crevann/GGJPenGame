using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;
using Cinemachine;


[System.Serializable]
public class KeyValuePair {
    public EndlessBook.StateEnum key;
    public CinemachineVirtualCamera val;
}



public class CameraMgr : Singleton<CameraMgr>
{
    [SerializeField] List<KeyValuePair> MyList = new List<KeyValuePair>();
    [SerializeField] CinemachineVirtualCamera currentCamera;

    Dictionary<EndlessBook.StateEnum, CinemachineVirtualCamera> cameras = new Dictionary<EndlessBook.StateEnum, CinemachineVirtualCamera>();

    void Awake() {
        foreach (var kvp in MyList) {
            cameras[kvp.key] = kvp.val;
            kvp.val.Priority = 0;
        }
        currentCamera.Priority = 1;
    }
    public void ChangeCamera(int val) {
        if (val >= 3) BookmarksMgr.Instance.negative = true;
        else BookmarksMgr.Instance.negative = false;
        if (val != 2) BookmarksMgr.Instance.Activate();
        currentCamera.Priority = 0;
        cameras[(EndlessBook.StateEnum)val].Priority = 1;
        currentCamera = cameras[(EndlessBook.StateEnum)val];
    }

}

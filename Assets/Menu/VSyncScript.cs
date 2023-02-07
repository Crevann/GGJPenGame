using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSyncScript : MonoBehaviour
{
    private bool vSync = false;

    public void SetVSync()
    {
        vSync = !vSync;
        QualitySettings.vSyncCount = vSync ? 1 : 0;
        Debug.Log("funziona stronzo");

    }
}

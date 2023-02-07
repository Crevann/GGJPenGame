using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class FullScreen : MonoBehaviour
{
    private bool fS = false;

    public void changeFullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
        fS = !fS;
        Debug.Log("Funziona Stronzo");
    }

}

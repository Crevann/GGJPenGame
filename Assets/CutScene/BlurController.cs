using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class BlurController : MonoBehaviour
{
    [SerializeField] Volume volume = null;
    Vignette myVignette = null;
    DepthOfField myDoF = null;
    [SerializeField][Range(0, 1)] float vignetteIntensity = 0.2f;
    [SerializeField] float startPos = 50;
    [SerializeField] float maxRadius = 1.5f;
    void Start()
    {
        if (volume.profile.TryGet(out DepthOfField DoF))
            myDoF = DoF;
        if (volume.profile.TryGet(out Vignette vignette))
            myVignette = vignette;
    }

    // Update is called once per frame
    void Update()
    {
        myDoF.gaussianStart.Override(startPos);
        myDoF.gaussianMaxRadius.Override(maxRadius);
        myVignette.intensity.Override(vignetteIntensity);
    }
}

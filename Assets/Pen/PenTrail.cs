using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenTrail : MonoBehaviour
{
    private TrailRenderer trail;
    private Vector3 lastPosition;
    private AnimationCurve penTrait;
    private void Awake() {
        trail = GetComponent<TrailRenderer>();
        lastPosition = transform.position;
        penTrait = new AnimationCurve();
    }

    private void Update() {
        float angle = Vector3.Angle(transform.position, lastPosition);
        if(transform.position != lastPosition) {
            penTrait.AddKey(0.0f, angle);
            Debug.Log(angle);
            Keyframe[] penKeyframes = penTrait.keys;
            for (int i = 0; i < penKeyframes.Length; i++) {
                Keyframe key = penKeyframes[i];
                key.time += 0.05f;
                penKeyframes[i] = key;
            }
            penTrait.keys = penKeyframes;
        }

        trail.widthCurve = penTrait;
        lastPosition = transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEffect : MonoBehaviour
{
    [SerializeField] private AnimationCurve bounceScale;
    private float currentTime;
    private float originalScale;

    // Update is called once per frame
    private void Awake() {
        originalScale = transform.localScale.y;
        currentTime = Random.Range(0f, 1f);
    }
    void Update()
    {
        currentTime = currentTime > 1 ? 0 : currentTime + Time.deltaTime;
        float scaleY = bounceScale.Evaluate(currentTime);
        transform.localScale = new Vector3(transform.localScale.x, originalScale * scaleY, transform.localScale.z);
    }
}

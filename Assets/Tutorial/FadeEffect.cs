using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FadeEffect : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 5f;
    TextMeshProUGUI textMeshProUGUI;
    bool fadingIn = false;
    float counter;
    float risingVal;
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
            //if (!isRising) {
            //    fioreAgent.enabled = true;
            //    counter = 0;
            //    isRising = true;
            //}
       
            counter += (fadingIn ? Time.deltaTime : -Time.deltaTime);
            
            counter = Mathf.Clamp(counter, 0 , fadeSpeed);
            risingVal = counter / fadeSpeed;
            risingVal = Mathf.Clamp01(risingVal);
            textMeshProUGUI.alpha = risingVal;
            //CaneFiore.transform.GetChild(0).rotation = Quaternion.Euler(Vector3.right * standUpMovement.Evaluate(risingVal) * 90);
            
        
    }
    public void FadeIn() {
        fadingIn = true;
    }
    public void FadeOut() {
        fadingIn = false;
    }
}

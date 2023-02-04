using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
public class FadeEffect : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 5f;
    TextMeshProUGUI textMeshProUGUI;
    UnityEvent finishFadeIN;
    UnityEvent finishFadeOUT;

    bool fadingIn = false;
    float counter;
    float risingVal;
    float oldRisingVal;
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        oldRisingVal = risingVal;
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
        if(oldRisingVal != 1 && risingVal == 1)
        {
            finishFadeIN.Invoke();
        }
        if(oldRisingVal != 0 && risingVal == 0)
        {
            finishFadeOUT.Invoke();
        }
            
        
    }
    public void FadeIn() {
        fadingIn = true;
    }
    public void FadeOut() {
        fadingIn = false;
    }

    
}

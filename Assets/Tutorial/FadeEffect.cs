using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
public class FadeEffect : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 5f;
    TextMeshProUGUI textMeshProUGUI;
    TextMeshPro textMeshPro;
    Image image;
    [SerializeField]UnityEvent finishFadeIN;
    [SerializeField]UnityEvent finishFadeOUT;

    [SerializeField] bool fadingIn = false;
    float counter;
    float risingVal;
    float oldRisingVal;
    void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        image = GetComponent<Image>();
        textMeshPro = GetComponent<TextMeshPro>();
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
        if(textMeshProUGUI) textMeshProUGUI.alpha = risingVal;
        if(textMeshPro) textMeshPro.alpha = risingVal;
        if(image) image.color = new Color(image.color.r, image.color.g, image.color.b, risingVal);
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

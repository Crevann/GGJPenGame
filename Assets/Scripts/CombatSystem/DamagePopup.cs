using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DamagePopup : MonoBehaviour
{
    [SerializeField] private float timeToLive;
    [SerializeField] private float speed;
    [SerializeField] private float friction;

    public TextMeshPro text;
    private void Awake() {
        text = GetComponent<TextMeshPro>();
    }
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        speed = Mathf.Lerp(speed, 0, Time.deltaTime * friction);
        timeToLive -= Time.deltaTime;
        if(timeToLive <= 0) {
            Destroy(gameObject);
        }
    }

    public void SetText(string text) {
        this.text.text = text; 
    }
}

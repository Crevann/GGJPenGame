using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndGameTrigger : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer = (LayerMask)9;
    [SerializeField] float delayTime = 1;
    [SerializeField] UnityEvent closeTheGame;
    float count;
    bool isCounting;
    PlayerMovement p;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (isCounting) {
            count += Time.deltaTime;
            if (count > delayTime) {
                isCounting = false;
                closeTheGame.Invoke();
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0) {
            p = other.GetComponent<PlayerMovement>();
            p.JumpIn(transform.position);
            GetComponent<Collider>().enabled = false;
            isCounting = true;
            count = 0;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class BookMark : MonoBehaviour
{
    [SerializeField]float speed;
    float counter;
    [SerializeField] LayerMask bookMark;
    [SerializeField] Transform target;
    [SerializeField] Transform starPos;
    Camera camera;
    [SerializeField]UnityEvent changePage;
    

    void Start()
    {
        camera = Camera.main;
        counter = 0;
    }

   
    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 1000, bookMark) && hit.collider.gameObject == gameObject)
        {
            counter += speed * Time.deltaTime;
            counter = Mathf.Clamp01(counter);
            transform.position = Vector3.right * Mathf.Lerp(transform.position.x, target.position.x, counter) + Vector3.forward * transform.position.z;
            BookMarkClicked();
        }
        else
        {
            counter -= speed * Time.deltaTime;
            counter = Mathf.Clamp01(counter);
            transform.position = Vector3.right * Mathf.Lerp(starPos.position.x, transform.position.x, counter) + Vector3.forward * transform.position.z;
        }
    }

    void BookMarkClicked()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            changePage.Invoke();
        }
    }
}

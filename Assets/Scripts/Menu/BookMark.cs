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
    [SerializeField] Transform bookMarkToMove;
    [SerializeField] Transform target;
    [SerializeField] Transform startTarget;
    [SerializeField] Transform hiddenTarget;
    Vector3 startPos;
    Vector3 colliderStartPos;
     public bool negative;
    Camera camera;
    bool wasClicked;

    bool isHiding = false;
    [SerializeField]UnityEvent changePage;
    

    void Awake()
    {
        camera = Camera.main;
        colliderStartPos = transform.position;
        startPos = bookMarkToMove.position;
        counter = 0;
        negative = false;
    }

   
    void Update()
    {
        if (isHiding) {
            ChaingePosition(bookMarkToMove.position.x, (negative ? -1 : 1) * hiddenTarget.position.x, ref counter, speed * Time.deltaTime);

            if (counter >= 0.5) {
                if (wasClicked) {
                    wasClicked = false;
                    changePage.Invoke();
                }
                isHiding = false;
                gameObject.SetActive(false);
            }
            return;
        }


        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 1000, bookMark) && hit.collider.gameObject == gameObject)
        {
            ChaingePosition(bookMarkToMove.position.x, (negative ? -1 : 1) * target.position.x, ref counter, speed * Time.deltaTime);

            BookMarkClicked();
        }
        else
        {
            ChaingePosition((negative ? -1 : 1) * startTarget.position.x, bookMarkToMove.position.x, ref counter, -speed * Time.deltaTime);
            
        }
    }
    void ChaingePosition(float start, float end, ref float counter, float deltaChange) {
        counter += deltaChange;
        counter = Mathf.Clamp01(counter);
        bookMarkToMove.position = Vector3.right * Mathf.Lerp(start, end, counter)
            + Vector3.forward * startPos.z
            + Vector3.up * startPos.y;
    }

    void BookMarkClicked()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            wasClicked = true;
            BookmarksMgr.Instance.ChangeScene();
        }
    }
    public void Hide() {
        
        isHiding = true;
        counter = 0;
    }
    private void OnEnable() {

        transform.position = new Vector3((negative ? -1 : 1) * colliderStartPos.x, colliderStartPos.y, colliderStartPos.z);
    }
}

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
    [HideInInspector] public bool negative;
    Camera camera;
    bool wasClicked;

    bool isHiding = false;
    [SerializeField]UnityEvent changePage;
    

    void Start()
    {
        camera = Camera.main;
        startPos = bookMarkToMove.position;
        counter = 0;
    }

   
    void Update()
    {
        if (isHiding) {
            ChaingePosition(bookMarkToMove, hiddenTarget, ref counter, speed * Time.deltaTime);

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
            ChaingePosition(bookMarkToMove, target, ref counter, speed * Time.deltaTime);

            BookMarkClicked();
        }
        else
        {
            ChaingePosition(startTarget, bookMarkToMove, ref counter, -speed * Time.deltaTime);
            
        }
    }
    void ChaingePosition(Transform start, Transform end, ref float counter, float deltaChange) {
        float mult = negative ? -1 : 1;
        counter += deltaChange;
        counter = Mathf.Clamp01(counter);
        bookMarkToMove.position = Vector3.right * Mathf.Lerp(start.position.x * mult, end.position.x * mult, counter)
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
        
    }
}

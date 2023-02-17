using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class BookMark : MonoBehaviour
{
    [SerializeField]float speed;
    float counter;
    [SerializeField] LayerMask bookMarkMask;
    [SerializeField] Transform bookMarkToMove;
    [SerializeField] Transform target;
    [SerializeField] Transform startTarget;
    [SerializeField] Transform hiddenTarget;
    [SerializeField] Material front;
    [SerializeField] Material back;
    MeshRenderer meshRenderer;
    Vector3 startPos;
    Vector3 colliderStartPos;
    Vector3 colliderColliderCenterPos;
     public bool negative;
    Camera camera;
    bool wasClicked;

    bool isHiding = false;
    [SerializeField]UnityEvent changePage;
    

    void Awake()
    {
        camera = Camera.main;
        colliderStartPos = transform.position;
        colliderColliderCenterPos = GetComponent<BoxCollider>().center;
        startPos = bookMarkToMove.position;
        counter = 0;
        negative = false;
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

   
    void Update()
    {
        if (isHiding) {
            ChangePosition(bookMarkToMove.position.x, (negative ? -1 : 1) * hiddenTarget.position.x, ref counter, speed * Time.deltaTime);

            if (counter >= 0.5) {
                if (wasClicked) {
                    wasClicked = false;
                    BookmarksMgr.Instance.StartCounting();
                    changePage.Invoke();
                }
                isHiding = false;
                gameObject.SetActive(false);
            }
            return;
        }

        RaycastCheck();
    }
    void RaycastCheck() {
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit, 1000, bookMarkMask) && hit.collider.gameObject == gameObject)
        {
            ChangePosition(bookMarkToMove.position.x, (negative ? -1 : 1) * target.position.x, ref counter, speed * Time.deltaTime);

            BookMarkClicked();
        }
        else
        {
            ChangePosition((negative ? -1 : 1) * startTarget.position.x, bookMarkToMove.position.x, ref counter, -speed * Time.deltaTime);
        }
    }
    void ChangePosition(float start, float end, ref float counter, float deltaChange) {
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
            AudioManager.instance.PlayOneShot(FMODEvents.instance.changePage, this.transform.position);
            BookmarksMgr.Instance.ChangeScene();
        }
    }
    public void Hide() {
        
        isHiding = true;
        counter = 0;
    }
    private void OnEnable() {

        transform.position = new Vector3((negative ? -1 : 1) * colliderStartPos.x, colliderStartPos.y, colliderStartPos.z);
        BoxCollider collider = GetComponent<BoxCollider>();
        collider.center = new Vector3((negative ? -1 : 1) * colliderColliderCenterPos.x, colliderColliderCenterPos.y, colliderColliderCenterPos.z);
        meshRenderer.material = negative ? back : front;
    }
}

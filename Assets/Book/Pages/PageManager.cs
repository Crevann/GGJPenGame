using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using echo17.EndlessBook;

public class PageManager : Singleton<PageManager>
{
    [System.Serializable]
    public class KeyValuePair {
        public int page;
        public Page mapObject;
    }
    
    [SerializeField] List<KeyValuePair> MyList = new List<KeyValuePair>();
    EndlessBook book;
    Dictionary<int, Page> dic = new Dictionary<int, Page>();

    float baseTotalPageLength = 39.2f * 0.01f; //cm
    float totalPageLength;
    float notViewablePageLength;
    float bohOffset;

    bool waitingForRisenObjToGoDown;
    bool waitingForLoweredObjToGoUp;
    public bool InTransition => waitingForLoweredObjToGoUp || waitingForRisenObjToGoDown;

    [Header("DEBUG")]
    [SerializeField] int currentPage = 0;
    public int CurrentPage => currentPage;
    [SerializeField] bool clickMe;
    [SerializeField] bool debug = false;
    [SerializeField] int pageIWantToGoToLeft;
    void Awake() {
        foreach (var kvp in MyList) {
            dic[kvp.page] = kvp.mapObject;
        }
    }

    void Start()
    {
        book = EndlessBookMgr.Instance.book;
        totalPageLength = baseTotalPageLength * book.transform.localScale.x;
        //viiblePageLength = totalPageLength * 0.8037109375f;

        notViewablePageLength = totalPageLength * 0.1962890625f;
        bohOffset = 0.008125f * book.transform.localScale.x;

        foreach (int p in dic.Keys) {
            RemoveFromPage(p, p == 20);
            dic[p].gameObject.SetActive(false);
        }

    }
    public void TurnPageForward() {
        if (currentPage % 2 == 0) OpenToPage(++currentPage);
        else {
            currentPage += 2;
            OpenToPage(currentPage);
        }
    }
    public void TurnPageBackward() {
        if (currentPage % 2 == 0) OpenToPage(--currentPage);
        else {
            currentPage -= 2;
            OpenToPage(currentPage);
        }
    }
    public void OpenToPage(int panegN) {
        CameraMgr.Instance.TransitTo(Cameras.FollowPlayer);
        pageIWantToGoToLeft = panegN % 2 == 1 ? panegN : panegN - 1;
        //EnemyMgr.Instance.DeactivateAllEnemies();
        if (dic.ContainsKey(currentPage)) dic[currentPage].DeactivateAll();
        if (dic.ContainsKey(currentPage + 1)) dic[currentPage + 1].DeactivateAll();
        RemoveFromPage(20, true);
        waitingForRisenObjToGoDown = true;
        
    }
    private void TurnPage() {
        book.TurnToPage(pageIWantToGoToLeft, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, 0.5f,
                    openTime: 1f,
                    onCompleted: OnBookTurnToPageCompleted,
                    onPageTurnStart: OnPageTurnStart,
                    onPageTurnEnd: OnPageTurnEnd
                    );
    }
    
    public virtual void ChangeBookState(int buttonIndex) {
        dic[20].gameObject.SetActive(false);
        if (buttonIndex == (int)EndlessBook.StateEnum.OpenFront) {
            dic[20].gameObject.SetActive(true);
        }
        book.SetState((EndlessBook.StateEnum)buttonIndex, animationTime: 1, onCompleted: OnBookStateChanged);

    }
    protected virtual void OnBookStateChanged(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int currentPageNumber) {
        RemoveFromPage(20, true);
        if (toState == EndlessBook.StateEnum.OpenFront) {
            AddToPage(20, true);
            return;
        }
        Debug.Log("State set to " + toState + ". Current Page Number = " + currentPageNumber);
    }
    //For state change
    protected virtual void OnBookTurnToPageCompleted(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int currentPageNumber) {
        //Turn off all pages not used
        int startingPage = Mathf.Min(currentPage, pageIWantToGoToLeft);
        int finishingPage = Mathf.Max(currentPage, pageIWantToGoToLeft);
        for (int i = startingPage; i <= finishingPage + 1; i++) {
            if (dic.ContainsKey(i) && (i != pageIWantToGoToLeft && i != pageIWantToGoToLeft + 1)) {
                dic[i].gameObject.SetActive(false);
            }
        }
        //Teleport objects
        currentPage = currentPageNumber;
        if (currentPageNumber % 2 == 1) {
            AddToPage(currentPageNumber);
            AddToPage(currentPageNumber + 1);
        } else {
            AddToPage(currentPageNumber);
            AddToPage(currentPageNumber - 1);
        }
        if (debug) Debug.Log("OnBookTurnToPageCompleted: State set to " + toState + ". Current Page Number = " + currentPageNumber);
    }

    protected virtual void OnPageTurnStart(echo17.EndlessBook.Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, echo17.EndlessBook.Page.TurnDirectionEnum turnDirection) {
        int startingPage = Mathf.Min(currentPage, pageIWantToGoToLeft);
        int finishingPage = Mathf.Max(currentPage, pageIWantToGoToLeft);
        for (int i = startingPage; i <= finishingPage + 1; i++) {
            if (dic.ContainsKey(i)) {
                dic[i].gameObject.SetActive(true);
                RemoveFromPage(i);
            }
        }
        if (debug)Debug.Log("OnPageTurnStart: front [" + pageNumberFront + "] back [" + pageNumberBack + "] fv [" + pageNumberFirstVisible + "] lv [" + pageNumberLastVisible + "] dir [" + turnDirection + "]");
    }

    protected virtual void OnPageTurnEnd(echo17.EndlessBook.Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, echo17.EndlessBook.Page.TurnDirectionEnum turnDirection) {
        //AddToPage(pageIWantToGoToLeft);
        //AddToPage(pageIWantToGoToLeft + 1);


        if (debug) Debug.Log("OnPageTurnEnd: front [" + pageNumberFront + "] back [" + pageNumberBack + "] fv [" + pageNumberFirstVisible + "] lv [" + pageNumberLastVisible + "] dir [" + turnDirection + "]");
    }
    private void AddToPage(int pageNumber, bool specialOffeset = false) {
        bool leftPage = pageNumber % 2 == 1;
        Vector3 sizeOfCameraPage;
        if (dic.ContainsKey(pageNumber)) {
            sizeOfCameraPage = new Vector3(dic[pageNumber].collider.size.x * dic[pageNumber].collider.transform.localScale.x, 
                dic[pageNumber].collider.size.y * dic[pageNumber].collider.transform.localScale.y, 
                dic[pageNumber].collider.size.z * dic[pageNumber].collider.transform.localScale.z);
            foreach (TeleportingObject item in dic[pageNumber].teleportingObjects) {
                if (!item.isActiveAndEnabled || item.onTopOfBook) continue;
                item.onTopOfBook = true;
                item.transform.rotation = Quaternion.Euler(Vector3.right * 90);
                item.transform.localScale *= totalPageLength / sizeOfCameraPage.x;
                Vector3 cameraPagePosition = item.transform.localPosition;
                Vector3 clampedPos;
                clampedPos.x = cameraPagePosition.x / (sizeOfCameraPage.x * 0.5f);
                clampedPos.y = cameraPagePosition.y / (sizeOfCameraPage.y * 0.5f);
                clampedPos.z = 0;

                Vector3 bookPagePosition;
                bookPagePosition.x = clampedPos.x * totalPageLength * 0.5f + bohOffset * (leftPage ? 0.8f : -0.8f); //boh
                bookPagePosition.z = clampedPos.y * totalPageLength * 0.5f;
                bookPagePosition.y = 3.6f * 0.01f * book.transform.localScale.y + (specialOffeset ? 0.5f : 0);

                item.transform.position = bookPagePosition + book.transform.position + (leftPage ? Vector3.left : Vector3.right) * (totalPageLength * 0.5f * (specialOffeset ? 1.205f : 1) - (leftPage ? notViewablePageLength : 0) );
                if (item.GetComponent<NavMeshAgent>()) item.GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }
    private void RemoveFromPage(int pageNumber, bool specialOffeset = false) {
        Vector3 sizeOfCameraPage;
        bool leftPage = pageNumber % 2 == 1;
        if (dic.ContainsKey(pageNumber)) {
            sizeOfCameraPage = new Vector3(dic[pageNumber].collider.size.x * dic[pageNumber].collider.transform.localScale.x,
                dic[pageNumber].collider.size.y * dic[pageNumber].collider.transform.localScale.y,
                dic[pageNumber].collider.size.z * dic[pageNumber].collider.transform.localScale.z);
            foreach (TeleportingObject item in dic[pageNumber].teleportingObjects) {
                if (!item.isActiveAndEnabled || !item.onTopOfBook) continue;
                if (item.GetComponent<NavMeshAgent>()) item.GetComponent<NavMeshAgent>().enabled = false;
                item.onTopOfBook = false;
                item.transform.rotation = Quaternion.Euler(Vector3.zero);
                item.transform.localScale /= totalPageLength / sizeOfCameraPage.x;
               
                Vector3 bookPagePosition = item.transform.position - (leftPage ? Vector3.left : Vector3.right) * (totalPageLength * 0.5f * (specialOffeset ? 1.205f : 1) - (leftPage ? notViewablePageLength : 0)) - book.transform.position;

                Vector3 clampedPos;
                clampedPos.z = -1;
                clampedPos.y = bookPagePosition.z / (totalPageLength * 0.5f);
                clampedPos.x = (bookPagePosition.x - bohOffset * (leftPage ? 0.8f : -0.8f)) / (totalPageLength * 0.5f);

                Vector3 cameraPagePosition;
                cameraPagePosition.z = clampedPos.z;
                cameraPagePosition.y = clampedPos.y * (sizeOfCameraPage.y * 0.5f);
                cameraPagePosition.x = clampedPos.x * (sizeOfCameraPage.x * 0.5f);
                item.transform.localPosition = cameraPagePosition;
            }

        }
    }
    void Update()
    {
        if (clickMe) {
            clickMe = false;
            OpenToPage(1);
        }
        if (waitingForRisenObjToGoDown && AreAllDown()) {
            TurnPage();
            waitingForRisenObjToGoDown = false;
            waitingForLoweredObjToGoUp = true;
        }
        if(waitingForLoweredObjToGoUp && AreAllUp()) {
            CameraMgr.Instance.TransitNow();
            waitingForLoweredObjToGoUp=false;
        }
    }
    bool AreAllDown() {
        if (dic.ContainsKey(currentPage)) {
            if (!dic[currentPage].AreAllDown()) 
                return false;
        }
        if (dic.ContainsKey(currentPage + 1)) {
            if (!dic[currentPage + 1].AreAllDown()) 
                return false;
        }
        return true;
    }
    public bool AreAllUp() {
        if (dic.ContainsKey(pageIWantToGoToLeft)) {
            if (!dic[pageIWantToGoToLeft].AreAllUp()) 
                return false;
        }
        if (dic.ContainsKey(pageIWantToGoToLeft + 1)) {
            if (!dic[pageIWantToGoToLeft + 1].AreAllUp()) 
                return false;
        }
        return true;
    }
}

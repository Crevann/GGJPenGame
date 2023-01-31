using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;

public class LevelMgr : Singleton<LevelMgr>
{
    [System.Serializable]
    public class KeyValuePair {
        public int page;
        public Level mapObject;
    }
    
    [SerializeField] List<KeyValuePair> MyList = new List<KeyValuePair>();
    EndlessBook book;
    Dictionary<int, Level> dic = new Dictionary<int, Level>();

    float baseTotalPageLength = 39.2f * 0.01f; //cm
    float totalPageLength;
    float pageLength;
    float notViewablePageLength;
    float bohOffset;
    [SerializeField] bool clickMe;
    [SerializeField] bool debug = false;
    void Awake() {
        foreach (var kvp in MyList) {
            dic[kvp.page] = kvp.mapObject;
        }
    }

    void Start()
    {
        book = EndlessBookMgr.Instance.book;
        totalPageLength = baseTotalPageLength * book.transform.localScale.x;
        pageLength = totalPageLength * 0.8037109375f;

        //pageLength = basePageLength * book.transform.localScale.x;
        notViewablePageLength = totalPageLength * 0.1962890625f;
        //totalPageLength = pageLength + notViewablePageLength;
        bohOffset = 0.008125f * book.transform.localScale.x;
        OpenToPage(3);
    }

    public void OpenToPage(int panegN) {
        book.TurnToPage(panegN, EndlessBook.PageTurnTimeTypeEnum.TimePerPage, 0.5f,
                    openTime: 1f,
                    onCompleted: OnBookTurnToPageCompleted,
                    onPageTurnStart: OnPageTurnStart,
                    onPageTurnEnd: OnPageTurnEnd
                    );

    }
    //For state change
    protected virtual void OnBookTurnToPageCompleted(EndlessBook.StateEnum fromState, EndlessBook.StateEnum toState, int currentPageNumber) {
        if (debug) Debug.Log("OnBookTurnToPageCompleted: State set to " + toState + ". Current Page Number = " + currentPageNumber);
    }

    protected virtual void OnPageTurnStart(Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, Page.TurnDirectionEnum turnDirection) {
        Vector3 sizeOfCameraPage = dic[pageNumberBack].collider.size;
        foreach (Mob item in dic[pageNumberBack].mobs) {
            if (!item.onTopOfBook) continue;
            item.onTopOfBook = false;
            item.transform.rotation = Quaternion.Euler(Vector3.zero);
            item.transform.localScale /= totalPageLength / sizeOfCameraPage.x;
            Vector3 bookPagePosition = item.transform.position - Vector3.left * (totalPageLength * 0.5f - notViewablePageLength);

            Vector3 clampedPos;
            clampedPos.z = -1;
            clampedPos.y = bookPagePosition.z / (totalPageLength * 0.5f);
            clampedPos.x = (bookPagePosition.x - bohOffset) / (totalPageLength * 0.5f);

            Vector3 cameraPagePosition;
            cameraPagePosition.z = clampedPos.z;
            cameraPagePosition.y = clampedPos.y * (sizeOfCameraPage.y * 0.5f);
            cameraPagePosition.x = clampedPos.x * (sizeOfCameraPage.x * 0.5f);
            item.transform.localPosition = cameraPagePosition;
        }
        sizeOfCameraPage = dic[pageNumberLastVisible].collider.size;
        foreach (Mob item in dic[pageNumberLastVisible].mobs) {
            if (!item.onTopOfBook) continue;
            item.onTopOfBook = false;
            item.transform.rotation = Quaternion.Euler(Vector3.zero);
            item.transform.localScale /= totalPageLength / sizeOfCameraPage.x;

            Vector3 bookPagePosition = item.transform.position - Vector3.right * (totalPageLength * 0.5f);

            Vector3 clampedPos;
            clampedPos.z = -1;
            clampedPos.y = bookPagePosition.z / (totalPageLength * 0.5f);
            clampedPos.x = (bookPagePosition.x + bohOffset * 0.8f) / (totalPageLength * 0.5f);

            Vector3 cameraPagePosition;
            cameraPagePosition.z = clampedPos.z;
            cameraPagePosition.y = clampedPos.y * (sizeOfCameraPage.y * 0.5f);
            cameraPagePosition.x = clampedPos.x * (sizeOfCameraPage.x * 0.5f);
            item.transform.localPosition = cameraPagePosition;
        }



        if(debug)Debug.Log("OnPageTurnStart: front [" + pageNumberFront + "] back [" + pageNumberBack + "] fv [" + pageNumberFirstVisible + "] lv [" + pageNumberLastVisible + "] dir [" + turnDirection + "]");
    }

    protected virtual void OnPageTurnEnd(Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, Page.TurnDirectionEnum turnDirection) {
        
        Vector3 sizeOfCameraPage = dic[pageNumberFirstVisible].collider.size;
        foreach (Mob item in dic[pageNumberFirstVisible].mobs) {
            if (item.onTopOfBook) continue;
            item.onTopOfBook = true;
            item.transform.rotation = Quaternion.Euler(Vector3.right * 90);
            item.transform.localScale *= totalPageLength / sizeOfCameraPage.x;
            Vector3 cameraPagePosition = item.transform.localPosition;
            Vector3 clampedPos;
            clampedPos.x = cameraPagePosition.x / (sizeOfCameraPage.x * 0.5f);
            clampedPos.y = cameraPagePosition.y / (sizeOfCameraPage.y * 0.5f);
            clampedPos.z = 0;

            Vector3 bookPagePosition;
            bookPagePosition.x = clampedPos.x * totalPageLength * 0.5f + bohOffset; //boh
            bookPagePosition.z = clampedPos.y * totalPageLength * 0.5f;
            bookPagePosition.y = 3.6f * 0.01f * book.transform.localScale.y;

            item.transform.position = bookPagePosition + Vector3.left * (totalPageLength * 0.5f - notViewablePageLength);
        }

        sizeOfCameraPage = dic[pageNumberLastVisible].collider.size;
        foreach (Mob item in dic[pageNumberLastVisible].mobs) {
            if (item.onTopOfBook) continue;
            item.onTopOfBook = true;
            item.transform.rotation = Quaternion.Euler(Vector3.right * 90);
            item.transform.localScale *= totalPageLength / sizeOfCameraPage.x;

            Vector3 cameraPagePosition = item.transform.localPosition;
            Vector3 clampedPos;
            clampedPos.x = cameraPagePosition.x / (sizeOfCameraPage.x * 0.5f);
            clampedPos.y = cameraPagePosition.y / (sizeOfCameraPage.y * 0.5f);
            clampedPos.z = 0;

            Vector3 bookPagePosition;
            bookPagePosition.x = clampedPos.x * totalPageLength * 0.5f - bohOffset * 0.8f; //boh
            bookPagePosition.z = clampedPos.y * totalPageLength * 0.5f;
            bookPagePosition.y = 3.6f * 0.01f * book.transform.localScale.y;

            item.transform.position = bookPagePosition + Vector3.right * (totalPageLength * 0.5f/* + notViewablePageLength*/);
        }


        //TODO also left


        if (debug) Debug.Log("OnPageTurnEnd: front [" + pageNumberFront + "] back [" + pageNumberBack + "] fv [" + pageNumberFirstVisible + "] lv [" + pageNumberLastVisible + "] dir [" + turnDirection + "]");
    }
    // Update is called once per frame
    void Update()
    {
        if (clickMe) {
            clickMe = false;
            OpenToPage(1);
        }
    }
}

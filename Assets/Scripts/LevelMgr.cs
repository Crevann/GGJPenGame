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
    [SerializeField] bool clickMe;
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
        Debug.Log("OnBookTurnToPageCompleted: State set to " + toState + ". Current Page Number = " + currentPageNumber);
    }

    protected virtual void OnPageTurnStart(Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, Page.TurnDirectionEnum turnDirection) {
        Debug.Log("OnPageTurnStart: front [" + pageNumberFront + "] back [" + pageNumberBack + "] fv [" + pageNumberFirstVisible + "] lv [" + pageNumberLastVisible + "] dir [" + turnDirection + "]");
    }

    protected virtual void OnPageTurnEnd(Page page, int pageNumberFront, int pageNumberBack, int pageNumberFirstVisible, int pageNumberLastVisible, Page.TurnDirectionEnum turnDirection) {
        
        Vector3 sizeOfCameraPage = dic[pageNumberFront].collider.size;
        foreach (Transform item in dic[pageNumberFront].mobs) {
            item.rotation = Quaternion.Euler(Vector3.right * 90);
            Vector3 cameraPagePosition = item.localPosition;
            Vector3 clampedPos;
            clampedPos.x = cameraPagePosition.x / (sizeOfCameraPage.x * 0.5f);
            clampedPos.y = cameraPagePosition.y / (sizeOfCameraPage.y * 0.5f);
            clampedPos.z = 0;



            Vector3 bookPagePosition;
            bookPagePosition.x = clampedPos.x * totalPageLength * 0.5f + 0.65f; //boh
            bookPagePosition.z = clampedPos.y * totalPageLength * 0.5f;
            bookPagePosition.y = 3.6f * 0.01f * book.transform.localScale.y;
            item.position = bookPagePosition + Vector3.left * (totalPageLength *0.5f - notViewablePageLength);

            item.localScale *= totalPageLength / sizeOfCameraPage.x;

        }


        //TODO also left


        Debug.Log("OnPageTurnEnd: front [" + pageNumberFront + "] back [" + pageNumberBack + "] fv [" + pageNumberFirstVisible + "] lv [" + pageNumberLastVisible + "] dir [" + turnDirection + "]");
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

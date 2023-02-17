using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;

public class BookmarksMgr : Singleton<BookmarksMgr>
{
    [System.Serializable]
    class BookMarkInfo {
        [SerializeField] public BookMark bookMark;
        [SerializeField] public List<EndlessBook.StateEnum> activeBookStates;
    }
    [SerializeField] List<BookMarkInfo> bookmarks = new List<BookMarkInfo>();
    [SerializeField] float bookmarkDelay = 1;
    float currentDelay = 0;
    public bool negative;
    public void ChangeScene() {
        foreach (var bookmark in bookmarks) {
            if(bookmark.bookMark.gameObject.activeSelf) bookmark.bookMark.Hide();
        }
    }
    public void Activate(EndlessBook.StateEnum currentState) {
        negative = (int)currentState >= 3;
        foreach (var bookmark in bookmarks) {
            if (!bookmark.activeBookStates.Contains(currentState)) continue;
            bookmark.bookMark.negative = negative;
            bookmark.bookMark.gameObject.SetActive(true);
        }
    }
    void Start(){
        negative = false;
    }

    public void StartCounting() {
        currentDelay = bookmarkDelay;
    }

    public void Update() {
        if (currentDelay > 0) {
            currentDelay -= Time.deltaTime;
        } else if (currentDelay < 0) {
            currentDelay = 0;
            Activate(CameraMgr.Instance.CurrentState);
        }
    }
}

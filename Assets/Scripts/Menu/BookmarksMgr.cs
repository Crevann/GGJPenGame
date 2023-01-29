using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookmarksMgr : Singleton<BookmarksMgr>
{
    [SerializeField] List<BookMark> bookmarks = new List<BookMark>();
    public bool negative;
    public void ChangeScene() {
        foreach (var bookmark in bookmarks) {
            bookmark.Hide();
        }
    }
    public void Activate() {
        foreach (var bookmark in bookmarks) {
            bookmark.gameObject.SetActive(true);
            bookmark.negative = negative;
        }
    }
    void Start()
    {
        negative = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

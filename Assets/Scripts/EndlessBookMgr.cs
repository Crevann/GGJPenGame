using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using echo17.EndlessBook;

public class EndlessBookMgr : Singleton<EndlessBookMgr>
{
    [HideInInspector] public EndlessBook book;
    void Awake()
    {
        book = GetComponent<EndlessBook>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] Root wordRoot;

    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("is in Inventary");
            gameObject.SetActive(false);
            inventory.AddWord(wordRoot);
        }
    }
}

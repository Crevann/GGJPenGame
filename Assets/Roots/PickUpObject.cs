using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    Inventory inventory;
    [SerializeField] Root wordRoot;
    [SerializeField] LayerMask playerLayer = (LayerMask) 9;

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
        if ((playerLayer.value & (1 << other.gameObject.layer)) > 0)
        {
            Debug.Log("is in Inventary");
            other.GetComponent<PlayerMovement>().GetItem(transform.position);
            GetComponent<Collider>().enabled = false;
            GetComponentInChildren<FadeEffect>().FadeOut();
            inventory.AddRoot(wordRoot);
        }
    }
}

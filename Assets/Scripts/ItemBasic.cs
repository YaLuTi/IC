using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBasic : MonoBehaviour
{
    public int ItemNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerPick>().SetPickItem(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerPick>().SetPickItem(null);
        }
    }

    public void Picked()
    {
        switch (ItemNum)
        {
            case 0:
                Debug.Log("Pick Item 0");
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}

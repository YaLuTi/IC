using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorNeedOpen : MonoBehaviour
{
    public GameObject door;
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
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerPick>().SetDoor(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerPick>().SetDoor(null);
        }
    }

    public void Open()
    {
        AkSoundEngine.PostEvent("OpenDoor", gameObject);
        door.transform.DOLocalRotate(new Vector3(0, 130, 0), 4f).SetEase(Ease.OutQuad);
        Destroy(this.gameObject);
    }
}

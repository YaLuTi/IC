using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBasic : MonoBehaviour
{
    public int ItemNum;
    public bool DeathEvent = false;
    GameObject player;
    public GameObject TestChangeWeaponFast;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (DeathEvent)
        {
            if (!PlayerBackpackData.FirstDeath)
            {
                Destroy(this.gameObject);
            }
        }
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
            case 1:
                PlayerBackpackData.HealthAmount += 1;
                Destroy(this.gameObject);
                break;
            case 2:
                PlayerBackpackData.HealthAmount += 2;
                Destroy(this.gameObject);
                break;
            case 3:
                PlayerBackpackData.HealthAmount += 3;
                Destroy(this.gameObject);
                break;
            case 4:
                PlayerBackpackData.SecondWeapon = true;
                Destroy(this.gameObject);
                break;
            case 5:
                player.GetComponent<PlayerWeaponController>().ChangeWeapon(TestChangeWeaponFast, 1);
                PlayerBackpackData.FirstWeapon = true;
                Destroy(this.gameObject);
                break;
            default:
                break;
        }
    }
}

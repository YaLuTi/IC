using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamage : MonoBehaviour
{
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (((1 << collision.gameObject.layer) & layerMask) != 0)
        {
            Debug.Log(1);
            if (collision.gameObject.tag == "Player")
            {
                collision.gameObject.GetComponent<PlayerHP>().Damaged(2.5f);
            }else if (collision.gameObject.tag == "Monster")
            {
                Debug.Log("D");
                collision.gameObject.GetComponent<MonsterBasic>().Damaged(1, collision.contacts[0].point, transform.position);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEventTrigger : MonoBehaviour
{
    bool IsIn = false;
    public CutSceneDirector director;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsIn)
        {
            if (Input.GetButtonDown("JoyStickO"))
            {
                director._BossScene();
                Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            IsIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            IsIn = false;
        }
    }
}

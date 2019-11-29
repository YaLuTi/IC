using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEventTrigger : MonoBehaviour
{
    public AK.Wwise.Event @event;
    bool IsT = false;
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
        if(other.gameObject.tag == "Player" && !IsT)
        {
            IsT = true;
            @event.Post(gameObject);
        }
    }
}

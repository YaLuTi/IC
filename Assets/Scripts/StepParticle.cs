using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepParticle : MonoBehaviour
{
    public GameObject particle;
    public AK.Wwise.Event akEvent;
    bool IsTrig = false;
    Collider TrigObj;
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
        if (other.gameObject.tag == "Floor" && IsTrig == false)
        {
            IsTrig = true;
            // AkSoundEngine.PostEvent("PlayerStep", gameObject);
            akEvent.Post(gameObject);
            TrigObj = other;
            Instantiate(particle, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(TrigObj != null)
        {
            if(other == TrigObj)
            {
                TrigObj = null;
                IsTrig = false;
            }
        }
    }
}

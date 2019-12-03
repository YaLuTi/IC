using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHint : MonoBehaviour
{
    public GameObject Hint;
    public GameObject BackGround;
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
            BackGround.SetActive(true);
            Hint.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            BackGround.SetActive(false);
            Hint.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        BackGround.SetActive(false);
        Hint.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHint : MonoBehaviour
{
    public GameObject Hint;
    public GameObject BackGround;
    bool Trig = false;
    bool Complete = false;
    public string TrigButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Hint.activeSelf && TrigButton != "")
        {
            if (Input.GetButtonDown(TrigButton) && !Complete)
            {
                Complete = true;
                BackGround.SetActive(false);
                Hint.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !Complete)
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
        if (BackGround != null)
        {
            BackGround.SetActive(false);
        }
        if (Hint != null)
        {
            Hint.SetActive(false);
        }
    }
}

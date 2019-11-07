using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSceneScript : MonoBehaviour
{
    [Header("EZMode")]
    public GameObject EZcam;
    public GameObject NoobCamera;
    public GameObject Player;    
    bool Mode = false;
    bool lastMode = false;
    // Start is called before the first frame update
    void Start()
    {
        /*EZcam.SetActive(true);
        NoobCamera.SetActive(false);
        Player.SetActive(false);*/
    }

    // Update is called once per frame
    void Update()
    {
        if(lastMode != Mode)
        {
            lastMode = Mode;
            if (Mode)
            {
                EZcam.SetActive(false);
                NoobCamera.SetActive(true);
                Player.GetComponent<PlayerMove>().enabled = true;
            }
            else
            {
                EZcam.SetActive(true);
                NoobCamera.SetActive(false);
                Player.GetComponent<PlayerMove>().enabled = false;
            }
        }
        if (Input.GetButtonDown("L3"))
        {
            Mode = !Mode;
        }
    }
}

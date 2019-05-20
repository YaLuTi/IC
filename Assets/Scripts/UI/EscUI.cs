using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscUI : MonoBehaviour {

    bool InMenu;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Esc"))
        {
            if (InMenu)
            {
                InMenu = false;
                Time.timeScale = 1;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
            else
            {
                InMenu = true;
                Time.timeScale = 0;
                Time.fixedDeltaTime = 0.02f * Time.timeScale;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemoCutScene : MonoBehaviour {

    public Image panel;
    public Camera CutSceneCamera;
    public Camera mainCamera;
    public GameObject CutScenePlayer;
    public GameObject mainPlayer;
    public float AwaitTime = 0.5f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator CutScene()
    {
        yield return new WaitForSecondsRealtime(AwaitTime);
        while (panel.color.a < 1)
        {
            Color c = panel.color;
            c.a += (1 - c.a) * 0.07f;
            if(c.a > 0.9f)
            {
                c.a = 1;
            }
            panel.color = c;
        }
        yield return 0;
    }
}

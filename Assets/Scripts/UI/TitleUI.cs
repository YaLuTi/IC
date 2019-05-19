using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour {

    public Image panel;
    public AnimationCurve animationCurve;
    float time;
    bool In = false;
	// Use this for initialization
	void Start () {
        panel.color = new Color(0, 0, 0, 1);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.anyKey && !In)
        {
            In = true;
        }

        if (!In)
        {
            if (time < 1)
            {
                time += Time.deltaTime;
                if (time > 1)
                {
                    time = 1;
                }
            }
        }
        else
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                if (time < 0)
                {
                    time = 0;
                    StartCoroutine(DisplayLoadingScreen("TestScene"));
                }
            }
        }
        panel.color = new Color(0, 0, 0, animationCurve.Evaluate(time));
	}

    IEnumerator DisplayLoadingScreen(string sceneName)////(1)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);////(2)
        while (!async.isDone)////(3)
        {
            Debug.Log(async.progress * 100);
            yield return null;
        }
    }
}

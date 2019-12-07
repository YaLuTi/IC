using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class TitleUI : MonoBehaviour
{

    public AK.Wwise.Event MenuMusic;
    public AK.Wwise.State Stop;
    public AK.Wwise.Event ButtonClick;
    public Image panel;
    public Image BlackPanel;
    public AnimationCurve animationCurve;
    public GameObject pressText;
    public GameObject Version;
    public GameObject Loading;
    float time;
    AudioSource audioSource;
    bool In = false;
    // Use this for initialization
    void Start()
    {
        BlackPanel.color = new Color(0, 0, 0, 1);
        audioSource = GetComponent<AudioSource>();
        MenuMusic.Post(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.anyKey && !In)
        {
            In = true;
            ButtonClick.Post(gameObject);
        }

        if (!In)
        {
            if (time < 1)
            {
                BlackPanel.color = new Color(0, 0, 0, animationCurve.Evaluate(time));
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
                pressText.SetActive(false);
                Version.SetActive(false);
                Loading.SetActive(true);
                panel.color = new Color(1, 1, 1, animationCurve.Evaluate(time));
                if (time < 0)
                {
                    time = 0;
                    StartCoroutine(DisplayLoadingScreen("SampleScene"));
                }
            }
        }
    }

    IEnumerator DisplayLoadingScreen(string sceneName)////(1)
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Stop.SetValue();
        while (BlackPanel.color.a < 1)
        {
            Color c = BlackPanel.color;
            c.a += (1 - c.a) * 0.05f;
            if (c.a > 0.9f)
            {
                c.a = 1;
            }
            BlackPanel.color = c;
            yield return 0;
        }

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName);////(2)
        while (!async.isDone)////(3)
        {
            Debug.Log(async.progress * 100);
            yield return null;
        }
    }
}

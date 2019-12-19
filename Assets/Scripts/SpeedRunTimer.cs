using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;


public class SpeedRunTimer : MonoBehaviour
{
    public static double RunTime;
    public TextMeshProUGUI text;
    public static bool IsRunning = true;
    public static bool Stop = false;
    public static string name = "Name";
    public bool Show = false;
    public GameObject Text;
    static double LastTime = 0;
    // Start is called before the first frame update
    void Start()
    {
        LastTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Stop)
        {
            RunTime += Time.time - LastTime;
            LastTime = Time.time;
        }
        TimeSpan timeSpan = TimeSpan.FromSeconds(RunTime);
        if (text != null)
        {
            text.text = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                    timeSpan.Hours,
                    timeSpan.Minutes,
                    timeSpan.Seconds,
                    timeSpan.Milliseconds);
        }
        if (Input.GetButtonDown("JoyStickPad"))
        {
            if (Show)
            {
                Text.SetActive(false);
                text.color = new Color(1, 1, 1, 0);
                Show = false;
            }
            else
            {
                Text.SetActive(true);
                text.color = new Color(1, 1, 1, 100f / 255f);
                Show = true;
            }
        }
    }

    public static void Write()
    {
        if((PlayerBackpackData.FirstWeapon || PlayerBackpackData.SecondWeapon))
        {
            PlayerPrefs.SetString("BestSwordPlayer", name);
            PlayerPrefs.SetString("BestSwordPTime", RunTime.ToString());
        }
        else
        {
            PlayerPrefs.SetString("BestHandPlayer", name);
            PlayerPrefs.SetString("BestHandPTime", RunTime.ToString());
        }

    }

    public void TimeStart()
    {
        text.gameObject.SetActive(true);
    }

    public void SetName(string s)
    {
        name = s;
    }
}

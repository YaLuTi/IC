using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class SpeedRunUI : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI BestPlayer;
    public TextMeshProUGUI SpendTime;
    public double BestTime;
    public static bool ChangeToSword = false;
    void Start()
    {
        if (!PlayerPrefs.HasKey("BestHandPlayer"))
        {
            PlayerPrefs.SetString("BestHandPlayer", "BigPong");
            PlayerPrefs.SetString("BestHandPTime", "14233221");
        }
        if (!PlayerPrefs.HasKey("BestSwordPlayer"))
        {
            PlayerPrefs.SetString("BestSwordPlayer", "Zian");
            PlayerPrefs.SetString("BestSwordPTime", "14233221");
        }
        if (PlayerPrefs.HasKey("BestHandPlayer"))
        {
            if (!ChangeToSword)
            {
                BestPlayer.text = "Hand Best : " + PlayerPrefs.GetString("BestHandPlayer");
                double time = double.Parse(PlayerPrefs.GetString("BestHandPTime"));
                BestTime = time;
                Debug.Log(time);
                TimeSpan timeSpan = TimeSpan.FromSeconds(time);
                SpendTime.text = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                        timeSpan.Hours,
                        timeSpan.Minutes,
                        timeSpan.Seconds,
                        timeSpan.Milliseconds);
            }
            else
            {
                BestPlayer.text = "Sword Best : " + PlayerPrefs.GetString("BestSwordPlayer");
                double time = double.Parse(PlayerPrefs.GetString("BestSwordPTime"));
                BestTime = time;
                Debug.Log(time);
                TimeSpan timeSpan = TimeSpan.FromSeconds(time);
                SpendTime.text = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                        timeSpan.Hours,
                        timeSpan.Minutes,
                        timeSpan.Seconds,
                        timeSpan.Milliseconds);
            }
        }
        else
        {
            BestPlayer.text = "Hand Best : " + "Panda";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerBackpackData.FirstWeapon && !ChangeToSword)
        {
            ChangeToSword = true;
            BestPlayer.text = "Sword Best : " + PlayerPrefs.GetString("BestSwordPlayer");
            double time = double.Parse(PlayerPrefs.GetString("BestSwordPTime"));
            BestTime = time;
            Debug.Log(time);
            TimeSpan timeSpan = TimeSpan.FromSeconds(time);
            SpendTime.text = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                    timeSpan.Hours,
                    timeSpan.Minutes,
                    timeSpan.Seconds,
                    timeSpan.Milliseconds);
        }
    }
}

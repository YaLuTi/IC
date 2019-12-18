using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBackpackData : MonoBehaviour
{
    public static bool WeaponOne;
    public static bool WeaponTwo;
    public static bool IsDoorOpen;
    public static bool FirstWeapon = false;
    public static bool SecondWeapon = false;
    public static bool FirstDeath = false;
    public static PlayerBackpackData dude;
    public static int HealthAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (dude == null)
        {
            dude = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

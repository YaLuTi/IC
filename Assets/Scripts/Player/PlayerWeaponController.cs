using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class PlayerWeaponController : MonoBehaviour {

    public GameObject WeaponParent;
    [Header("Put Your Weapon Here")]
    public GameObject SpawnWeapon;
    GameObject UsingWeapon;
    public WeaponList weaponList;
    [Header("Other Shit")]
    int WeaponNum = 0;
    public GameObject FirstWeapon;
    public GameObject SecondWeapon;

    public GameObject WeaponUI;

    public Image WeaponImageOne;
    public Image WeaponImageTwo;
    public TextMeshProUGUI text;
    public Sprite WeaponOneSprite;
    public Sprite WeaponTwoSprite;
    Image WeaponIcon;
    ButtonHighLight WeaponFrame;

    PlayerCombatEventer combatEventer;
    NewPlayerLongSword longSword;
    public AnimationEvent ChangeWeaponEvent;
	// Use this for initialization
	void Awake () {
        UsingWeapon = SpawnWeapon;
        UsingWeapon = Instantiate(UsingWeapon);
        UsingWeapon.transform.parent = WeaponParent.transform;
        UsingWeapon.transform.localPosition = Vector3.zero;
        UsingWeapon.transform.localRotation = Quaternion.identity;
        combatEventer = GetComponent<PlayerCombatEventer>();
        WeaponIcon = WeaponUI.GetComponent<Image>();
        WeaponFrame = WeaponUI.GetComponentInChildren<ButtonHighLight>();
        longSword = GetComponent<NewPlayerLongSword>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (CutSceneDirector.IsOnMovie) return;
        if (Input.GetButtonDown("JoyStickT"))
        {
            if (!PlayerBackpackData.FirstWeapon || !PlayerBackpackData.SecondWeapon) return;
            if (WeaponNum == 0) return;
            if (combatEventer.SetAnimation(ChangeWeaponEvent))
            {
                WeaponFrame._HighLight();
                if(WeaponNum == 1)
                {
                    WeaponNum = 2;
                    Destroy(UsingWeapon);
                    UsingWeapon = SecondWeapon;
                    UsingWeapon = Instantiate(SecondWeapon);
                    UsingWeapon.transform.parent = WeaponParent.transform;
                    UsingWeapon.transform.localPosition = Vector3.zero;
                    UsingWeapon.transform.localRotation = Quaternion.identity;
                    longSword.weapon = UsingWeapon.GetComponent<WeaponColliderBasic>();
                }
                else if(WeaponNum == 2)
                {
                    WeaponNum = 1;
                    Destroy(UsingWeapon);
                    UsingWeapon = FirstWeapon;
                    UsingWeapon = Instantiate(FirstWeapon);
                    UsingWeapon.transform.parent = WeaponParent.transform;
                    UsingWeapon.transform.localPosition = Vector3.zero;
                    UsingWeapon.transform.localRotation = Quaternion.identity;
                    longSword.weapon = UsingWeapon.GetComponent<WeaponColliderBasic>();
                }
            }
        }
        if(WeaponNum == 0)
        {
            WeaponImageOne.color = new Color(1,1,1,0);
            WeaponImageTwo.color = new Color(1, 1, 1, 0);
            text.text = "";
        }else if(WeaponNum == 1)
        {
            WeaponImageOne.color = new Color(1, 1, 1, 1);
            WeaponImageOne.sprite = WeaponOneSprite;
            if (PlayerBackpackData.SecondWeapon)
            {
                WeaponImageTwo.sprite = WeaponTwoSprite;
                WeaponImageTwo.color = new Color(1, 1, 1, 1);
            }
            else
            {
                WeaponImageTwo.sprite = WeaponTwoSprite;
                WeaponImageTwo.color = new Color(1, 1, 1, 0);
            }
            text.text = "長劍";
        }
        else if(WeaponNum == 2)
        {
            if (PlayerBackpackData.FirstWeapon)
            {
                WeaponImageOne.color = new Color(1, 1, 1, 1);
                WeaponImageOne.sprite = WeaponTwoSprite;
            }
            else
            {
                WeaponImageOne.color = new Color(1, 1, 1, 0);
                WeaponImageOne.sprite = WeaponTwoSprite;
            }

            WeaponImageTwo.color = new Color(1, 1, 1, 1);
            WeaponImageTwo.sprite = WeaponOneSprite;
            text.text = "聖劍";
        }
	}
    
    public void ChangeWeapon(GameObject weapon, int n)
    {
        WeaponNum = n;
        Destroy(UsingWeapon);
        UsingWeapon = weapon;
        UsingWeapon = Instantiate(weapon);
        UsingWeapon.transform.parent = WeaponParent.transform;
        UsingWeapon.transform.localPosition = Vector3.zero;
        UsingWeapon.transform.localRotation = Quaternion.identity;
        longSword.weapon = UsingWeapon.GetComponent<WeaponColliderBasic>();
    }
}

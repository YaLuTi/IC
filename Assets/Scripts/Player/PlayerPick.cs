using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPick : MonoBehaviour
{
    public AnimationEvent Pick;
    public AnimationEvent Open;
    PlayerCombatEventer combatEventer;
    public static bool CanPickItem = false;
    ItemBasic item;
    DoorNeedOpen door;
    // Start is called before the first frame update
    void Start()
    {
        combatEventer = GetComponent<PlayerCombatEventer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("JoyStickO"))
        {
            if(item != null)
            {
                if (combatEventer.SetAnimation(Pick))
                {
                    item.Picked();
                    CanPickItem = false;
                    item = null;
                }
            }else if (door != null)
            {
                if (combatEventer.SetAnimation(Open))
                {
                    door.Open();
                    CanPickItem = false;
                    door = null;
                }
            }
        }
    }

    public void SetPickItem(ItemBasic i)
    {
        item = i;
        if(item != null)
        {
            CanPickItem = true;
        }
        else
        {
            CanPickItem = false;
        }
    }
    public void SetDoor(DoorNeedOpen d)
    {
        door = d;
        if (door != null)
        {
            // CanPickItem = true;
        }
        else
        {
            // CanPickItem = false;
        }
    }
}

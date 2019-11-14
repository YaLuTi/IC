using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatEventer : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    public AnimationEvent PlayingEvent;
    public AnimationEvent NextEvent;
    AnimatorStateInfo StateInfo;
    PlayerHP playerHP;
    PlayerMove playerMove;

    public int ComboCount = 0;

    bool AnimatorChange = true; // Unity Animator need transtion. Use this to know is Animator already change.
    void Start()
    {
        animator = GetComponent<Animator>();
        playerHP = GetComponent<PlayerHP>();
        playerMove = GetComponent<PlayerMove>();
        StateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayingEvent != null)
        {
            if (PlayingEvent.Tag == "Dodge") playerMove.Rotateable = false;
            if (animator.IsInTransition(0)) return;
            StateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (PlayingEvent.Tag == "Attack")
            {
                ComboCount = 20;
                // animator.SetBool("IsOnCombo", true);
            }
            Debug.Log(StateInfo.normalizedTime);
            if (StateInfo.normalizedTime > PlayingEvent.EndTime)
            {
                // if (PlayingEvent.Tag == "Attack") animator.SetBool("IsOnCombo", false);
                if (PlayingEvent.Tag == "Dodge") playerMove.Rotateable = true;
                PlayingEvent = null;
                if(NextEvent != null)
                {
                    PlayingEvent = NextEvent;
                    playerHP.ExpendSP(PlayingEvent.CostStamina);
                    if (PlayingEvent.Tag == "Dodge") animator.SetTrigger("IsDodging");
                    NextEvent = null;
                }
            }
            // Debug.Log(StateInfo.normalizedTime);
        }
        /*if(ComboCount > 0)
        {
            ComboCount--;
            if(ComboCount == 0)
            {
                animator.SetBool("IsOnCombo", false);
            }
        }*/
    }

    // Interrupt level rull
    // same level can't interrupt each other. Except 0.
    // 0 - Stand, Walk, Lock
    // 1 - Using Item, Attack. Set End Time to 0 to make it can be interrupt in any time.
    // 2 - Dodge
    // 3 - Damaged
    public bool SetAnimation(AnimationEvent animationEvent)
    {
        if (PlayingEvent != null)
        {
            // IMPORTANT  Now can't do low stamina dodge
            if (NextEvent == null && animationEvent.Tag == "Attack" && PlayingEvent.Tag == "Attack")
            {
                NextEvent = animationEvent;
                animator.SetTrigger("Attack");
                return true;
            }
            if (!playerHP.CheckSP(animationEvent.CostStamina)) return false;
            if (animator.IsInTransition(0))
            {
                return false;
            }
            if (animationEvent.Tag == "Dodge")
            {
                StateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (NextEvent != null && PlayingEvent.Tag == "Attack")
                {
                    Debug.Log("D");
                    NextEvent = animationEvent;
                    animator.ResetTrigger("Attack");
                    return true;
                }
                if (StateInfo.normalizedTime < PlayingEvent.EndTime) return false;
            }
            if (PlayingEvent.InterruptLevel > animationEvent.InterruptLevel) return false;
            if (PlayingEvent.InterruptLevel == animationEvent.InterruptLevel && PlayingEvent.InterruptLevel != 0)
            {
                StateInfo = animator.GetCurrentAnimatorStateInfo(0);
                if (StateInfo.normalizedTime > PlayingEvent.ReadyTime && StateInfo.normalizedTime < PlayingEvent.EndTime) return false;
            }
            PlayingEvent = animationEvent;
            // playerHP.ExpendSP(PlayingEvent.CostStamina);
            if (PlayingEvent.AnimatorTriggerName != null) animator.SetTrigger(PlayingEvent.AnimatorTriggerName);
            StateInfo = animator.GetCurrentAnimatorStateInfo(0);
            return true;
        }
        else
        {
            PlayingEvent = animationEvent;
            if (PlayingEvent.AnimatorTriggerName != null) animator.SetTrigger(PlayingEvent.AnimatorTriggerName);
            return true;
        }
    }
}

[System.Serializable]
public class AnimationEvent
{
    public string EventName;
    public string TriggerButton;
    public string AnimatorTriggerName;
    public string Tag;
    public int InterruptLevel;
    public float CostStamina;
    public float ReadyTime;
    public float EndTime;
}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatEventer : MonoBehaviour
{
    // Start is called before the first frame update
    Animator animator;
    AnimationEvent PlayingEvent;
    AnimationEvent NextEvent;
    AnimationEvent empty;
    AnimatorStateInfo StateInfo;
    PlayerHP playerHP;
    PlayerMove playerMove;

    bool IsStateChange = false;
    public int ComboCount = 0;

    bool AnimatorChange = true; // Unity Animator need transtion. Use this to know is Animator already change.
    void Start()
    {
        animator = GetComponent<Animator>();
        playerHP = GetComponent<PlayerHP>();
        playerMove = GetComponent<PlayerMove>();
        PlayingEvent = empty;
        NextEvent = empty;
    }

    private void OnGUI()
    {
        if(PlayingEvent != empty)
        {
            GUI.TextArea(new Rect(10, 10, 100, 20), PlayingEvent.EventName);
        }
        else
        {
            GUI.TextArea(new Rect(10, 10, 100, 20), "");
        }
        if(NextEvent != empty)
        {
            GUI.TextArea(new Rect(10, 30, 100, 20), NextEvent.EventName);
        }
        else
        {
            GUI.TextArea(new Rect(10, 30, 100, 20), "");
        }
    } 

    // Update is called once per frame
    void Update()
    {
        if (PlayingEvent != empty)
        {
            if (PlayingEvent.Tag == "Dodge") playerMove.Rotateable = false;
            if (animator.IsInTransition(0)) return;
            StateInfo = animator.GetCurrentAnimatorStateInfo(0);
            // Debug.Log(StateInfo.normalizedTime);
            /*if (StateInfo.IsName(PlayingEvent.EventName)) IsStateChange = true;
            if (!IsStateChange) return;*/
            if (StateInfo.normalizedTime > PlayingEvent.EndTime && StateInfo.IsName(PlayingEvent.EventName))
            {
                // if (PlayingEvent.Tag == "Attack") animator.SetBool("IsOnCombo", false);
                if (PlayingEvent.Tag == "Dodge") playerMove.Rotateable = true;
                PlayingEvent = empty;
                if(NextEvent != empty)
                {
                    PlayingEvent = NextEvent;
                    IsStateChange = true; 
                   
                    playerHP.ExpendSP(PlayingEvent.CostStamina);
                    if (PlayingEvent.Tag == "Dodge")
                    {
                        animator.ResetTrigger("Attack");
                        animator.SetTrigger("IsDodging");
                    }
                    if(PlayingEvent.Tag == "Attack") animator.SetTrigger("Attack"); 
                    NextEvent = empty;
                    // Debug.Log(PlayingEvent.AnimatorTriggerName);
                }
            }
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
        // Debug.Log(animationEvent.TriggerButton);
        if (PlayingEvent != empty)
        {
            // Debug.Log("C1");
            if (!playerHP.CheckSP(animationEvent.CostStamina)) return false;
            if (NextEvent == empty && animationEvent.Tag == "Attack" && PlayingEvent.Tag == "Attack")
            {
                // Debug.Log("OOOOOOOOOOOOOOOOOOOOOOAAAAAAAAAAAAA");
                NextEvent = animationEvent;
                // 要把這個Set移到NowPlaying動畫結束 不然翻滾也不能用而且在Transtion中會出Bug
                // animator.SetTrigger("Attack");
                return true;
            }
            if (animator.IsInTransition(0))
            {
                return false;
            }
            if (animationEvent.Tag == "Dodge")
            {
                StateInfo = animator.GetCurrentAnimatorStateInfo(0);
                // if (StateInfo.normalizedTime < PlayingEvent.EndTime) return false;
                if (NextEvent == empty) NextEvent = animationEvent;
                if (NextEvent != empty && PlayingEvent.Tag == "Attack")
                {
                    Debug.Log("D");
                    NextEvent = animationEvent;
                    return true;
                }
            }
            if (NextEvent != empty)
            {
                return false;
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
            Debug.Log("C2");
            PlayingEvent = animationEvent;
            IsStateChange = false;
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




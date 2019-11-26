using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_SoundPlayer : StateMachineBehaviour
{
    public AnimationSoundEvent[] soundEvents;

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        for(int i = 0; i < soundEvents.Length; i++)
        {
            if( animatorStateInfo.normalizedTime >= soundEvents[i].time && !soundEvents[i].played)
            {
                soundEvents[i].AKevent.Post(animator.gameObject);
                soundEvents[i].played = true;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        for (int i = 0; i < soundEvents.Length; i++)
        {
           soundEvents[i].played = false;
        }
    }
}

[System.Serializable]
public class AnimationSoundEvent
{
    public AK.Wwise.Event AKevent;
    public float time;
    public bool played = false;
}



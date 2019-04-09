using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;



[CreateAssetMenu(menuName = "CreatAudioAssets")]
public class AudioAssets : AudioEvent {
    public AudioClip[] clips;

    public RangedFloat volume;

    [MinMaxRange(0f,2f)]
    public RangedFloat pitch;

    public override void Play(AudioSource source)
    {
        if (clips.Length == 0) return;

        source.clip = clips[Random.Range(0,clips.Length)];
        source.volume = Random.Range(volume.minValue, volume.maxValue);
        source.pitch = Random.Range(pitch.minValue, pitch.maxValue);
        source.PlayOneShot(source.clip);
    }
}

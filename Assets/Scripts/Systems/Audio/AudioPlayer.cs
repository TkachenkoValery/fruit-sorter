using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AudioPlayer : IInitializable
{
    public enum SoundID
    {
        ButtonSound,
        WhooshSound
    }

    private AudioSource ButtonSound;
    private AudioSource WhooshSound;

    private Dictionary<SoundID, AudioSource> AllSounds;

    public AudioEnabledStateHandler AudioEnabledStateHandler { get; private set; }

    public AudioPlayer(AudioEnabledStateHandler audioEnabledStateHandler, AudioSource buttonSound, AudioSource whooshSound)
    {
        AudioEnabledStateHandler = audioEnabledStateHandler;
        ButtonSound = buttonSound;
        WhooshSound = whooshSound;
    }

    public void Initialize()
    {
        AllSounds = new()
        {
            { SoundID.ButtonSound, ButtonSound },
            { SoundID.WhooshSound, WhooshSound }
        };
    }

    public void PlaySound(SoundID soundID)
    {
        if (AudioEnabledStateHandler.IsAudioEnabled && AllSounds.ContainsKey(soundID) && AllSounds[soundID] != null && !AllSounds[soundID].isPlaying)
        {
            AllSounds[soundID].Play();
        }
    }
}
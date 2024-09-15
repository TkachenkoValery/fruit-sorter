using System;
using UnityEngine;
using Zenject;

public class AudioSwitcherView : IInitializable, IDisposable
{
    private AudioPlayer AudioPlayer;
    private GameObject EnabledSign;
    private GameObject DisabledSign;

    public AudioSwitcherView(AudioPlayer audioPlayer, GameObject enabledSign, GameObject disabledSign)
    {
        AudioPlayer = audioPlayer;
        EnabledSign = enabledSign;
        DisabledSign = disabledSign;
    }

    public void Initialize()
    {
        VisualizeAudioEnableState(AudioPlayer.AudioEnabledStateHandler.IsAudioEnabled);
        AudioPlayer.AudioEnabledStateHandler.OnChangingAudioEnableState += VisualizeAudioEnableState;
    }

    public void Dispose()
    {
        AudioPlayer.AudioEnabledStateHandler.OnChangingAudioEnableState -= VisualizeAudioEnableState;
    }

    private void VisualizeAudioEnableState(bool newEnableState)
    {
        EnabledSign.SetActive(newEnableState);
        DisabledSign.SetActive(!newEnableState);
    }
}

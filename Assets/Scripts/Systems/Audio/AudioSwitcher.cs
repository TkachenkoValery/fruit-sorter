public class AudioSwitcher : OneActionPerformer
{
    private AudioPlayer AudioPlayer;

    public AudioSwitcher(AudioPlayer audioPlayer)
    {
        AudioPlayer = audioPlayer;
    }
    
    public override void PerformAction()
    {
        AudioPlayer.AudioEnabledStateHandler.SwitchEnableState();
    }
}
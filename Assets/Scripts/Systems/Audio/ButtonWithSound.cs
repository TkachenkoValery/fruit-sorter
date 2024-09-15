using UnityEngine.EventSystems;
using UnityEngine.UI;
using Zenject;

public class ButtonWithSound : IInitializable
{
    private AudioPlayer AudioPlayer;
    private Graphic GraphicForAddingEventTrigger;

    public ButtonWithSound(AudioPlayer audioPlayer, Graphic graphicForAddingEventTrigger)
    {
        AudioPlayer = audioPlayer;
        GraphicForAddingEventTrigger = graphicForAddingEventTrigger;
    }

    public void Initialize()
    {
        EventTrigger ThisEventTrigger = GraphicForAddingEventTrigger.gameObject.AddComponent<EventTrigger>();
        EventTrigger.Entry EntryForAdding = new();
        EntryForAdding.eventID = EventTriggerType.PointerDown;
        EntryForAdding.callback.AddListener(data => AudioPlayer.PlaySound(AudioPlayer.SoundID.ButtonSound));
        ThisEventTrigger.triggers.Add(EntryForAdding);
    }
}
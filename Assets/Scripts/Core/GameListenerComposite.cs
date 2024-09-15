using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameListenerComposite : MonoBehaviour, IGameFinishListener
{
    [Inject]
    private GameCycleEventHandler GameCycleEventHandler;
    
    [InjectLocal]
    private readonly List<IGameListener> GameListeners = new();

    private void Start()
    {
        GameCycleEventHandler.AddListener(this);
    }

    private void OnDestroy()
    {
        GameCycleEventHandler.AddListener(this);
    }

    void IGameFinishListener.OnFinishGame()
    {
        foreach (IGameListener gameListener in GameListeners)
        {
            if (gameListener is IGameFinishListener gameFinishListener)
            {
                gameFinishListener.OnFinishGame();
            }
        }
    }
}
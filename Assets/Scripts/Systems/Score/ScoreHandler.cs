using System;
using UnityEngine;
using Zenject;

public interface IScoreHandler : IInitializable, IFixedTickable
{
    public int CurrentValue { get; }
    public event Action OnChangingValue;
}

public class ScoreHandler : IScoreHandler
{
    private ScoreHandlerConfig Config;
    private GameCycleEventHandler GameCycleEventHandler;

    private float TimeAtPreviousDecreasing;

    private int _CurrentValue;
    public int CurrentValue
    {
        get => _CurrentValue;
        private set
        {
            _CurrentValue = value;
            OnChangingValue?.Invoke();
        }
    }

    public event Action OnChangingValue;

    public ScoreHandler(ScoreHandlerConfig scoreHandlerConfig, GameCycleEventHandler gameCycleEventHandler)
    {
        Config = scoreHandlerConfig;
        GameCycleEventHandler = gameCycleEventHandler;
    }

    public void Initialize()
    {
        CurrentValue = Config.StartScore;
        TimeAtPreviousDecreasing = Time.time;
    }

    public void FixedTick()
    {
        if (GameCycleEventHandler.CurrentGameState != GameState.Finished && CurrentValue > 0 && Time.time - TimeAtPreviousDecreasing >= Config.DecreasingSpeedInSeconds)
        {
            CurrentValue--;
            TimeAtPreviousDecreasing = Time.time;
        }
    }
}
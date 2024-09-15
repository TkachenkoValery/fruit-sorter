using System;
using UnityEngine;
using Zenject;

public interface ICoinsHandler : IInitializable, IDisposable
{
    public int CurrentValue { get; set; }
    public event Action OnChangingValue;
}

public class CoinsHandler : ICoinsHandler
{
    private CoinsHandlerConfig Config;
    
    private int _CurrentValue;
    public int CurrentValue
    {
        get => _CurrentValue;
        set
        {
            _CurrentValue = value >= 0 ? value : 0;
            OnChangingValue?.Invoke();
        }
    }

    public event Action OnChangingValue;

    public CoinsHandler(CoinsHandlerConfig config)
    {
        Config = config;
    }
    private void SaveCoinsAmount()
    {
        PlayerPrefs.SetInt(Config.KeyForPlayerPrefs, CurrentValue);
    }

    public void Initialize()
    {
        CurrentValue = PlayerPrefs.GetInt(Config.KeyForPlayerPrefs);
        OnChangingValue += SaveCoinsAmount;
    }

    public void Dispose()
    {
        OnChangingValue -= SaveCoinsAmount;
    }
}
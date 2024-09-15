using System;
using UnityEngine.UI;
using Zenject;

public class ScoreUIView : IInitializable, IDisposable
{
    private IScoreHandler ScoreHandler;
    private Text TextWithScore;

    public ScoreUIView(IScoreHandler scoreHandler, Text textWithScore)
    {
        ScoreHandler = scoreHandler;
        TextWithScore = textWithScore;
    }
    
    public void Initialize()
    {
        ScoreHandler.OnChangingValue += ShowCurrentScore;
        ShowCurrentScore();
    }

    public void Dispose()
    {
        ScoreHandler.OnChangingValue -= ShowCurrentScore;
    }

    private void ShowCurrentScore()
    {
        TextWithScore.text = ScoreHandler.CurrentValue.ToString();
    }
}
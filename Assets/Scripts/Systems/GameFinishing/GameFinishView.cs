using UnityEngine.UI;

public class GameFinishView : IGameFinishListener
{
    private MovableFinalWindow FinalWindow;
    private Text TextWithScoreOnGameFinishScreen;
    private Text TextWithCoinsOnGameFinishScreen;
    private IScoreHandler ScoreHandler;
    private CoinsUIView CoinsUIView;

    public GameFinishView(MovableFinalWindow finalWindow, Text textWithScoreOnGameFinishScreen, Text textWithCoinsOnGameFinishScreen, IScoreHandler scoreHandler, CoinsUIView coinsUIView)
    {
        FinalWindow = finalWindow;
        TextWithScoreOnGameFinishScreen = textWithScoreOnGameFinishScreen;
        TextWithCoinsOnGameFinishScreen = textWithCoinsOnGameFinishScreen;
        ScoreHandler = scoreHandler;
        CoinsUIView = coinsUIView;
    }

    void IGameFinishListener.OnFinishGame()
    {
        EndGame();
    }

    public void EndGame()
    {
        FinalWindow.Move();
        TextWithScoreOnGameFinishScreen.text = ScoreHandler.CurrentValue.ToString();
        CoinsUIView.TextsWithCoinsAmount.Add(TextWithCoinsOnGameFinishScreen);
    }
}
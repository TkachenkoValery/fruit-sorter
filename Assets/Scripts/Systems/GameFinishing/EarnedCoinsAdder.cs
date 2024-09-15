public class EarnedCoinsAdder : IGameFinishListener
{
    private ICoinsHandler ICoinsHandler;
    private IScoreHandler IScoreHandler;

    public EarnedCoinsAdder(ICoinsHandler iCoinsHandler, IScoreHandler iScoreHandler)
    {
        ICoinsHandler = iCoinsHandler;
        IScoreHandler = iScoreHandler;
    }

    void IGameFinishListener.OnFinishGame()
    {
        AddEarnedCoins();
    }

    private void AddEarnedCoins()
    {
        ICoinsHandler.CurrentValue += IScoreHandler.CurrentValue;
    }
}
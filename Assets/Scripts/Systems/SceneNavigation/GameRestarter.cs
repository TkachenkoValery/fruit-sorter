using UnityEngine.SceneManagement;

public class GameRestarter : OneActionPerformerWithBuildIndex
{
    public override void PerformAction()
    {
        SceneManager.LoadScene(BuildIndex);
    }
}
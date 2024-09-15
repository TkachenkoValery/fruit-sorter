using UnityEngine;

public class ApplicationQuitter : OneActionPerformer
{
    public override void PerformAction()
    {
        Application.Quit();
    }
}
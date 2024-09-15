public class SideUICloser : OneActionPerformer
{
    private MovableSideUI MovableSideUI;

    public SideUICloser(MovableSideUI movableSideUI)
    {
        MovableSideUI = movableSideUI;
    }

    public override void PerformAction()
    {
        MovableSideUI.Close();
    }
}
public class SideUIMover : OneActionPerformer
{
    private MovableSideUI MovableSideUI;

    public SideUIMover(MovableSideUI movableSideUI)
    {
        MovableSideUI = movableSideUI;
    }

    public override void PerformAction()
    {
        MovableSideUI.Move();
    }
}

namespace Snake_box
{
    public class PointerController : IExecute
    {
        public void Execute()
        {
            for (int i = 0; i < Services.Instance.LevelService.ActivePoints.Count; i++)
            {
                Services.Instance.LevelService.ActivePoints[i].UpdateTargetPoint();
            }
        }
    }
}

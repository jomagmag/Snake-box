
namespace Snake_box
{
    public class EnemyPointer : BasePointer
    {
        public EnemyPointer()
        {
            _pointer = Data.Instance.EnemyPointerData.Pointer;
            _border = Data.Instance.EnemyPointerData.Border;
        }       
    }
}

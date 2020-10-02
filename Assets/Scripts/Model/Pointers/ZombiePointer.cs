
namespace Snake_box
{
    public class ZombiePointer : BasePointer
    {
        public ZombiePointer()
        {
            _pointer = Data.Instance.ZombiePointerData.Pointer;
            _border = Data.Instance.ZombiePointerData.Border;
        }
    }
}

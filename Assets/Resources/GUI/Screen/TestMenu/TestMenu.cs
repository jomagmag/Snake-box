using UnityEngine;

namespace Snake_box
{
    public class TestMenu : MonoBehaviour
    {        
        void Start()
        {
            ScreenInterface.GetInstance().Execute(ScreenType.TestMenu);
        }       
    }
}

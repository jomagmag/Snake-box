using System.Collections.Generic;
using UnityEngine;

namespace Snake_box
{
    [CreateAssetMenu(fileName = "TrapsList", menuName = "Data/Level/Traps/TrapsList")]
    public class TrapList : ScriptableObject
    {
        public List<BaseTrapsData> TrapsList;
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Snake_box
{
    public sealed class TrapsController : IExecute, ICleanUp, IInitialization
    {

        #region Fields

        private List<BaseTraps> _traps => Services.Instance.LevelService.ActiveTraps;

        #endregion
        
        #region IInitialization

        public void Initialization()
        {

        }

        #endregion
        
        
        #region IExecute

        public void Execute()
        {
            if (_traps.Count > 0)
            {
                for (int i = 0; i < _traps.Count; i++)
                {
                    _traps[i].Execute();
                }
            }
        }

        #endregion

        
        #region ICleanUp

        public void Clean()
        {
            throw new NotImplementedException();
        }

        #endregion

        
    }
}

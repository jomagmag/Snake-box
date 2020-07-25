using UnityEngine;

namespace Snake_box
{
    public class GrenadeLauncherTraps : BaseTraps
    {
        private float _topBorderZ;
        private float _bottomBorderZ;
        private float _rightBorderX;
        private float _leftBorderX;
        private float _radius;
        private Transform _firePoint;
        
        
        public GrenadeLauncherTraps(BaseTrapsData data) : base(data)
        {
            _rightBorderX = Data.Instance.BordersData.RightBorderX;
            _leftBorderX = Data.Instance.BordersData.LeftBorderX;
            _topBorderZ = Data.Instance.BordersData.TopBorderZ;
            _bottomBorderZ = Data.Instance.BordersData.BottomBorderZ;
        }

        public override void Active()
        {
            
            _isActive = false;
        }


        private void GetFirePoint()
        {
            var x = Random.Range(_leftBorderX + _radius, _rightBorderX - _radius);
            var z = Random.Range(_bottomBorderZ + _radius, _topBorderZ - _radius);
            _firePoint.position = new Vector3(x,0,z);
        }
    }
}

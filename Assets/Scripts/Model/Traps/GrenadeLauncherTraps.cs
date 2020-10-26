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
        private Vector3 _firePoint;
        private float _damage;
        private TimeRemaining _timeRemaining;


        public GrenadeLauncherTraps(BaseTrapsData data) : base(data)
        {
            _rightBorderX = Data.Instance.BordersData.RightBorderX;
            _leftBorderX = Data.Instance.BordersData.LeftBorderX;
            _topBorderZ = Data.Instance.BordersData.TopBorderZ;
            _bottomBorderZ = Data.Instance.BordersData.BottomBorderZ;
            _damage = data.Damage;
            _timeRemaining = new TimeRemaining(GranadeLaunch,data.ReloadTime);
        }

        public override void Active()
        {
            GetFirePoint();
            _timeRemaining.AddTimeRemaining();
            _isActive = false;
        }


        private void GetFirePoint()
        {
            var x = Random.Range(_leftBorderX + _radius, _rightBorderX - _radius);
            var z = Random.Range(_bottomBorderZ + _radius, _topBorderZ - _radius);
            _firePoint = new Vector3(x,0,z);
        }
        
        
        private void GranadeLaunch()
        {
            var hits = Physics.OverlapSphere(_firePoint, _radius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag(TagManager.GetTag(TagType.Player)))
                {
                    Services.Instance.LevelService.CharacterBehaviour.SetArmor(_damage);
                }
            }
        }
    }
}

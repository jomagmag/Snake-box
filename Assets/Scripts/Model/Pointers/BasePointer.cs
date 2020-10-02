using UnityEngine;


namespace Snake_box
{
    public abstract class BasePointer
    {

        #region PrivateData

        protected GameObject _pointer;
        protected Vector3 _pointerPosition;
        protected int _border;
        private Transform _target;
        private Camera _camera;
        private Plane[] __lanes;

        #endregion


        public virtual void CreatePoint(Transform TargetTransform)
        {
            _target = TargetTransform;
            _pointer = Object.Instantiate(_pointer);
            _pointer.transform.SetParent(GameObject.FindGameObjectWithTag(TagManager.GetTag(TagType.Canvas)).transform);
            Services.Instance.LevelService.ActivePoints.Add(this);
            _camera = Camera.main;
        }

        public virtual void UpdateTargetPoint()
        {
            if (_target != null)
            {
                _pointerPosition = Camera.main.WorldToScreenPoint(_target.position);
                _pointerPosition.x = Mathf.Clamp(_pointerPosition.x, _border, Screen.width - _border);
                _pointerPosition.y = Mathf.Clamp(_pointerPosition.y, _border, Screen.height - _border);
                _pointer.transform.position = _pointerPosition;
                _pointer.transform.rotation = _target.rotation;
                __lanes = GeometryUtility.CalculateFrustumPlanes(_camera);
                if (GeometryUtility.TestPlanesAABB(__lanes,_target.GetComponent<Collider>().bounds))
                {
                    _pointer.SetActive(false);
                }
                else _pointer.SetActive(true);
            }
            else
            {
                GameObject.Destroy(_pointer);
                Services.Instance.LevelService.ActivePoints.Remove(this);
            } 
        }
    }
}

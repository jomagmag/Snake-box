using System;


namespace Snake_box
{
    public sealed class Services
    {
        #region ClassLifeCycles

        static Services()
        {
            Instance = new Services();
            Instance.Initialize();
        }

        #endregion


        #region Properties

        public static Services Instance { get; }
        public CameraServices CameraServices { get; private set; }
        public ITimeService TimeService { get; private set; }
        public PhysicsService PhysicsService { get; private set; }
        public ISaveData SaveData { get; private set; }
        public JsonService JsonService { get; private set; }
        public LevelService LevelService { get; private set; }
        public LevelLoadService LevelLoadService { get; private set; }
        public FlyingIconsService FlyingIconsService { get; private set; }
        
        public EventService EventService { get; private set; }
        
        #endregion
        
        
        #region Methods
        
        private void Initialize()
        {
            TimeService = new UnityTimeService();
            EventService = new EventService();
            LevelLoadService = new LevelLoadService();
            LevelService = new LevelService();
            CameraServices = new CameraServices();
            PhysicsService = new PhysicsService(CameraServices);
            SaveData = new PrefsService();
            JsonService = new JsonService();
        }
        
        #endregion
    }
}

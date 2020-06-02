﻿using System;
using UnityEngine;


namespace Snake_box
{
    public sealed class GameController : MonoBehaviour
    {
        #region Fields
        
        private Controllers _controllers;
        private bool _gameActive = false;

        #endregion


        #region UnityMethods

        private void Start()
        {
            Services.Instance.LevelLoadService.LevelLoaded += Initialize;
        }

        private void Initialize()
        {
            _gameActive = true;
            _controllers = new Controllers();
            Initialization();
            ScreenInterface.GetInstance().Execute(ScreenType.GameMenu);
        }

        private void Update()
        {
            if (!_gameActive)
                return;
            for (var i = 0; i < _controllers.Length; i++)
            {
                _controllers[i].Execute();
            }
        }

        #endregion

        public void Cleaner()
        {
            _controllers.Cleaner();
        }

        public void Initialization()
        {
            _controllers.Initialization();
        }
    }
}

﻿using UnityEngine;
using Zenject;

namespace Code
{
    public class PlayerActionHandler : ITickable
    {
        private readonly PlayerModel _playerModel;
        private readonly Dynamite.Pool _dynamitePool;
        private readonly DynamiteCounter _dynamiteCounter;
        private readonly PlayerInputState _inputState;
        private readonly Laser _laser;

        public PlayerActionHandler(
            PlayerModel playerModel,
            PlayerInputState playerInputState,
            Dynamite.Pool dynamitePool,
            DynamiteCounter dynamiteCounter,
            Laser laser)
        {
            _playerModel = playerModel;
            _inputState = playerInputState;
            _dynamitePool = dynamitePool;
            _dynamiteCounter = dynamiteCounter;
            _laser = laser;
        }

        public void Tick()
        {
            if (_playerModel.IsDead) return;

            // Todo: Input.GetKeyDown should be handled by an InputManager
            if (Input.GetKeyDown(KeyCode.DownArrow) && _playerModel.IsGrounded) PlaceDynamite();

            if (_inputState.IsFiring)
                _laser.IsFiring = true;
            else
                _laser.IsFiring = false;
        }

        private void PlaceDynamite()
        {
            if (_dynamiteCounter.DynamitesLeft <= 0) return;
            var dynamite = _dynamitePool.Spawn();
            dynamite.transform.position = _playerModel.Position;
            _dynamiteCounter.SubtractDynamite();
        }
    }
}
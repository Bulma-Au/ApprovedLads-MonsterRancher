﻿using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Monster_Rancher.PlayerInput;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using Utility_Classes;

namespace Monster_Rancher.Player
{
    public class PlayerController : MonoBehaviour
    {
        private float _moveSpeed = 3f;
        
        private PlayerInputHandler _playerInputHandler;
        private Vector2 _lastTargetVector2;


        private void Start ( )
        {
            _playerInputHandler = PlayerInputHandler.Instance;
            _playerInputHandler.OnMoveInputDetected.Subscribe ( HandlePlayerInputEvent ).AddTo ( gameObject );
        }

        private void HandlePlayerInputEvent ( InputAction.CallbackContext receivedContext)
        {
            if(receivedContext.started)
                return;
            
            var contextValue = receivedContext.ReadValue<Vector2> (  );
            _lastTargetVector2 = contextValue;
            MoveToTask( contextValue ).WrapErrors (  );
        }

        private async Task MoveToTask ( Vector2 inputDirection)
        {
            while ( _lastTargetVector2 == inputDirection )
            {
                gameObject.transform.Translate ( inputDirection * _moveSpeed * Time.deltaTime );
                await UniTask.WaitForEndOfFrame ( );
            }
        }
    }
}
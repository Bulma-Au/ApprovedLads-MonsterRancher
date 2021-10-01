using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using Debug = UnityEngine.Debug;

namespace Player.Input
{
    public class PlayerController : MonoBehaviour
    {
        private float moveSpeed = 3f;
        
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
            MoveToTask( contextValue );
        }

        private async Task MoveToTask ( Vector2 inputDirection)
        {
            while ( _lastTargetVector2 == inputDirection )
            {
                gameObject.transform.Translate ( inputDirection * moveSpeed * Time.deltaTime );
                await UniTask.WaitForEndOfFrame ( );
            }
        }
    }
}
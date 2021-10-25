using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Monster_Rancher.PlayerInput;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Monster_Rancher.MouseEventSystem
{
    public class MouseEventHandler: MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera;

        private PlayerInputHandler _inputHandler;

        private MouseEventTargetBase _currentMouseEventTarget;

        private ContactFilter2D _mouseOverFilter = new ContactFilter2D().NoFilter();
        private List<Collider2D> _mouseOverOverlapResults = new List<Collider2D>();

        private ContactFilter2D _mouseClickFilter = new ContactFilter2D().NoFilter();
        private List<Collider2D> _mouseClickOverlapResults = new List<Collider2D>();

        private void Start ( )
        {
            if ( _mainCamera == null )
                throw new Exception ( "Field _mainCamera is not referenced, this should never be the case." );
            
            _inputHandler = PlayerInputHandler.Instance;
            _inputHandler.OnMousePosInputDetected.Subscribe ( HandleMousePosInput ).AddTo ( gameObject );
            _inputHandler.OnMouseClickInputDetected.Subscribe ( HandleMouseClickInput ).AddTo ( gameObject );
        }

        private void HandleMousePosInput ( InputAction.CallbackContext receivedContext)
        {
            if ( receivedContext.started || receivedContext.canceled)
                return;
            
            CheckForMousePosOverlapPoint (  );
            
        }

        private void HandleMouseClickInput ( InputAction.CallbackContext receivedContext )
        {
            if(receivedContext.started || receivedContext.canceled)
                return;
            
            CheckMouseClickOverlapPoint (  );
        }

        private void CheckForMousePosOverlapPoint ( )
        {
            if ( Physics2D.OverlapPoint ( _mainCamera.ScreenToWorldPoint ( Mouse.current.position.ReadValue ( ) ), _mouseOverFilter, _mouseOverOverlapResults ) == 0 )
            {
                _currentMouseEventTarget = null;
                return; 
            }


            foreach (var targetCollider in _mouseOverOverlapResults)
            {
                targetCollider.TryGetComponent<MouseEventTargetBase>(out var foundTargetComponent);
                if(foundTargetComponent == null)
                    continue;

                var previousMouseEventTarget = _currentMouseEventTarget;
                _currentMouseEventTarget = foundTargetComponent;

                if ( previousMouseEventTarget == foundTargetComponent )
                    continue;

                FireMouseOverEvent( _currentMouseEventTarget );
            }
        }

        private void CheckMouseClickOverlapPoint ( )
        {
            if (Physics2D.OverlapPoint(_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()), _mouseClickFilter, _mouseClickOverlapResults) == 0)
                return;

            foreach (var targetCollider in _mouseClickOverlapResults)
            {
                targetCollider.TryGetComponent<MouseEventTargetBase>(out var foundTargetComponent);
                if(foundTargetComponent == null)
                    continue;
                
                foundTargetComponent.OnMouseClickReaction();
            }
        }

        private async Task FireMouseOverEvent ( MouseEventTargetBase targetMouseEventTarget)
        {
            targetMouseEventTarget.OnMouseEnterReaction (  );

            while ( _currentMouseEventTarget == targetMouseEventTarget )
            {
                targetMouseEventTarget.OnMouseOverReaction (  );
                var deltaTimeInMilliseconds = (int)(Time.deltaTime * 1000);
                await Task.Delay ( deltaTimeInMilliseconds );
            }
            
            targetMouseEventTarget.OnMouseExitReaction (  );
        }
    }
}
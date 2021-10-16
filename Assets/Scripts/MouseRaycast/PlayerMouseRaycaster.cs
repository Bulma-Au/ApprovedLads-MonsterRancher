using System;
using System.Collections.Generic;
using MouseRaycast;
using Player.Input;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMouseRaycaster: MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera;

        private PlayerInputHandler _inputHandler;

        private ContactFilter2D _mouseOverFilter = new ContactFilter2D().NoFilter();
        List<Collider2D> _mouseOverOverlapResults = new List<Collider2D>();

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
            
            FireMousePosRaycast (  );
            
        }

        private void HandleMouseClickInput ( InputAction.CallbackContext receivedContext )
        {
            if(receivedContext.started || receivedContext.canceled)
                return;
            
            FireMouseClickRaycast (  );
        }

        private void FireMousePosRaycast ( )
        {
            if(Physics2D.OverlapPoint(_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()), _mouseOverFilter, _mouseOverOverlapResults) == 0)
                return;

            foreach (var targetCollider in _mouseOverOverlapResults)
            {
                targetCollider.TryGetComponent<MouseRaycastTarget>(out var foundTargetComponent);
                if(foundTargetComponent == null)
                    continue;
                
                foundTargetComponent.OnMouseOverReaction();
            }
        }

        private void FireMouseClickRaycast ( )
        {
            if (Physics2D.OverlapPoint(_mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue()), _mouseClickFilter, _mouseClickOverlapResults) == 0)
                return;

            foreach (var targetCollider in _mouseClickOverlapResults)
            {
                targetCollider.TryGetComponent<MouseRaycastTarget>(out var foundTargetComponent);
                if(foundTargetComponent == null)
                    continue;
                
                foundTargetComponent.OnMouseClickReaction();
            }
        }
    }
}
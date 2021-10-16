using System;
using MouseRaycast;
using Player.Input;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerMouseRaycaster: MonoBehaviour
    {
        [SerializeField]
        private Camera _mainCamera;

        private PlayerInputHandler _inputHandler;

        private Vector2 _mousePos = new Vector2 ( 0,0 );

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
            var ray = _mainCamera.ScreenPointToRay ( Mouse.current.position.ReadValue (  ) );
            var rayOrigin = (Vector2) ray.origin;
            var rayDirection = (Vector2) ray.direction;
            
            var hits = Physics2D.RaycastAll ( rayOrigin, rayDirection, 10f );
            if ( hits.Length == 0 ) 
                return;
            
            foreach ( var raycastHit in hits )
            {
                raycastHit.collider.TryGetComponent < MouseRaycastTarget > ( out var foundTargetComponent );
                if(foundTargetComponent == null)
                    continue;
                
                foundTargetComponent.OnMouseOverReaction (  );
            }
        }

        private void FireMouseClickRaycast ( )
        {
            var ray = _mainCamera.ScreenPointToRay ( Mouse.current.position.ReadValue (  ) );
            var rayOrigin = (Vector2) ray.origin;
            var rayDirection = (Vector2) ray.direction;
            
            var hits = Physics2D.RaycastAll ( rayOrigin, rayDirection, 10f );
            if ( hits.Length == 0 ) 
                return;
            
            foreach ( var raycastHit in hits )
            {
                raycastHit.collider.TryGetComponent < MouseRaycastTarget > ( out var foundTargetComponent );
                if(foundTargetComponent == null)
                    continue;
                
                foundTargetComponent.OnMouseClickReaction (  );
            }
        }
    }
}
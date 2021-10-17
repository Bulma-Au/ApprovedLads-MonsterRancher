using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Monster_Rancher.PlayerInput
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private static PlayerInputHandler _instance;
        public static PlayerInputHandler Instance => _instance;
    
        [SerializeField]
        private UnityEngine.InputSystem.PlayerInput playerInputComponent;

        private Subject < InputAction.CallbackContext > onMoveInputDetected = new Subject < InputAction.CallbackContext > (  );
        public IObservable < InputAction.CallbackContext > OnMoveInputDetected => onMoveInputDetected;

        private Subject < InputAction.CallbackContext > onMousePosInputDetected = new Subject < InputAction.CallbackContext > ( );
        public IObservable < InputAction.CallbackContext > OnMousePosInputDetected => onMousePosInputDetected;

        private Subject < InputAction.CallbackContext > onMouseClickInputDetected = new Subject < InputAction.CallbackContext > ( );

        public IObservable < InputAction.CallbackContext > OnMouseClickInputDetected => onMouseClickInputDetected;

        private void Start ( )
        {
            if ( playerInputComponent == null )
                playerInputComponent = this.gameObject.GetComponent < UnityEngine.InputSystem.PlayerInput > ( );
        
            InitialiseEventCallbacks (  );
        }

        private void Awake ( )
        {
            _instance = this;
        }

        private void InitialiseEventCallbacks (  )
        {
            playerInputComponent.onActionTriggered += context =>
            {
                var contextActionName = context.action.name;
                switch ( contextActionName )
                {
                    case "Move":
                        onMoveInputDetected.OnNext ( context );
                        break;
                
                    case "Mouse Pos":
                        onMousePosInputDetected.OnNext ( context );
                        break;
                    
                    case "Mouse Click":
                        onMouseClickInputDetected.OnNext ( context );
                        break;
                    
                    default:
                        break;
                }
            };
        }
    }
}


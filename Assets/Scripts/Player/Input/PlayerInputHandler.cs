using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private static PlayerInputHandler _instance;
    public static PlayerInputHandler Instance => _instance;
    
    [SerializeField]
    private PlayerInput playerInputComponent;

    private Subject < InputAction.CallbackContext > onMoveInputDetected = new Subject < InputAction.CallbackContext > (  );
    public IObservable < InputAction.CallbackContext > OnMoveInputDetected => onMoveInputDetected;

    private void Start ( )
    {
        if ( playerInputComponent == null )
            playerInputComponent = this.gameObject.GetComponent < PlayerInput > ( );
        
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
                default:
                    break;
            }
        };
    }
}

using System;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Monster_Rancher.RuntimeDebuggerSystem
{
    /// <summary>
    /// Shelving this for now, it's not high priority.
    /// </summary>
    public class RuntimeDebugger : MonoBehaviour
    {
        //Singleton Pattern
        private static RuntimeDebugger _instance;
        public static RuntimeDebugger Instance => _instance;
        
        private bool _isDisplayingWindow = false;
        public bool IsDisplayingWindow => _isDisplayingWindow;
        [SerializeField]
        private GameObject _debuggerUIParent;
        [SerializeField]
        private TextMeshProUGUI _objectNameTextField;
        [SerializeField]
        private TextMeshProUGUI _debugInfoTextField;

        private void Start ( )
        {
            if ( _instance == null )
                _instance = this;
        }

        public void ToggleDebugWindow ( )
        {
            _debuggerUIParent.SetActive ( !_isDisplayingWindow );
            _isDisplayingWindow = !_isDisplayingWindow;
        }

        public void UpdateDebuggerWindow ( Object targetObject, string informationToDisplay )
        {
            _objectNameTextField.text = targetObject.name;
            _debugInfoTextField.text = informationToDisplay;
        }
    }
}
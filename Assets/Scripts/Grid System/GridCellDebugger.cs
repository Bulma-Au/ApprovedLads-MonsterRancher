using TMPro;
using UnityEngine;

namespace Monster_Rancher.Grids
{
    public class GridCellDebugger : MonoBehaviour
    {
        [SerializeField] 
        private GameObject selectionSpriteGameObject;
        [SerializeField] 
        private GameObject textFieldGameObject;
        [SerializeField] 
        private TextMeshProUGUI textField;

        public void Initialise ( )
        {
            SetDebugUIText (  );
            DisableDebugInfo (  );
        }
        
        private void SetDebugUIText ( )
        {
            var cachedTransform = transform.position;
            textField.text = $"X: {cachedTransform.x} \n Y: {cachedTransform.y}";
        }
        
        private void EnableDebugInfo ( )
        {
            selectionSpriteGameObject.SetActive ( true );
            textFieldGameObject.SetActive ( true );
        }

        private void DisableDebugInfo ( )
        {
            selectionSpriteGameObject.SetActive ( false );
            textFieldGameObject.SetActive ( false );
        }

        private void OnMouseEnter ( )
        {
            // Debug.Log ( $"Mouse entered: {gameObject.name}." );
            EnableDebugInfo (  );
        }

        private void OnMouseExit ( )
        {
            // Debug.Log ( $"Mouse exited: {gameObject.name}." );
            DisableDebugInfo (  );
        }
    }
}
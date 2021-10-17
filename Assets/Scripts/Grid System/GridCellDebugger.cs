using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

namespace Monster_Rancher.GridSystem
{
    public class GridCellDebugger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
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

        public void OnPointerEnter ( PointerEventData eventData )
        {
            //Debug.Log ( $"Mouse entered: {gameObject.name}." );
            EnableDebugInfo (  );
        }

        public void OnPointerExit ( PointerEventData eventData )
        {
            //Debug.Log ( $"Mouse exited: {gameObject.name}." );
            DisableDebugInfo (  );
        }

        public void OnPointerDown ( PointerEventData eventData )
        {
            //Debug.Log ( $"Mouse clicked: {gameObject.name}" );
        }
    }
}
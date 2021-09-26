using System;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Monster_Rancher.Grids
{
    public class GridCell : MonoBehaviour
    {
        private CustomGrid _gridParent;
        private Vector3 _sizeVector;
        
        [SerializeField] 
        private TextMeshProUGUI debugTextField;

        public void Initialise ( CustomGrid gridParent, int cellSize)
        {
            _gridParent = gridParent;
            _sizeVector = new Vector3 ( cellSize, cellSize, 0 );
            SetDebugUIText (  );
        }
        
        private void SetDebugUIText ( )
        {
            var cachedTransform = transform.position;
            debugTextField.text = $"X: {cachedTransform.x} \n Y: {cachedTransform.y}";
        }

        private void OnDrawGizmos ( )
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube ( this.transform.position, _sizeVector );
        }
    }
}
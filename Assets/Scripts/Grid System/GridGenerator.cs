using UnityEngine;

namespace Monster_Rancher.GridSystem
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] 
        private CustomGrid targetCustomGrid;

        public void GenerateTargetGrid ( )
        {
            targetCustomGrid.GenerateGrid (  );
        }

        public void DestroyTargetGrid ( )
        {
            targetCustomGrid.DestroyGrid (  );
        }
    }
}
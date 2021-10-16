using System.Collections.Generic;
using UnityEngine;

namespace Monster_Rancher.Grids
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
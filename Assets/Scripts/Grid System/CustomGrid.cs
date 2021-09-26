using System;
using System.Collections.Generic;
using UnityEngine;

namespace Monster_Rancher.Grids
{
    public class CustomGrid : MonoBehaviour
    {
        [SerializeField] 
        private int width = 16;
        [SerializeField] 
        private int height = 9;
        [SerializeField] 
        private int cellSize = 1;
        [SerializeField] 
        private List< GridCell >  gridCellsComponents = new List < GridCell > ();

        [SerializeField] 
        private GameObject gridCellPrefab;

        public void GenerateGrid ( )
        {
            for ( var xIndex = 0; xIndex < width; xIndex += cellSize )
            {
                for ( var yIndex = 0; yIndex < height; yIndex++ )
                {
                    var spawnedCellObject = Instantiate ( gridCellPrefab, new Vector3 ( xIndex, yIndex ), Quaternion.identity, this.transform );
                    spawnedCellObject.name = $"Grid Cell ({xIndex}, {yIndex})";
                    var gridCellComponent = spawnedCellObject.GetComponent < GridCell > ( );
                    gridCellsComponents.Add ( gridCellComponent );
                    spawnedCellObject.GetComponent <GridCell> (  ).Initialise ( this );
                }
            }
        }

        public void DestroyGrid ( )
        {
            foreach ( var gridCellsComponent in gridCellsComponents )
            {
                DestroyImmediate ( gridCellsComponent.gameObject );
            }
            
            gridCellsComponents.Clear (  );
        }
    }
}
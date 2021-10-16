using System;
using Player.Input;
using TMPro;
using UniRx;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Monster_Rancher.Grids
{
    public class GridCell : MonoBehaviour
    {
        //Private Fields
        private CustomGrid _gridParent;
        private GridCellDebugger _gridCellDebugger;
        private GridCellBackground _gridCellBackground;
        private GridCellForeground _gridCellForeground;
        
        //Public Fields
        public CustomGrid GridParent => _gridParent;
        public GridCellDebugger GridCellDebugger => _gridCellDebugger;
        public GridCellBackground CellBackground => _gridCellBackground;
        public GridCellForeground CellForeground => _gridCellForeground;
        

        public void Initialise ( CustomGrid gridParent)
        {
            _gridParent = gridParent;
            _gridCellDebugger = this.gameObject.GetComponent < GridCellDebugger > ( );
            _gridCellDebugger.Initialise (  );
            _gridCellBackground = this.gameObject.GetComponentInChildren < GridCellBackground > ( );
            _gridCellForeground = this.gameObject.GetComponentInChildren < GridCellForeground > ( );
        }
    }
}
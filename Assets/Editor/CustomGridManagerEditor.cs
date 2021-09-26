using Monster_Rancher.Grids;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(GridGenerator))]
    public class CustomGridManagerEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI ( )
        {
            DrawDefaultInspector ( );

            var targetGridManager = (GridGenerator) target;
            
            if(GUILayout.Button ( "Generate Target Grid" ))
                targetGridManager.GenerateTargetGrid (  );
            
            if(GUILayout.Button ( "Destroy Target Grid" ))
                targetGridManager.DestroyTargetGrid (  );
        }
    }
}

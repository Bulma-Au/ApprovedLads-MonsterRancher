using Monster_Rancher.MouseEventSystem;
using UnityEngine;

namespace Monster_Rancher.GridSystem
{
    public class GridMouseEventTargetConsumer : MouseEventTargetBase
    {
        public override void OnMouseOverReaction ( )
        {
            Debug.Log ( $"I've getting mouse overed! GameObject: {this.gameObject.name}." );
        }

        public override void OnMouseClickReaction ( )
        {
            Debug.Log ( $"I've been clicked! GameObject: {this.gameObject.name}." );
        }

        public override void OnMouseEnterReaction ( )
        {
            
        }

        public override void OnMouseExitReaction ( )
        {
            
        }
    }
}
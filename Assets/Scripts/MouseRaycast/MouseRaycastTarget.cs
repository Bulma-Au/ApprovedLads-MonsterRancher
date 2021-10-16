using UnityEngine;

namespace MouseRaycast
{
    public class MouseRaycastTarget : MonoBehaviour
    {
        public void OnMouseOverReaction ( )
        {
            Debug.Log ( $"{this.gameObject.name} is getting mouse overed!" );
        }

        public void OnMouseClickReaction ( )
        {
            Debug.Log ( $"{this.gameObject.name} is getting clicked!" );
        }
    }
}
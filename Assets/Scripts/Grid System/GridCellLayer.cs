using UnityEngine;

namespace Monster_Rancher.GridSystem
{
    public class GridCellLayer : MonoBehaviour
    {
        //Private Fields
        [SerializeField]
        private Sprite _currentSprite;
        private SpriteRenderer _spriteRenderer;
        
        //Public Fields
        [SerializeField]
        public Sprite CurrentSprite => _currentSprite;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
    }
}
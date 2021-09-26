using UnityEngine;

namespace Monster_Rancher.Grids
{
    public class GridCellLayer : MonoBehaviour
    {
        //Private Fields
        [SerializeField]
        private Sprite _currentSprite;
        private SpriteRenderer _spriteRenderer;
        
        //Public Fields
        [SerializeField]
        public Sprite currentCurrentSprite => _currentSprite;
        public SpriteRenderer spriteSpriteRenderer => _spriteRenderer;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace WorldGen
{
    public class PlatformsGenerator : MonoBehaviour
    {
        [SerializeField] private Sprite[] textures = new Sprite[3];
        [SerializeField] private GameObject platformBase;
        private SpriteRenderer _spriteRenderer;
        private BoxCollider2D[] _colliders = new BoxCollider2D[4];
    
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void Generate(Vector3 position, int texture, float width)
        {
            GameObject platform = Instantiate(platformBase, new Vector3(position.x, position.y, position.z),
                Quaternion.identity);
        
            _spriteRenderer = platform.GetComponent<SpriteRenderer>();
            _colliders = platform.GetComponents<BoxCollider2D>();
         
            _spriteRenderer.sprite = textures[texture];
            _spriteRenderer.size = new Vector2(width, _spriteRenderer.size.y);

            _colliders[0].size = new Vector2((width-0.04f), _colliders[0].size.y);
            _colliders[1].size = new Vector2((width-0.04f), _colliders[1].size.y);
            _colliders[2].offset = new Vector2(-(width - 0.02f) / 2, 0);
            _colliders[3].offset = new Vector2((width - 0.02f) / 2, 0);
            platform.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
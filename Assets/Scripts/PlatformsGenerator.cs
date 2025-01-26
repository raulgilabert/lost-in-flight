using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformsGenerator : MonoBehaviour
{
    public Sprite[] textures = new Sprite[3];
    public GameObject platform_base;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generate(Vector3 position, int texture, float width)
    {
        GameObject platform = Instantiate(platform_base, new Vector3(position.x/4, position.y, position.z), Quaternion.identity);
        platform.GetComponent<SpriteRenderer>().sprite = textures[texture];
        platform.GetComponent<SpriteRenderer>().size = new Vector2(width/4, platform.GetComponent<SpriteRenderer>().size.y);

        BoxCollider2D[] colliders = platform.GetComponents<BoxCollider2D>();



        colliders[0].size = new Vector2((width-0.04f)/4, platform.GetComponent<BoxCollider2D>().size.y);
        colliders[1].size = new Vector2((width-0.04f)/4, platform.GetComponent<BoxCollider2D>().size.y);
        colliders[2].offset = new Vector2(-(width - 0.02f) / 8, 0);
        colliders[3].offset = new Vector2((width - 0.02f) / 8, 0);
        platform.transform.localScale = new Vector3(4, 4, 4);
    }
}

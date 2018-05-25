using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScript : MonoBehaviour {
    private SpriteRenderer renderer;
    private Sprite blank;
    private Texture2D texture;
    private Rect rect = new Rect(0, 0, 100, 100);

    private Sprite server;

    private int collisionCount;

    private void Start()
    {
        texture = new Texture2D(100, 100);
        for (int w = 0; w < texture.width; w++)
        {
            for (int h = 0; h < texture.height; h++)
            {
                texture.SetPixel(w, h, Color.white);
            }
        }

        blank = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));
        renderer = GetComponent<SpriteRenderer>();

        //server = renderer.sprite;
    }

    public void SetSprite(Sprite server)
    {
        this.server = server;
        renderer.sprite = server;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionCount++;

        if (collisionCount > 0)
        {
            renderer.color = Color.red;
            renderer.sprite = blank;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collisionCount--;

        if (collisionCount <= 0)
        {
            renderer.color = Color.white;
            renderer.sprite = server;
        }
    }

    public bool InValidPosition()
    {
        return renderer.color != Color.red;
    }
}

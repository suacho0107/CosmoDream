using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;
    public static CursorManager Instance
    {
        get
        { return instance; }
    }
    
    #region Singleton
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public Sprite handSprite;
    public Sprite originalSprite;
    private Texture2D hand;
    private Texture2D original;

    void Start()
    {
        hand = SpriteToTexture2D(handSprite);
        original = SpriteToTexture2D(originalSprite);
    }

    public void OnMouseOver()
    {
        Cursor.SetCursor(hand, Vector2.zero, CursorMode.Auto);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(original, Vector2.zero, CursorMode.Auto);
    }

    private Texture2D SpriteToTexture2D(Sprite sprite)
{
    if (sprite.rect.width != sprite.texture.width)
    {
        Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);
        newText.filterMode = FilterMode.Point;
        newText.wrapMode = TextureWrapMode.Clamp;

        Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x,
                                                     (int)sprite.textureRect.y,
                                                     (int)sprite.textureRect.width,
                                                     (int)sprite.textureRect.height);
        newText.SetPixels(newColors);
        newText.Apply();
        return newText;
    }
    else
    {
        Texture2D newText = new Texture2D(sprite.texture.width, sprite.texture.height, TextureFormat.RGBA32, false);
        newText.SetPixels(sprite.texture.GetPixels());
        newText.Apply();
        return newText;
    }
}


}

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SpriteLoader : MonoBehaviour
{
    enum ScaleMode
    {
        ScaleToLoadedImage,
        ScaleToSprite
    }

    [SerializeField,
        Tooltip("Loaded Image: Keep image the same size\n"
        + "Sprite: Scale to match the size set in the editor")]
    private ScaleMode m_scaleMode = ScaleMode.ScaleToLoadedImage;

    [SerializeField, Tooltip("Path to asset relative to build's PROJECT_Data/ folder")]
    private string m_relativeFilePath = "";
    public string RelativeFilePath { get => m_relativeFilePath; set { m_relativeFilePath = value; OnEnable(); } }

    [SerializeField,
        Tooltip("Whether to throw an error if we can't find the file\n"
        + "(disable this for Shape Jam II week 1 - you won't have image files)")]
    private bool m_missingFileIsAnError = true;

    [SerializeField, Tooltip("Whether to clear the color (to white) when loading an image")]
    private bool m_clearColorAfterLoad = true;

    private SpriteRenderer m_spriteRenderer = null;
    private Image m_image = null;

    // verify that we have either a Sprite Renderer or Image; otherwise this component is useless
    private void Awake()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
        if (m_spriteRenderer == null)
        {
            m_image = GetComponent<Image>();
            if (m_image == null)
            {
                Debug.LogError($"Sprite Loader requires a Sprite Renderer or an Image, but neither exists in {name}.");
                Destroy(this);
            }
        }
    }

    // apply the texture from file to the Sprite Renderer or Image
    private void OnEnable()
    {
        if (m_spriteRenderer == null && m_image == null)
            return;

        // get texture from file
        var tex = LoadTexture();
        if (tex == null)
            return;

        // create rectangle for bounds
        var rect = new Rect(0, 0, tex.width, tex.height);

        // construct a Sprite for Unity to use
        // NOTE: this sprite defaults to 100 ppu
        var sprite = Sprite.Create(tex, rect, Vector2.one * 0.5f);

        // if we're doing a Sprite Renderer...
        if (m_spriteRenderer != null)
        {
            if (m_clearColorAfterLoad)
                m_spriteRenderer.color = Color.white;
            m_spriteRenderer.sprite = sprite;

            // scale if desired (sprites default to image size)
            if (m_scaleMode == ScaleMode.ScaleToSprite)
                transform.localScale = Vector2.one * 100f / sprite.rect.size;
        }

        // if we're doing an image...
        if (m_image != null)
        {
            if (m_clearColorAfterLoad)
                m_image.color = Color.white;
            m_image.sprite = sprite;

            // scale if desired (images default to in-editor size)
            if (m_scaleMode == ScaleMode.ScaleToLoadedImage)
            {
                m_image.rectTransform.sizeDelta *= sprite.rect.size / 100f;
            }
        }
    }

    private string FilePath
    {
        get
        {
            var appDataPath = Application.dataPath;

            return $"{appDataPath}/{m_relativeFilePath}";
        }
    }

    // load the texture from file
    private Texture2D LoadTexture()
    {
        // check for file 
        if (File.Exists(FilePath) == false)
        {
            if (m_missingFileIsAnError)
                Debug.LogError($"File for {name} does not exist: [{FilePath}].");

            return null;
        }

        // get the file data
        var data = File.ReadAllBytes(FilePath);

        // put it in a texture
        var tex = new Texture2D(2, 2);
        var wasLoaded = tex.LoadImage(data);

        if (wasLoaded == false)
        {
            Debug.LogError($"Failed to load data for {name} at [{FilePath}] (but file exists)");
            return null;
        }

        return tex;
    }
}

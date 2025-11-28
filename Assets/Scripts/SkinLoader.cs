using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerSkinLoader : MonoBehaviour
{
    public SpriteLibraryAsset planeRed;
    public SpriteLibraryAsset planeBlue;

    private SpriteLibrary spriteLibrary;
    public SpriteResolver spriteResolver;

    void Awake()
    {
        if (spriteResolver == null)
        spriteResolver = GetComponent<SpriteResolver>();

        spriteLibrary = GetComponent<SpriteLibrary>();

        string selected = PlayerPrefs.GetString("SelectedLibrary", "Red");

        if (selected == "Red")
        {
            spriteLibrary.spriteLibraryAsset = planeRed;
        }
        else if (selected == "Blue")
        {
            spriteLibrary.spriteLibraryAsset = planeBlue;
        }

        spriteResolver.SetCategoryAndLabel("Idle", "Idle");
        spriteResolver.ResolveSpriteToSpriteRenderer();
    }
}
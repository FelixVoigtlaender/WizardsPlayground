using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dresser : MonoBehaviour
{
    [Header("Hat")]
    public SpriteRenderer hat;
    public List<Sprite> hatSprites;
    [Header("Skin")]
    public SpriteRenderer skin;
    public List<Sprite> skinSprites;
    [Header("Face")]
    public SpriteRenderer face;
    public List<Sprite> faceSprites;

    public void Start()
    {
        DressUp();
    }

    public void DressUp()
    {
        Sprite hatSprite = GetRandomSprite(hatSprites);
        Sprite skinSprite = GetRandomSprite(skinSprites);
        Sprite faceSprite = GetRandomSprite(faceSprites);
        DressUp(hatSprite, skinSprite, faceSprite);
    }
    public void DressUp(Sprite hatSprite, Sprite skinSprite, Sprite faceSprite)
    {
        hat.sprite = hatSprite;
        skin.sprite = skinSprite;
        face.sprite = faceSprite;
    }


    public Sprite GetRandomSprite(List<Sprite> sprites)
    {
        int randomIndex = Random.Range(0, sprites.Count);
        return sprites[randomIndex];
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITopFoodItem : UIView
{
    public const string PREFAB_TopFoodItem = "App/Prefab/Game/UITopFoodItem";
    public Image imageCup;
    public Image imageFood;
    public int index;
    public float width;
    public float height;
    string strImageDirRoot = "App/UI/Game/TopFoodBar/CupFood";
    string strImageCup = "App/UI/Game/TopFoodBar/Cup";

    private void Awake()
    {

    }
    public void UpdateItem()
    {
        float w, h;
        {
            string pic = strImageCup;
            TextureUtil.UpdateImageTexture(imageCup, pic, true);
            w = imageCup.sprite.texture.width;
            h = imageCup.sprite.texture.height;
            float scale = Common.GetBestFitScale(w, h, width, height);
            imageCup.transform.localScale = new Vector3(scale, scale, 1f);
        }

        {
            string pic = strImageDirRoot + "/" + index.ToString();
            TextureUtil.UpdateImageTexture(imageFood, pic, true);
            w = imageFood.sprite.texture.width;
            h = imageFood.sprite.texture.height;
            float scale = Common.GetBestFitScale(w, h, width, height);
            imageFood.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}

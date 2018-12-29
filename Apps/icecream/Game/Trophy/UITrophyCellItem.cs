using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

//index 0:奖励星  1：奖牌 2:奖杯
public class UITrophyCellItem : UICellItemBase
{
    public Image imageBg;

    public GameObject objLeft;
    public Image imageIconLeftBg;
    public Image imageIconLeft;
    public Image imageLevel;


    public GameObject objRight;
    public Image imageRight0;
    public Image imageRight1;
    public Image imageRight2;
    public Image imageRight3;
    public Image imageRight4;
    public Image imageRight5;
    public Image imageRight6;
    public Image imageRight7;
    public Image imageRight8;
    public Image imageRight9;

    Image[] listImage = new Image[10];
    static Shader shaderGrey;
    private void Awake()
    {
        //  base.Awake();

        if (shaderGrey == null)
        {
            shaderGrey = Shader.Find("Custom/Grey");
        }
        Image[] listTmp = { imageRight0, imageRight1, imageRight2, imageRight3, imageRight4, imageRight5, imageRight6, imageRight7, imageRight8, imageRight9 };
        for (int i = 0; i < listTmp.Length; i++)
        {
            listImage[i] = listTmp[i];
        }
    }

    string GetImageOfIcon(int idx, int group)
    {
        if (idx == 0)
        {
            return GetImageOfStar(group);
        }
        if (idx == 1)
        {
            return GetImageOfMedal(group);
        }
        if (idx == 2)
        {
            return GetImageOfCup(group);
        }

        if (idx == 3)
        {
            //皇冠
            return AppRes.IMAGE_TROPHY_Crown_small;
        }
        return "";
    }
    //奖励星
    string GetImageOfStar(int group)
    {
        //1 - 1 - 1
        return AppRes.IMAGE_TROPHY_Star_PREFIX + group.ToString() + "-1-1";
    }
    //奖牌
    string GetImageOfMedal(int group)
    {
        //1 - 2 - 1
        return AppRes.IMAGE_TROPHY_Medal_PREFIX + group.ToString() + "-2-1";
    }
    //奖杯  
    string GetImageOfCup(int group)
    {
        //1-3-big
        return AppRes.IMAGE_TROPHY_Cup_PREFIX + group.ToString() + "-3-small";
    }


    public override void UpdateItem(List<object> list)
    {
        if (index < list.Count)
        {
            ItemInfo info = list[index] as ItemInfo;
            tagValue = info.tag;

            //  Vector4 border = AppRes.borderCellSettingBg;
            // TextureUtil.UpdateImageTexture(imageBg, AppRes.IMAGE_TROPHY_CELL_BG, false, border);
        }
        //level
        {
            //begain with 1
            int idx = 1;
            string pic = AppRes.IMAGE_TROPHY_LEVEL_PREFIX + idx.ToString();
            TextureUtil.UpdateImageTexture(imageLevel, pic, true);
        }

        //icon left
        {
            //begain with 1 
            int group = 1;
            string pic = GetImageOfIcon(index + 1, group);
            TextureUtil.UpdateImageTexture(imageIconLeft, pic, true);
            SetImageGrey(imageIconLeft, true);
        }


        //icon right 
        for (int i = 0; i < listImage.Length; i++)
        {
            Image image = listImage[i];
            //begain with 1
            int group = 1;
            string pic = GetImageOfIcon(index, group);
            TextureUtil.UpdateImageTexture(image, pic, true);
            SetImageGrey(image, true);
        }


        if (index == 2)
        {
            //2:奖杯
            for (int i = 5; i < listImage.Length; i++)
            {
                Image image = listImage[i];
                image.gameObject.SetActive(false);
            }

        }
        LayOut();
    }

    //变灰
    void SetImageGrey(Image image, bool enable)
    {
        //Shader "Custom/Grey" 
        if (enable)
        {
            image.material = new Material(shaderGrey);
        }
        else
        {
            image.material = null;
        }

    }

    public override void LayOut()
    {
        float x, y, w, h, ratio, scale;
        RectTransform rctranLeft = objLeft.GetComponent<RectTransform>();
        RectTransform rctranRight = objRight.GetComponent<RectTransform>();
        GridLayoutGroup grid = objRight.GetComponent<GridLayoutGroup>();

        float oft_border_x = 48;
        {
            ratio = 0.8f;
            w = height * ratio;
            h = w;

            rctranLeft.sizeDelta = new Vector2(w, h);
            x = -width / 2 + w /2+oft_border_x;
            y = 0;
            rctranLeft.anchoredPosition = new Vector2(x, y);

            scale = Common.GetBestFitScale(imageIconLeftBg.sprite.texture.width, imageIconLeftBg.sprite.texture.height, w, h);
            imageIconLeftBg.transform.localScale = new Vector3(scale, scale, 1f);
            scale = Common.GetBestFitScale(imageIconLeft.sprite.texture.width, imageIconLeft.sprite.texture.height, w, h);
            imageIconLeft.transform.localScale = new Vector3(scale, scale, 1f);
        }

        {
            ratio = 0.8f;

            w = (width / 2 - oft_border_x) - (rctranLeft.anchoredPosition.x + rctranLeft.rect.width / 2);
            h = height * ratio;

            rctranRight.sizeDelta = new Vector2(w, h);
            x = (width / 2 - oft_border_x) - w / 2;
            y = 0;
            rctranRight.anchoredPosition = new Vector2(x, y);


            float cell_w = w / 5;
            float cell_h = h / 2;
            if (index == 2)
            {
                //2:奖杯 显示一行
                cell_h = h;
            }
            grid.cellSize = new Vector2(cell_w, cell_h);
        }



        //icon right 
        for (int i = 0; i < listImage.Length; i++)
        {
            Image image = listImage[i];
            scale = Common.GetBestFitScale(image.sprite.texture.width, image.sprite.texture.height, grid.cellSize.x, grid.cellSize.y);
            image.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
}




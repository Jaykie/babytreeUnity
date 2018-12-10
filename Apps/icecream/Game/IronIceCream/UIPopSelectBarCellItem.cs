using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPopSelectBarCellItem : UICellItemBase
{
    public Image imageBg;

    public override void UpdateItem(List<object> list)
    {
        FoodItemInfo info = list[index] as FoodItemInfo;
        Debug.Log("UpdateItem:info.pic="+info.pic);
        TextureUtil.UpdateImageTexture(imageBg, info.pic, true);

        LayOut();
    }
    public override bool IsLock()
    {
        return false;
    }

    public override void LayOut()
    {
        RectTransform rctran = imageBg.GetComponent<RectTransform>();
        float ratio = 1f;

        float scale = Common.GetBestFitScale(rctran.rect.width, rctran.rect.height, width, height) * ratio;
        imageBg.transform.localScale = new Vector3(scale, scale, 1.0f);

    }
}

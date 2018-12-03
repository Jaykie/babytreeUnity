using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/*铁板冰淇淋步骤：
2,装淇淋卷到碗里
*/
public delegate void OnGameIronIceCreamDidUpdateStatusDelegate(UIView ui, int status);


public class TopFoodItemInfo
{
    public GameObject obj;
    public string name;
    public Vector3 pt; //world
    public int type;
}
public class IronIceCreamStepBase : UIView
{
    public const string IMAGE_WAN_DIR_ROOT = "App/UI/Game/TopFoodBar/Wan";
    public const int STATUS_STEP_NONE = 0;
    public const int STATUS_STEP_START = 1;
    public const int STATUS_STEP_END = 2;
    public const int STATUS_Liquid_Finish = 3;//倒完冰淇凌液

    //顶料
    public static string[] strTopFoodSort = { "cream", "sugar", "chocolate", "wire", "egg", "fruit", "scoop" };
    public static int[] countTopFoodSort = { 142, 98, 29, 20, 27, 53, 40 };

    public GameObject objPanzi;//盘子
    public Rect rectMain;//local 
    public OnGameIronIceCreamDidUpdateStatusDelegate callBackDidUpdateStatus { get; set; }

    //碗
    static public string GetImageOfWan(int idx)
    {
        return IMAGE_WAN_DIR_ROOT + "/" + GameIronIceCream.indexFood.ToString() + "-" + (idx + 1).ToString();
    }
    public void UpdateRect()
    {
        float x, y, w, h;
        RectTransform rctranMainWorld = AppSceneBase.main.GetRectMainWorld();

        float topbar_canvas_h = 160f;
        float oft_h = Common.CanvasToWorldHeight(mainCam, AppSceneBase.main.sizeCanvas, topbar_canvas_h) * 2;
        w = rctranMainWorld.rect.width;
        h = rctranMainWorld.rect.height - oft_h;
        x = -w / 2;
        y = rctranMainWorld.rect.height / 2 - oft_h - h;
        rectMain = new Rect(x, y, w, h);
    }

    public void LayOutBase()
    {
        float x, y, w, h;
        float scale = 0;
        RectTransform rctranMainWorld = AppSceneBase.main.GetRectMainWorld();
        UpdateRect();


        float ratio = 0.8f;
        {
            SpriteRenderer rd = objPanzi.GetComponent<SpriteRenderer>();
            w = rd.sprite.texture.width / 100f;
            h = rd.sprite.texture.height / 100f;
            scale = Common.GetBestFitScale(w, h, rectMain.width, rectMain.height) * ratio;
            objPanzi.transform.localScale = new Vector3(scale, scale, 1f);
        }

        this.transform.localPosition = new Vector3(rectMain.center.x, rectMain.center.y, 0f);
    }

    public virtual void ResetStep()
    {

    }
    public virtual void OnUITopFoodItemDidClick(UITopFoodItem item)
    {

    }
}

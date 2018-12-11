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

    //顶料
    public const string IMAGE_TOPFOOD_DIR_ROOT = "App/UI/Game/IronIceCream/TopFood";
    public const int STATUS_STEP_NONE = 0;
    public const int STATUS_STEP_START = 1;
    public const int STATUS_STEP_END = 2;
    public const int STATUS_Liquid_Finish = 3;//倒完冰淇凌液

    //顶料
    public static string[] strTopFoodSort = { "cream", "sugar", "chocolate", "wire", "egg", "fruit", "scoop" };
    public static int[] countTopFoodSort = { 142, 98, 29, 20, 27, 53, 40 };
    static public UIWanIron uiWanIronPrefab;
    static public UIWanIron uiWanIron;
    public GameObject objPanzi;//盘子
    public GameObject objHand;//操作提示的手 
    public Rect rectMain;//local 
    Tween tweenAlpha;
    public OnGameIronIceCreamDidUpdateStatusDelegate callBackDidUpdateStatus { get; set; }

    //杯子food
    static public string GetImageOfCupFood(int idx)
    {
        string strImageDirRoot = "App/UI/Game/TopFoodBar/CupFood";
        return strImageDirRoot + "/" + idx.ToString();
    }

    //碗
    static public string GetImageOfWan(int idx)
    {
        return IMAGE_WAN_DIR_ROOT + "/" + GameIronIceCream.indexFood.ToString() + "-" + (idx + 1).ToString();
    }

    static public string GetImageOfTopFood(int idx)
    {
        return IMAGE_TOPFOOD_DIR_ROOT + "/" + IronIceCreamStepBase.strTopFoodSort[idx]; ;
    }
    static public string GetImageOfTopFoodSubFood(int idx)
    {
        int indexsort = UIPopSelectBar.indexFoodSort;
        string namesort = IronIceCreamStepBase.strTopFoodSort[indexsort];
        return IMAGE_TOPFOOD_DIR_ROOT + "/" + namesort + "/" + idx.ToString();
    }

    //冰淇凌块-2
    static public string GetBlockItemPic()
    {
        //0-2
        return GameIronIceCream.IMAGE_DIR_ROOT_SingleColor + "/" + GameIronIceCream.indexFood + "-2";
    }
    //冰淇淋液
    static public string GetImageOfIcecreemLiquid(int idx)
    {
        return GameIronIceCream.IMAGE_DIR_ROOT_SingleColor + "/" + idx;
    }
    //冰淇淋片-1
    static public string GetImageOfIcecreemPiece(int idx)
    {
        return GetImageOfIcecreemLiquid(idx) + "-1";
    }
    //冰淇凌卷-3
    static public string GetWanJuanPic()
    {
        //0-3
        return GameIronIceCream.IMAGE_DIR_ROOT_SingleColor + "/" + GameIronIceCream.indexFood + "-3";
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
        uiWanIron.UpdateRect(rectMain);
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


    public virtual void UpdateFood(FoodItemInfo info)
    {

    }
    //闪烁动画
    public void ShowHandFlickerAnimation(bool isAnimation)
    {
        if(objHand==null){
            return;
        }
        if (isAnimation)
        {

            // ActionBlink ac = this.gameObject.AddComponent<ActionBlink>();
            // ac.duration = 3f;
            // ac.count = 75;
            // ac.target = imageHand.gameObject;
            // ac.isLoop = true;
            // ac.Run(); 
            float duration = 1f;

            //ng：这种方法淡入淡出会改变整个ui的alpha
            // imageHand.material.DOFade(0, duration).SetLoops(-1, LoopType.Yoyo);
            //imageHand.color = Color.white;
            if (tweenAlpha == null)
            {
                SpriteRenderer rd = objHand.GetComponent<SpriteRenderer>();
                tweenAlpha = DOTween.ToAlpha(() => rd.color, x => rd.color = x, 0f, duration).SetLoops(-1, LoopType.Yoyo);
            }
            tweenAlpha.Play();

        }
        else
        {

            if (tweenAlpha != null)
            {
                tweenAlpha.Pause();
            }

        }
    }
}

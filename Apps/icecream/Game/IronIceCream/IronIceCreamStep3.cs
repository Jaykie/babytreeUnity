using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
/* 
3，装顶料 
*/
public class IronIceCreamStep3 : IronIceCreamStepBase
{
    public GameObject objWan;//碗 
    public GameObject objWanFt;//碗 
    public GameObject objWanBg;//碗bg Wan_bg
    public GameObject objWanItemRoot;
    public GameObject objWanItem0;
    public GameObject objWanItem1;
    public GameObject objWanItem2;
    public GameObject objWanItem3;
    public GameObject objWanItem4;
    public GameObject objWanItem5;
    public List<TopFoodItemInfo> listItem;
    public GameObject objHand;//操作提示的手 
    int indexStep = 0;
    int totalStep = 4;
    Vector3 posLocalTouchDown;
    Vector3 posInputTouchDown;
    void Awake()
    {
        listItem = new List<TopFoodItemInfo>();
        TextureUtil.UpdateSpriteTexture(objHand, AppRes.IMAGE_HAND);
        TextureUtil.UpdateSpriteTexture(objWanBg, UITopFoodItem.IMAGE_WAN_BG);
        ResetStep();



    }
    void Start()
    {
        OnDoStep(GameIronIceCream.indexFood);
        LayOut();
    }

    public override void LayOut()
    {
        float x, y, w, h;
        float scale = 0;
        LayOutBase();

        float ratio = 0.8f;
        {
            SpriteRenderer rd = objWanBg.GetComponent<SpriteRenderer>();
            if (rd.sprite != null)
            {
                w = rd.sprite.texture.width / 100f;
                h = rd.sprite.texture.height / 100f;
                scale = Common.GetBestFitScale(w, h, rectMain.width, rectMain.height) * ratio;
                objWanBg.transform.localScale = new Vector3(scale, scale, 1f);
            }
        }
        {
            SpriteRenderer rd = objWanFt.GetComponent<SpriteRenderer>();
            if (rd.sprite != null)
            {
                w = rd.sprite.texture.width / 100f;
                h = rd.sprite.texture.height / 100f;
                scale = Common.GetBestFitScale(w, h, rectMain.width, rectMain.height) * ratio;
                objWanFt.transform.localScale = new Vector3(scale, scale, 1f);
            }
        }
    }
    //冰淇凌块
    string GetBlockItemPic()
    {
        //0-2
        return GameIronIceCream.IMAGE_DIR_ROOT_SingleColor + "/" + GameIronIceCream.indexFood + "-2";
    }

    //冰淇凌卷
    string GetWanJuanPic()
    {
        //0-3
        return GameIronIceCream.IMAGE_DIR_ROOT_SingleColor + "/" + GameIronIceCream.indexFood + "-3";
    }

    void UpdateWanItem(GameObject obj)
    {
        Texture2D tex = TextureCache.main.Load(GetWanJuanPic());
        TextureUtil.UpdateSpriteTexture(obj, tex);
        // BoxCollider box = obj.GetComponent<BoxCollider>();
        // box.size = new Vector3(tex.width / 100f, tex.height / 100f);
    }
    void UpdateWan()
    {
        objWan.SetActive(true);
        //  Debug.Log("UpdateWan:pic="+pic);
        string pic = IronIceCreamStep2.strImageWan;
        if (pic != null)
        {
            TextureUtil.UpdateSpriteTexture(objWanFt, pic);
        }


    }


    void UpdateItem()
    {
        UpdateWanItem(objWanItem0);
        UpdateWanItem(objWanItem1);
        UpdateWanItem(objWanItem2);
        UpdateWanItem(objWanItem3);
        UpdateWanItem(objWanItem4);
        UpdateWanItem(objWanItem5);
    }

    public void OnAddTopFood(string pic)
    {
        TopFoodItemInfo info = new TopFoodItemInfo();
        info.name = FileUtil.GetFileName(pic);

        GameObject obj = new GameObject(info.name);
        SpriteRenderer rd = obj.AddComponent<SpriteRenderer>();
        Texture2D tex = TextureCache.main.Load(pic);
        rd.sprite = LoadTexture.CreateSprieFromTex(tex);

        BoxCollider box = obj.AddComponent<BoxCollider>();
        box.size = new Vector2(tex.width / 100f, tex.height / 100f);

        UITouchEventWithMove ev = obj.AddComponent<UITouchEventWithMove>();
        ev.callBackTouch = OnUITouchEvent;

        //AppSceneBase.main.AddObjToMainWorld(obj);
        obj.transform.SetParent(objWan.transform);
        obj.transform.localPosition = new Vector3(0, 0, -1);
        obj.transform.localScale = new Vector3(1, 1, 1);
        info.obj = obj;
        info.pt = Vector3.zero;

        listItem.Add(info);
    }
    public void OnDoStep(int idx)
    {

        objHand.SetActive(false);
        UpdateItem();

    }


    public override void ResetStep()
    {
        objPanzi.SetActive(false);
        objWan.SetActive(false);
        objWanItem0.SetActive(false);
        objWanItem1.SetActive(false);
        objWanItem2.SetActive(false);
        objWanItem3.SetActive(false);
        objWanItem4.SetActive(false);
        objWanItem5.SetActive(false);

    }

    public override void OnUITopFoodItemDidClick(UITopFoodItem item)
    {
        UpdateWan();
        OnAddTopFood(item.strPic);
        LayOut();
    }

    public void OnUITouchEvent(UITouchEvent ev, PointerEventData eventData, int status)
    {
        Vector3 posworld = Common.GetInputPositionWorld(mainCam);
        Vector3 poslocal = objWan.transform.InverseTransformPoint(posworld);
        poslocal.z = ev.gameObject.transform.localPosition.z;

        switch (status)
        {
            case UITouchEvent.STATUS_TOUCH_DOWN:
                {
                    posInputTouchDown = posworld;
                    posLocalTouchDown = poslocal;
                }
                break;
            case UITouchEvent.STATUS_TOUCH_MOVE:
                {
                    Vector3 step = posworld - posInputTouchDown;

                    Vector3 posnow = posLocalTouchDown + step;
                    posnow.z = poslocal.z;
                    ev.gameObject.transform.localPosition = posnow;
                }
                break;
            case UITouchEvent.STATUS_TOUCH_UP:
                {

                }
                break;
        }
    }
}

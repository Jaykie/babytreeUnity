using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
/*铁板冰淇淋步骤：
2,装淇淋卷到碗里
*/
public class IronIceCreamStep2 : IronIceCreamStepBase
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


    public GameObject objBlock;//块
    public GameObject objBlockItem0;
    public GameObject objBlockItem1;
    public GameObject objBlockItem2;
    public GameObject objBlockItem3;
    public GameObject objBlockItem4;
    public GameObject objBlockItem5;

    static public string strImageWan;
    public GameObject objHand;//操作提示的手 
    int indexStep = 0;
    int totalStep = 4;

    void Awake()
    {

        strImageWan = IronIceCreamStepBase.GetImageOfWan(0);
        TextureUtil.UpdateSpriteTexture(objHand, AppRes.IMAGE_HAND);
        TextureUtil.UpdateSpriteTexture(objWanBg, UITopFoodItem.IMAGE_WAN_BG);
        ResetStep();
        {
            UITouchEvent ev = objBlockItem0.AddComponent<UITouchEvent>();
            ev.callBackTouch = OnUITouchEvent;
            BoxCollider box = objBlockItem0.AddComponent<BoxCollider>();
        }

        {
            UITouchEvent ev = objBlockItem1.AddComponent<UITouchEvent>();
            ev.callBackTouch = OnUITouchEvent;
            BoxCollider box = objBlockItem1.AddComponent<BoxCollider>();
        }
        {
            UITouchEvent ev = objBlockItem2.AddComponent<UITouchEvent>();
            ev.callBackTouch = OnUITouchEvent;
            BoxCollider box = objBlockItem2.AddComponent<BoxCollider>();
        }
        {
            UITouchEvent ev = objBlockItem3.AddComponent<UITouchEvent>();
            ev.callBackTouch = OnUITouchEvent;
            BoxCollider box = objBlockItem3.AddComponent<BoxCollider>();
        }
        {
            UITouchEvent ev = objBlockItem4.AddComponent<UITouchEvent>();
            ev.callBackTouch = OnUITouchEvent;
            BoxCollider box = objBlockItem4.AddComponent<BoxCollider>();
        }
        {
            UITouchEvent ev = objBlockItem5.AddComponent<UITouchEvent>();
            ev.callBackTouch = OnUITouchEvent;
            BoxCollider box = objBlockItem5.AddComponent<BoxCollider>();
        }



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

        {
            RectTransform rctran = objBlock.GetComponent<RectTransform>();
            w = rectMain.width;
            h = rectMain.height / 4;
            rctran.sizeDelta = new Vector2(w, h);
            x = 0;
            y = rectMain.height / 2 - h / 2;
            rctran.anchoredPosition = new Vector2(x, y);
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

    void UpdateBlockItem(GameObject obj)
    {
        Texture2D tex = TextureCache.main.Load(GetBlockItemPic());
        TextureUtil.UpdateSpriteTexture(obj, tex);
        BoxCollider box = obj.GetComponent<BoxCollider>();
        box.size = new Vector3(tex.width / 100f, tex.height / 100f);
    }

    void UpdateWanItem(GameObject obj)
    {
        Texture2D tex = TextureCache.main.Load(GetWanJuanPic());
        TextureUtil.UpdateSpriteTexture(obj, tex);
        // BoxCollider box = obj.GetComponent<BoxCollider>();
        // box.size = new Vector3(tex.width / 100f, tex.height / 100f);
    }
    void UpdateWan(string pic)
    {
        objWan.SetActive(true);
        TextureUtil.UpdateSpriteTexture(objWanFt, pic);
        strImageWan = pic;

    }


    void UpdateItem()
    {
        UpdateBlockItem(objBlockItem0);
        UpdateBlockItem(objBlockItem1);
        UpdateBlockItem(objBlockItem2);
        UpdateBlockItem(objBlockItem3);
        UpdateBlockItem(objBlockItem4);
        UpdateBlockItem(objBlockItem5);


        UpdateWanItem(objWanItem0);
        UpdateWanItem(objWanItem1);
        UpdateWanItem(objWanItem2);
        UpdateWanItem(objWanItem3);
        UpdateWanItem(objWanItem4);
        UpdateWanItem(objWanItem5);
    }
    public void OnDoStep(int idx)
    {

        objHand.SetActive(false);
        UpdateItem();

    }


    public override void ResetStep()
    {
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
        UpdateWan(item.strImageWan);
        LayOut();
    }

    void CheckFinish()
    {
        if ((objBlockItem0.activeSelf == false) &&
        (objBlockItem1.activeSelf == false) &&
        (objBlockItem2.activeSelf == false) &&
        (objBlockItem3.activeSelf == false) &&
        (objBlockItem4.activeSelf == false) &&
        (objBlockItem5.activeSelf == false))
        {
            if (callBackDidUpdateStatus != null)
            {
                callBackDidUpdateStatus(this, STATUS_STEP_END);
            }
        }

    }

    public void OnUITouchEvent(UITouchEvent ev, PointerEventData eventData, int status)
    {
        switch (status)
        {
            case UITouchEvent.STATUS_TOUCH_DOWN:
                {

                }
                break;
            case UITouchEvent.STATUS_TOUCH_MOVE:
                {

                }
                break;
            case UITouchEvent.STATUS_TOUCH_UP:
                {
                    if (objWan.activeSelf)
                    {

                        ev.gameObject.SetActive(false);
                        if (ev.gameObject == objBlockItem0)
                        {
                            objWanItem0.SetActive(true);
                        }
                        if (ev.gameObject == objBlockItem1)
                        {
                            objWanItem1.SetActive(true);
                        }
                        if (ev.gameObject == objBlockItem2)
                        {
                            objWanItem2.SetActive(true);
                        }
                        if (ev.gameObject == objBlockItem3)
                        {
                            objWanItem3.SetActive(true);
                        }
                        if (ev.gameObject == objBlockItem4)
                        {
                            objWanItem4.SetActive(true);
                        }
                        if (ev.gameObject == objBlockItem5)
                        {
                            objWanItem5.SetActive(true);

                        }
                        CheckFinish();
                    }
                }
                break;
        }
    }
}

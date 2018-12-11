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

    public GameObject objBlock;//块
    public GameObject objBlockItem0;
    public GameObject objBlockItem1;
    public GameObject objBlockItem2;
    public GameObject objBlockItem3;
    public GameObject objBlockItem4;
    public GameObject objBlockItem5;

    static public string strImageWan;
    int indexStep = 0;
    int totalStep = 4;

    void Awake()
    {

        strImageWan = IronIceCreamStepBase.GetImageOfWan(0);
        TextureUtil.UpdateSpriteTexture(objHand, AppRes.IMAGE_HAND);

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
        float x, y, z, w, h, scale;
        float ratio = 0.8f;
        LayOutBase();
        RectTransform rctranBlock = objBlock.GetComponent<RectTransform>();
        {

            w = rectMain.width;
            h = rectMain.height / 4;
            rctranBlock.sizeDelta = new Vector2(w, h);
            x = 0;
            y = rectMain.height / 2 - h / 2;
            rctranBlock.anchoredPosition = new Vector2(x, y);
        }
        {
            w = rctranBlock.rect.width / 6;
            z = objHand.transform.position.z;
            y = objBlock.transform.localPosition.y;
            x = objBlock.transform.localPosition.x + rctranBlock.rect.width / 2 - w / 2;
            objHand.transform.localPosition = new Vector3(x, y, z);

            {
                SpriteRenderer rd = objHand.GetComponent<SpriteRenderer>();
                w = rd.sprite.texture.width / 100f;
                h = rd.sprite.texture.height / 100f;
                ratio = 0.5f;
                scale = Common.GetBestFitScale(w, h, w, rctranBlock.rect.height) * ratio;
                objHand.transform.localScale = new Vector3(scale, scale, 1f);
            }
        }


    }
    void UpdateBlockItem(GameObject obj)
    {
        Texture2D tex = TextureCache.main.Load(IronIceCreamStepBase.GetBlockItemPic());
        TextureUtil.UpdateSpriteTexture(obj, tex);
        BoxCollider box = obj.GetComponent<BoxCollider>();
        box.size = new Vector3(tex.width / 100f, tex.height / 100f);
    }

    void UpdateWan(string pic)
    {
        // objWan.SetActive(true);
        uiWanIron.UpdateWan(pic);
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

        IronIceCreamStepBase.uiWanIron.UpdateJuan();
    }


    public void OnDoStep(int idx)
    {

        objHand.SetActive(true);
        ShowHandFlickerAnimation(true);
        UpdateItem();
        LayOut();
    }


    public override void ResetStep()
    {
        //  objWan.SetActive(false);
        IronIceCreamStepBase.uiWanIron.ShowJuan(false);
    }

    public override void UpdateFood(FoodItemInfo info)
    {
        string pic = IronIceCreamStepBase.GetImageOfWan(info.index);
        Debug.Log("UpdateFood pic=" + pic);
        UpdateWan(pic);
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
                    //  if (objWan.activeSelf)
                    {
                        objHand.SetActive(false);
                        ShowHandFlickerAnimation(false);
                        ev.gameObject.SetActive(false);
                        if (ev.gameObject == objBlockItem0)
                        {
                            uiWanIron.ShowJuanItem(true, 0);
                        }
                        if (ev.gameObject == objBlockItem1)
                        {
                            uiWanIron.ShowJuanItem(true, 1);
                        }
                        if (ev.gameObject == objBlockItem2)
                        {
                            uiWanIron.ShowJuanItem(true, 2);
                        }
                        if (ev.gameObject == objBlockItem3)
                        {
                            uiWanIron.ShowJuanItem(true, 3);
                        }
                        if (ev.gameObject == objBlockItem4)
                        {
                            uiWanIron.ShowJuanItem(true, 4);
                        }
                        if (ev.gameObject == objBlockItem5)
                        {
                            uiWanIron.ShowJuanItem(true, 5);

                        }
                        CheckFinish();
                    }
                }
                break;
        }
    }
}

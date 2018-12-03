using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

/* 
1，铲冰淇淋 
*/
public class IronIceCreamStep1 : IronIceCreamStepBase
{
    public GameObject objChanzi;//铲子
    public GameObject objBlock;
    public GameObject objBlock0;
    public GameObject objBlock1;
    public GameObject objBlock2;
    public GameObject objBlock3;
    public GameObject objBlock4;
    public GameObject objBlock5;

    public GameObject objJuan;
    public GameObject objJuan0;
    public GameObject objJuan1;
    public GameObject objJuan2;
    public GameObject objJuan3;
    public GameObject objJuan4;
    public GameObject objJuan5;

    Texture2D texBlock;
    int numBlock = 6;
    int indexBlock;
    bool isTouchItem;
    void Awake()
    {
        texBlock = TextureCache.main.Load("APP/UI/Game/test");
        ResetStep();
        RectTransform rctran = objBlock.GetComponent<RectTransform>();
        rctran.sizeDelta = new Vector2(texBlock.width / 100f, texBlock.height / 100f);
        UITouchEventWithMove ev = objChanzi.AddComponent<UITouchEventWithMove>();
        ev.callBackTouch = OnUITouchEvent;
        BoxCollider box = objChanzi.AddComponent<BoxCollider>();
        box.size = objChanzi.GetComponent<SpriteRenderer>().bounds.size;
        indexBlock = numBlock - 1;
    }
    void Start()
    {
        UpdateItem();
        LayOut();
    }
    public override void LayOut()
    {
        float x, y, w, h;
        LayOutBase();
    }

    public override void ResetStep()
    {
        indexBlock = numBlock - 1;
        isTouchItem = false;
    }
    public override void OnUITopFoodItemDidClick(UITopFoodItem item)
    {

    }
    //冰淇凌卷
    string GetWanJuanPic()
    {
        //0-3
        return GameIronIceCream.IMAGE_DIR_ROOT_SingleColor + "/" + GameIronIceCream.indexFood + "-3";
    }
    void UpdateJuanItem(GameObject obj)
    {
        Texture2D tex = TextureCache.main.Load(GetWanJuanPic());
        TextureUtil.UpdateSpriteTexture(obj, tex);
    }
    void UpdateBlockItem(GameObject obj, int idx)
    {
        BlockItemChan it = obj.GetComponent<BlockItemChan>();
        if (it != null)
        {
            it.UpdateSize((texBlock.width / 100f) / numBlock, (texBlock.height / 100f));
            it.indexCol = idx;
            it.row = 1;
            it.col = numBlock;
            it.UpdateTexture(texBlock);
            //it.UpdatePercent(50);
        }
    }
    void UpdateItem()
    {
        UpdateJuanItem(objJuan0);
        UpdateJuanItem(objJuan1);
        UpdateJuanItem(objJuan2);
        UpdateJuanItem(objJuan3);
        UpdateJuanItem(objJuan4);
        UpdateJuanItem(objJuan5);

        UpdateBlockItem(objBlock0, 0);
        UpdateBlockItem(objBlock1, 1);
        UpdateBlockItem(objBlock2, 2);
        UpdateBlockItem(objBlock3, 3);
        UpdateBlockItem(objBlock4, 4);
        UpdateBlockItem(objBlock5, 5);

    }

    int GetPercent(Vector3 pos)
    {
        int percent = 0;
        Vector3 poslocal = objBlock.transform.InverseTransformPoint(pos);
        RectTransform rctran = objBlock.GetComponent<RectTransform>();
        float h_block = rctran.rect.height * objBlock.transform.localScale.y;
        percent = (int)(100 * (h_block / 2 - poslocal.y) / h_block);
        if (percent < 0)
        {
            percent = 0;
        }
        if (percent > 100)
        {
            percent = 100;
        }

        return percent;
    }
    GameObject GetBlock(int idx)
    {
        GameObject obj = objBlock0;
        if (idx == 0)
        {
            obj = objBlock0;
        }
        if (idx == 1)
        {
            obj = objBlock1;
        }
        if (idx == 2)
        {
            obj = objBlock2;
        }
        if (idx == 3)
        {
            obj = objBlock3;
        }
        if (idx == 4)
        {
            obj = objBlock4;
        }
        if (idx == 5)
        {
            obj = objBlock5;
        }
        return obj;
    }

    public void OnUITouchEvent(UITouchEvent ev, PointerEventData eventData, int status)
    {
        Vector3 pos = Common.GetInputPositionWorld(mainCam);
        Vector3 poslocal = this.transform.InverseTransformPoint(pos);
        GameObject objItem = GetBlock(indexBlock);
        float x, y, w, h;
        switch (status)
        {
            case UITouchEvent.STATUS_TOUCH_DOWN:
                {
                    isTouchItem = false;
                    Renderer rd = objItem.GetComponent<Renderer>();
                    w = rd.bounds.size.x;
                    h = rd.bounds.size.y;
                    x = rd.bounds.center.x - w / 2;
                    y = rd.bounds.center.y - h / 2;
                    Rect rc = new Rect(x, y, w, h);
                    if (rc.Contains(poslocal))
                    {
                        isTouchItem = true;
                    }
                }
                break;
            case UITouchEvent.STATUS_TOUCH_MOVE:
                {
                    poslocal.z = objChanzi.transform.localPosition.z;
                    objChanzi.transform.localPosition = poslocal;
                    if (isTouchItem)
                    {
                        BlockItemChan it = objItem.GetComponent<BlockItemChan>();
                        it.UpdatePercent(GetPercent(pos));
                    }

                }
                break;
            case UITouchEvent.STATUS_TOUCH_UP:
                if (isTouchItem)
                {
                    BlockItemChan it = objItem.GetComponent<BlockItemChan>();
                    if (it.percent <= 0)
                    {
                        indexBlock--;
                        if (indexBlock < 0)
                        {
                            indexBlock = 0;
                            if (callBackDidUpdateStatus != null)
                            {
                                callBackDidUpdateStatus(this, STATUS_STEP_END);
                            }
                        }
                    }
                }
                break;

        }
    }
}

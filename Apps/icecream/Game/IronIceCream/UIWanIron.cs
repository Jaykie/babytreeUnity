using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
/*铁板冰淇淋 碗
*/
public class UIWanIron : UIView
{
    public List<TopFoodItemInfo> listItem;

    Vector3 posLocalTouchDown;
    Vector3 posInputTouchDown;
    void Awake()
    {
        listItem = new List<TopFoodItemInfo>();



    }
    public void OnUITouchEvent(UITouchEvent ev, PointerEventData eventData, int status)
    {
        Vector3 posworld = Common.GetInputPositionWorld(mainCam);
        Vector3 poslocal = this.transform.InverseTransformPoint(posworld);
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
        obj.transform.SetParent(this.transform);
        obj.transform.localPosition = new Vector3(0, 0, -1);
        obj.transform.localScale = new Vector3(1, 1, 1);
        info.obj = obj;
        info.pt = Vector3.zero;

        listItem.Add(info);
    }
}

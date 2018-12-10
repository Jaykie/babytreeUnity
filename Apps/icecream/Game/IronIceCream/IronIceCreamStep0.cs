using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
/*铁板冰淇淋步骤：
0，炒冰淇淋
*/
public class IronIceCreamStep0 : IronIceCreamStepBase
{
    public const int CHANZI_STATUS_NONE = 0;
    public const int CHANZI_STATUS_START = 1;
    public const int CHANZI_STATUS_MOVE = 2;
    public const int CHANZI_STATUS_END = 3;
    public GameObject objChanzi;//铲子
    public GameObject objIcecreemBlock;//冰淇凌块
    public GameObject objIcecreemPiece;//冰淇凌片
    public GameObject objIcecreemLiquid;//冰淇凌液体倾倒动画
    public GameObject objHand;//操作提示的手 
    int indexFood = 0;
    int indexStep = 0;
    int totalStep = 4;

    Tween tweenAlpha;
    int chanziStatus;
    float scaleBlockNormal;
    void Awake()
    {
        TextureUtil.UpdateSpriteTexture(objHand, AppRes.IMAGE_HAND);
        ResetStep();
        objHand.SetActive(false);
        objChanzi.SetActive(true);
        objIcecreemPiece.SetActive(false);

        UITouchEventWithMove ev = objChanzi.AddComponent<UITouchEventWithMove>();
        ev.callBackTouch = OnUITouchEvent;
        BoxCollider box = objChanzi.AddComponent<BoxCollider>();
        box.size = objChanzi.GetComponent<SpriteRenderer>().bounds.size;

    }
    void Start()
    {

        LayOut();
    }

    public override void LayOut()
    {
        float x, y, z, w, h;
        float scale = 0;
        LayOutBase();
        RectTransform rectMainWorld = AppSceneBase.main.GetRectMainWorld();
        float ratio = 0.8f;
        SpriteRenderer rdpanzi = objPanzi.GetComponent<SpriteRenderer>();
        {
            SpriteRenderer rd = objChanzi.GetComponent<SpriteRenderer>();
            w = rd.sprite.texture.width / 100f;
            h = rd.sprite.texture.height / 100f;



            z = objChanzi.transform.localPosition.z;
            float w_rect = (rectMainWorld.rect.width - rdpanzi.bounds.size.x) / 2;
            x = -rdpanzi.bounds.size.x / 2 - w_rect / 2;
            y = 0;
            objChanzi.transform.localPosition = new Vector3(x, y, z);

            scale = Common.GetBestFitScale(w, h, w_rect, rdpanzi.bounds.size.y) * ratio;
            objChanzi.transform.localScale = new Vector3(scale, scale, 1f);
        }

        {
            z = objHand.transform.localPosition.z;
            Vector3 pos = objChanzi.transform.localPosition;
            SpriteRenderer rd = objChanzi.GetComponent<SpriteRenderer>();
            x = pos.x + rd.bounds.size.x / 2;
            y = pos.y + rd.bounds.size.y / 2;
            objHand.transform.localPosition = new Vector3(x, y, z);

        }

        {
            SpriteRenderer rd_chanzi = objChanzi.GetComponent<SpriteRenderer>();
            SpriteRenderer rd = objHand.GetComponent<SpriteRenderer>();
            w = rd.sprite.texture.width / 100f;
            h = rd.sprite.texture.height / 100f;
            scale = Common.GetBestFitScale(w, h, rd_chanzi.bounds.size.x, rd_chanzi.bounds.size.y) * ratio;
            objHand.transform.localScale = new Vector3(scale, scale, 1f);
        }
        {
            SpriteRenderer rd = objIcecreemBlock.GetComponent<SpriteRenderer>();
            if (rd.sprite != null && rd.sprite.texture != null)
            {
                w = rd.sprite.texture.width / 100f;
                h = rd.sprite.texture.height / 100f;
                scale = Common.GetBestFitScale(w, h, rdpanzi.bounds.size.x, rdpanzi.bounds.size.y) * ratio;
                scaleBlockNormal = scale;
                objIcecreemBlock.transform.localScale = new Vector3(scale, scale, 1f);
            }

        }
        {
            SpriteRenderer rd = objIcecreemPiece.GetComponent<SpriteRenderer>();
            if (rd.sprite != null && rd.sprite.texture != null)
            {
                w = rd.sprite.texture.width / 100f;
                h = rd.sprite.texture.height / 100f;
                scale = Common.GetBestFitScale(w, h, rdpanzi.bounds.size.x, rdpanzi.bounds.size.y) * ratio;
                objIcecreemPiece.transform.localScale = new Vector3(scale, scale, 1f);
            }
        }

    }

   

    // 炒冰淇淋
    public void OnDoStep(int idx)
    {
        indexFood = idx;
        GameIronIceCream.status = STATUS_STEP_START;
        GameIronIceCream.indexFood = idx;
        objHand.SetActive(false);
        objIcecreemLiquid.SetActive(true);
        objIcecreemBlock.SetActive(true);
        //objChanzi.SetActive(false);
        TextureUtil.UpdateSpriteTexture(objIcecreemBlock, IronIceCreamStepBase.GetImageOfIcecreemLiquid(indexFood));
        TextureUtil.UpdateSpriteTexture(objIcecreemPiece, IronIceCreamStepBase.GetImageOfIcecreemPiece(indexFood));
        ActionImage acImage = objIcecreemLiquid.AddComponent<ActionImage>();
        acImage.duration = 1f;
        acImage.isLoop = false;
        for (int i = 0; i < 5; i++)
        {
            string pic = GameIronIceCream.IMAGE_DIR_ROOT_CupLiquid + "/" + indexFood.ToString() + "/" + (i + 1).ToString();
            acImage.AddPic(pic);
        }
        acImage.Run();


        LayOut();

        float scale0 = 0.1f;
        float scale1 = scaleBlockNormal;
        objIcecreemBlock.transform.localScale = new Vector3(scale0, scale0, 1f);
        objIcecreemBlock.transform.DOScale(new Vector3(scale1, scale1, 1f), acImage.duration).OnComplete(() =>
        {
            //倒冰淇凌夜结束
            objIcecreemLiquid.SetActive(false);
            objChanzi.SetActive(true);
            ShowHand(true, true);
            if (callBackDidUpdateStatus != null)
            {
                callBackDidUpdateStatus(this, STATUS_Liquid_Finish);
            }
            chanziStatus = CHANZI_STATUS_START;
        });

    }
    public override void ResetStep()
    {
        GameIronIceCream.status = STATUS_STEP_NONE;
        objIcecreemBlock.SetActive(false);
        objIcecreemPiece.SetActive(false);
        chanziStatus = CHANZI_STATUS_NONE;
    }

    public override void UpdateFood(FoodItemInfo info)
    {
        OnDoStep(info.index);
    }

    //淇淋液变淇淋片
    void MakeIceCreamBlock()
    {
        float duration = 5f;
        {
            SpriteRenderer rd = objIcecreemBlock.GetComponent<SpriteRenderer>();
            Color cr = rd.color;
            cr.a = 1f;
            rd.color = cr;
            DOTween.ToAlpha(() => rd.color, x => rd.color = x, 0f, duration);
        }
        {
            SpriteRenderer rd = objIcecreemPiece.GetComponent<SpriteRenderer>();
            Color cr = rd.color;
            cr.a = 0f;
            rd.color = cr;
            objIcecreemPiece.SetActive(true);
            //objIcecreemBlock.SetActive(false);
            DOTween.ToAlpha(() => rd.color, x => rd.color = x, 1f, duration).OnComplete(() =>
        {
            GameIronIceCream.status = STATUS_STEP_END;
            //制作淇淋片结束
            if (callBackDidUpdateStatus != null)
            {
                callBackDidUpdateStatus(this, STATUS_STEP_END);
            }

        });

        }

        chanziStatus = CHANZI_STATUS_NONE;
        objHand.SetActive(false);
        objChanzi.SetActive(false);


    }
    public void ShowHand(bool isShow, bool isAnimation)
    {
        objHand.SetActive(isShow);
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

            //闪烁动画
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

    public void OnUITouchEvent(UITouchEvent ev, PointerEventData eventData, int status)
    {
        float x, y, z, w, h;

        switch (status)
        {
            case UITouchEvent.STATUS_TOUCH_DOWN:
                {
                    //铲子移动到盘子上 竖向滑动指导动作
                    if (chanziStatus == CHANZI_STATUS_START)
                    {

                        z = objChanzi.transform.localPosition.z;
                        x = 0;
                        y = 0;
                        objChanzi.transform.localPosition = new Vector3(x, y, z);

                        z = objHand.transform.localPosition.z;
                        Vector3 pos = objChanzi.transform.localPosition;
                        SpriteRenderer rd = objChanzi.GetComponent<SpriteRenderer>();
                        x = pos.x;
                        y = pos.y + rd.bounds.size.y / 2;
                        objHand.transform.localPosition = new Vector3(x, y, z);
                        y = pos.y - rd.bounds.size.y / 2;
                        Vector3 posEnd = new Vector3(x, y, z);
                        objHand.transform.DOLocalMove(posEnd, 2f).SetLoops(-1, LoopType.Restart);
                        tweenAlpha.Pause();

                        rd = objHand.GetComponent<SpriteRenderer>();
                        rd.color = Color.white;
                        chanziStatus = CHANZI_STATUS_MOVE;
                    }
                }
                break;
            case UITouchEvent.STATUS_TOUCH_MOVE:
                {
                    Vector3 pos = Common.GetInputPositionWorld(mainCam);
                    // Vector3 poslocal = this.transform.InverseTransformPoint(pos);
                    // poslocal.z = objChanzi.transform.localPosition.z;
                    // objChanzi.transform.localPosition = poslocal;
                    if (chanziStatus == CHANZI_STATUS_MOVE)
                    {
                        chanziStatus = CHANZI_STATUS_END;
                    }
                }
                break;
            case UITouchEvent.STATUS_TOUCH_UP:
                if (chanziStatus == CHANZI_STATUS_END)
                {
                    MakeIceCreamBlock();

                }
                break;

        }
    }
}

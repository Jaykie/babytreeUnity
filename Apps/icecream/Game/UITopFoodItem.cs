using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public delegate void OnUITopFoodItemDidClickDelegate(UITopFoodItem item);


public class UITopFoodItem : UIView
{

    public enum Type
    {
        CUP,//杯子
        WAN,//碗
        FOOD,//顶料
        SUB_FOOD,//顶料子项 popselect bar
    }
    public const string PREFAB_TopFoodItem = "App/Prefab/Game/UITopFoodItem";
    public Image imageCup;
    public Image imageFood;
    public Image imageLock;
    public GameObject objHand;
    public Image imageHand;
    public int index;
    public float width;
    public float height;
    public bool enableLock = true;

    string strImageCup = "App/UI/Game/TopFoodBar/Cup";

    public const string IMAGE_WAN_BG = "App/UI/Game/TopFoodBar/Wan/WanBg";



    public string strImageWan;
    public string strPic;
    UITouchEvent uITouchEvent;
    Tween tweenAlpha;

    public Type type;
    public OnUITopFoodItemDidClickDelegate callBackDidClick { get; set; }
    private void Awake()
    {
        uITouchEvent = this.gameObject.AddComponent<UITouchEvent>();
        uITouchEvent.callBackTouch = OnUITouchEvent;
        TextureUtil.UpdateImageTexture(imageHand, AppRes.IMAGE_HAND, true);
        imageHand.gameObject.SetActive(false);
    }
    public void UpdateItem()
    {
        float x = 0, y = 0, w, h;
        {
            string pic = strImageCup;
            TextureUtil.UpdateImageTexture(imageCup, pic, true);
            w = imageCup.sprite.texture.width;
            h = imageCup.sprite.texture.height;
            float scale = Common.GetBestFitScale(w, h, width, height);
            imageCup.transform.localScale = new Vector3(scale, scale, 1f);

        }

        {
            string pic = "";
            switch (type)
            {
                case Type.CUP:
                    pic = IronIceCreamStepBase.GetImageOfCupFood(index);
                    break;
                case Type.WAN:
                    imageCup.gameObject.SetActive(false);
                    pic = IronIceCreamStepBase.GetImageOfWan(index);
                    strImageWan = pic;
                    break;
                case Type.FOOD:
                    imageCup.gameObject.SetActive(false);
                    pic = IronIceCreamStepBase.GetImageOfTopFood(index);
                    break;
                case Type.SUB_FOOD:
                    imageCup.gameObject.SetActive(false);
                    pic = IronIceCreamStepBase.GetImageOfTopFoodSubFood(index);
                    break;
            }
            strPic = pic;
            TextureUtil.UpdateImageTexture(imageFood, pic, true);
            w = imageFood.sprite.texture.width;
            h = imageFood.sprite.texture.height;
            float scale = Common.GetBestFitScale(w, h, width, height);
            imageFood.transform.localScale = new Vector3(scale, scale, 1f);
        }

        {
            RectTransform rctranCup = imageCup.GetComponent<RectTransform>();
            string pic = GameIronIceCream.IMAGE_LOCK;
            TextureUtil.UpdateImageTexture(imageLock, pic, true);
            w = imageLock.sprite.texture.width;
            h = imageLock.sprite.texture.height;
            float w_cup = rctranCup.rect.width * imageCup.transform.localScale.x;
            float h_cup = rctranCup.rect.height * imageCup.transform.localScale.y;
            float w_rect = w_cup / 2;
            float h_rect = h_cup / 2;

            float scale = Common.GetBestFitScale(w, h, w_rect, h_rect);
            imageLock.transform.localScale = new Vector3(scale, scale, 1f);

            RectTransform rctran = imageLock.GetComponent<RectTransform>();
            x = w_cup / 2 - w_rect / 2;
            y = -h_cup / 2 + h_rect / 2;
            rctran.anchoredPosition = new Vector2(x, y);
            imageLock.gameObject.SetActive(false);
            if ((index % 2 != 0) && enableLock)
            {
                imageLock.gameObject.SetActive(true);
            }
        }

        {
            w = imageHand.sprite.texture.width;
            h = imageHand.sprite.texture.height;
            float scale = Common.GetBestFitScale(w, h, width, height) * 0.7f;
            imageHand.transform.localScale = new Vector3(scale, scale, 1f);
            x = -width / 2;
            y = height / 2;
            RectTransform rctran = imageHand.GetComponent<RectTransform>();
            rctran.anchoredPosition = new Vector2(x, y);
        }
    }

    public void ShowHand(bool isShow, bool isAnimation)
    {
        imageHand.gameObject.SetActive(isShow);
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
                tweenAlpha = DOTween.ToAlpha(() => imageHand.color, x => imageHand.color = x, 0f, duration).SetLoops(-1, LoopType.Yoyo);
            }
            tweenAlpha.Play();

        }
        else
        {

            if (tweenAlpha != null)
            {
                tweenAlpha.Pause();
            }
            //imageHand.color = Color.white;
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
                    if (type == Type.FOOD)
                    {
                        UIPopSelectBar.indexFoodSort = index;
                        UIPopSelectBar.countFoodSort = IronIceCreamStepBase.countTopFoodSort[index];
                    }

                    if (callBackDidClick != null)
                    {
                        callBackDidClick(this);
                    }
                }
                break;
        }
    }
}

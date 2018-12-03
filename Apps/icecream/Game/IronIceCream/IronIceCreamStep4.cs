using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

/*  
4，吃冰淇凌
*/
public class IronIceCreamStep4 : IronIceCreamStepBase
{
    public const string IMAGE_EatErase = "App/UI/Game/EatErase";
    public GameObject objWan;//碗 
    public GameObject objWanFt;//碗 
    public GameObject objWanBg;//碗bg Wan_bg
    public GameObject objErase;//碗 
    public GameObject objWanItemRoot;
    public GameObject objWanItem0;
    public GameObject objWanItem1;
    public GameObject objWanItem2;
    public GameObject objWanItem3;
    public GameObject objWanItem4;
    public GameObject objWanItem5;
    public MeshTexture meshTex;
    RenderTexture rtMain;
    public Camera camEat;


    public GameObject objHand;//操作提示的手

    Material matEat;
    Material matErase;
    int indexStep = 0;
    int totalStep = 4;
    int indexLayer = 8;//Layer8
    Texture2D texBrush;
    void Awake()
    {
        texBrush = TextureCache.main.Load("App/UI/Brush/brush_dot");
        rtMain = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        camEat.targetTexture = rtMain;

        matEat = new Material(Shader.Find("Custom/IceCreamEat"));
        matErase = new Material(Shader.Find("Custom/Erase"));
        TextureUtil.UpdateSpriteTexture(objHand, AppRes.IMAGE_HAND);
        TextureUtil.UpdateSpriteTexture(objWanBg, UITopFoodItem.IMAGE_WAN_BG);
        TextureUtil.UpdateSpriteTexture(objErase, texBrush);
        ResetStep();

        SpriteRenderer rd = objErase.GetComponent<SpriteRenderer>();
        rd.material = matErase;

        int layer = indexLayer;
        objErase.layer = layer;
        mainCam.cullingMask &= ~(1 << layer); // 关闭层x
                                              // mainCam.cullingMask |= (1 << layer);  // 打开层x

        objErase.SetActive(false);

    }
    void Start()
    {

        OnDoStep(GameIronIceCream.indexFood);

        meshTex.EnableTouch(true);
        meshTex.UpdateMaterial(matEat);
        meshTex.UpdateTexture(rtMain);
        Vector2 worldsize = Common.GetWorldSize(mainCam);
        meshTex.UpdateSize(worldsize.x, worldsize.y);
        UITouchEventWithMove ev = meshTex.gameObject.AddComponent<UITouchEventWithMove>();
        ev.callBackTouch = OnUITouchEvent;

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
    void UpdateWan(string pic)
    {
        objWan.SetActive(true);
        TextureUtil.UpdateSpriteTexture(objWanFt, pic);


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
    public void OnDoStep(int idx)
    {

        objHand.SetActive(false);
        UpdateItem();

    }


    public override void ResetStep()
    {
        int index = 0;
        string pic = IronIceCreamStepBase.GetImageOfWan(index);
        UpdateWan(pic);

    }

    public override void OnUITopFoodItemDidClick(UITopFoodItem item)
    {
        UpdateWan(item.strImageWan);
        LayOut();
    }

    void StartEat()
    {
        objErase.SetActive(true);
        objWanItemRoot.SetActive(false);

    }
    public void OnUITouchEvent(UITouchEvent ev, PointerEventData eventData, int status)
    {
        Vector3 posworld = Common.GetInputPositionWorld(mainCam);
        Vector3 poslocal = this.transform.InverseTransformPoint(posworld);
        switch (status)
        {
            case UITouchEvent.STATUS_TOUCH_DOWN:
                {
                    StartEat();
                    poslocal.z = objErase.transform.localPosition.z;
                    objErase.transform.localPosition = poslocal;
                }
                break;
            case UITouchEvent.STATUS_TOUCH_MOVE:
                {
                    poslocal.z = objErase.transform.localPosition.z;
                    objErase.transform.localPosition = poslocal;
                }
                break;
            case UITouchEvent.STATUS_TOUCH_UP:
                {

                    poslocal.z = objErase.transform.localPosition.z;
                    objErase.transform.localPosition = poslocal;

                }
                break;
        }
    }
}
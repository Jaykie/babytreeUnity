using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/*铁板冰淇淋 碗
*/
public class UIWanIron : UIView
{
    public const string IMAGE_EatErase = "App/UI/Game/EatErase";
    public List<TopFoodItemInfo> listItem;
    public GameObject objWanFt;//碗 
    public GameObject objWanBg;//碗bg Wan_bg
    public GameObject objWanItemRoot;//顶料
    public GameObject objJuan;//冰淇凌券
    public GameObject objJuanitem0;
    public GameObject objJuanitem1;
    public GameObject objJuanitem2;
    public GameObject objJuanitem3;
    public GameObject objJuanitem4;
    public GameObject objJuanitem5;
    public GameObject objErase;//碗  

    GameObject objItemSelect;//选中的顶料

    public MeshTexture meshTex;
    Rect rectMain;//local 
    RenderTexture rtMain;
    public Camera camWan;
    Vector3 posLocalTouchDown;
    Vector3 posInputTouchDown;
    Material matErase;

    Material matEat;
    Texture2D texBrush;

    GameObject[] listJuan = new GameObject[6];
    int indexLayer = 8;//Layer8

    int indexStep = 0;
    void Awake()
    {
        listItem = new List<TopFoodItemInfo>();
        GameObject[] listJuanTmp = { objJuanitem0, objJuanitem1, objJuanitem2, objJuanitem3, objJuanitem4, objJuanitem5 };
        for (int i = 0; i < listJuanTmp.Length; i++)
        {
            listJuan[i] = listJuanTmp[i];
        }
        texBrush = TextureCache.main.Load("App/UI/Brush/brush_dot");
        TextureUtil.UpdateSpriteTexture(objWanBg, UITopFoodItem.IMAGE_WAN_BG);
        TextureUtil.UpdateSpriteTexture(objErase, texBrush);
        matErase = new Material(Shader.Find("Custom/Erase"));
        matEat = new Material(Shader.Find("Custom/IceCreamEat"));
        rtMain = new RenderTexture(Screen.width, Screen.height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
        camWan.targetTexture = rtMain;
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
        meshTex.EnableTouch(false);
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
        float x, y, z, w, h;
        float scale = 0;
        float ratio = 0.8f;



        {
            SpriteRenderer rd = objWanBg.GetComponent<SpriteRenderer>();
            if (rd.sprite != null)
            {
                w = rd.sprite.texture.width / 100f;
                h = rd.sprite.texture.height / 100f;
                scale = Common.GetBestFitScale(w, h, rectMain.width, rectMain.height) * ratio;
                objWanBg.transform.localScale = new Vector3(scale, scale, 1f);
                z = objWanBg.transform.localPosition.z;
                objWanBg.transform.localPosition = new Vector3(rectMain.center.x, rectMain.center.y, z);
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
                z = objWanFt.transform.localPosition.z;
                objWanFt.transform.localPosition = new Vector3(rectMain.center.x, rectMain.center.y, z);
            }
        }
        {
            SpriteRenderer render = objErase.GetComponent<SpriteRenderer>();
            if (render.sprite && render.sprite.texture)
            {
                w = render.sprite.texture.width / 100f;
                h = render.sprite.texture.height / 100f;
                if ((w != 0) && (h != 0))
                {
                    scale = (rectMain.width / 10) / w;
                    objErase.transform.localScale = new Vector3(scale, scale, 1f);
                }


            }

        }



    }

    public void UpdateRect(Rect rc)
    {
        rectMain = rc;
        LayOut();


    }

    public void UpdateStep(int idx)
    {
        indexStep = idx;
        meshTex.EnableTouch(false);
        if (indexStep == GameIronIceCream.INDEX_STEP_CHI)
        {
            meshTex.EnableTouch(true);

            //显示到meshtex
            foreach (TopFoodItemInfo info in listItem)
            {
                info.obj.layer = indexLayer;
            }
        }
    }

    public void OnUITouchEvent(UITouchEvent ev, PointerEventData eventData, int status)
    {
        if (indexStep == GameIronIceCream.INDEX_STEP_ZHUANG)
        {
            OnUITouchEventTopFood(ev, eventData, status);
        }
        if (indexStep == GameIronIceCream.INDEX_STEP_CHI)
        {
            OnUITouchEventEat(ev, eventData, status);

        }
    }
    public void OnUITouchEventTopFood(UITouchEvent ev, PointerEventData eventData, int status)
    {
        Vector3 posworld = Common.GetInputPositionWorld(mainCam);
        Vector3 poslocal = this.transform.InverseTransformPoint(posworld);
        poslocal.z = ev.gameObject.transform.localPosition.z;

        switch (status)
        {
            case UITouchEvent.STATUS_TOUCH_DOWN:
                {
                    objItemSelect = ev.gameObject;
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

    public void OnUITouchEventEat(UITouchEvent ev, PointerEventData eventData, int status)
    {
        Vector3 posworld = Common.GetInputPositionWorld(mainCam);
        Vector3 poslocal = this.transform.InverseTransformPoint(posworld);
        poslocal.z = objErase.transform.localPosition.z;

        switch (status)
        {
            case UITouchEvent.STATUS_TOUCH_DOWN:
                {
                    StartEat();

                }
                break;
            case UITouchEvent.STATUS_TOUCH_MOVE:
                {

                }
                break;
            case UITouchEvent.STATUS_TOUCH_UP:
                {

                }
                break;
        }

        objErase.transform.localPosition = poslocal;
    }

    //添加顶料
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
        obj.transform.SetParent(objWanItemRoot.transform);

        SpriteRenderer rdbg = objWanBg.GetComponent<SpriteRenderer>();
        float scale = 0f;
        float ratio = 0.25f;
        scale = Common.GetBestFitScale(tex.width / 100f, tex.height / 100f, rdbg.bounds.size.x * ratio, rdbg.bounds.size.y * ratio);
        obj.transform.localScale = new Vector3(scale, scale, 1);

        info.obj = obj;
        info.pt = Vector3.zero;
        objItemSelect = obj;
        listItem.Add(info);

        obj.transform.localPosition = new Vector3(0, 0, -1 * listItem.Count);
    }

    //删除选中顶料
    public void OnDeleteTopFood()
    {
        if (objItemSelect != null)
        {
            TopFoodItemInfo infoSel = null;
            foreach (TopFoodItemInfo info in listItem)
            {
                if (info.obj == objItemSelect)
                {
                    infoSel = info;
                    break;
                }
            }
            if (infoSel != null)
            {
                listItem.Remove(infoSel);
            }
            DestroyImmediate(objItemSelect);
            objItemSelect = null;
        }
    }
    //逆时针旋转选中顶料
    public void OnRotationAddTopFood()
    {
        float step_angle = 15f;
        if (objItemSelect != null)
        {
            float angle_z = objItemSelect.transform.localRotation.eulerAngles.z;
            objItemSelect.transform.localRotation = Quaternion.Euler(0, 0, angle_z + step_angle);
        }
    }

    //顺时针旋转选中顶料
    public void OnRotationMinusTopFood()
    {
        float step_angle = 15f;
        if (objItemSelect != null)
        {
            float angle_z = objItemSelect.transform.localRotation.eulerAngles.z;
            objItemSelect.transform.localRotation = Quaternion.Euler(0, 0, angle_z - step_angle);
        }
    }

    //缩小选中顶料
    public void OnScaleMinusTopFood()
    {
        float step = 0.1f;
        if (objItemSelect != null)
        {
            float scale = objItemSelect.transform.localScale.x - step;
            if (scale <= 0)
            {
                scale = step;
            }
            objItemSelect.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
    //放大选中顶料
    public void OnScaleAddTopFood()
    {
        float step = 0.1f;
        float scale_max = 3f;
        if (objItemSelect != null)
        {
            float scale = objItemSelect.transform.localScale.x + step;
            if (scale > scale_max)
            {
                scale = scale_max;
            }
            objItemSelect.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }
    void UpdateJuanItem(GameObject obj, float w, float h)
    {
        Texture2D tex = TextureCache.main.Load(IronIceCreamStepBase.GetWanJuanPic());
        TextureUtil.UpdateSpriteTexture(obj, tex);
        // BoxCollider box = obj.GetComponent<BoxCollider>();
        // box.size = new Vector3(tex.width / 100f, tex.height / 100f);

        {
            float ratio = 1.5f;
            float w_tex = tex.width / 100f;
            float h_tex = tex.height / 100f;
            float scale = Common.GetMaxFitScale(w_tex, h_tex, w, h) * ratio;
            obj.transform.localScale = new Vector3(scale, scale, 1f);
        }
    }

    //放置冰淇凌卷
    public void UpdateJuan()
    {

        SpriteRenderer rd = objWanBg.GetComponent<SpriteRenderer>();
        float x, y, w, h;
        float ratio = 0.5f;
        w = rd.bounds.size.x * ratio;
        h = rd.bounds.size.y * ratio;
        GridLayoutGroup gridLayout = objJuan.GetComponent<GridLayoutGroup>();

        gridLayout.cellSize = new Vector2(w / 3, h / 2);

        RectTransform rctran = objJuan.GetComponent<RectTransform>();
        rctran.sizeDelta = new Vector2(w, h);

        foreach (GameObject obj in listJuan)
        {
            UpdateJuanItem(obj, gridLayout.cellSize.x, gridLayout.cellSize.y);
        }
    }
    public void ShowJuan(bool isShow)
    {
        ShowJuanItem(isShow, 0);
        ShowJuanItem(isShow, 1);
        ShowJuanItem(isShow, 2);
        ShowJuanItem(isShow, 3);
        ShowJuanItem(isShow, 4);
        ShowJuanItem(isShow, 5);

    }
    public void ShowJuanItem(bool isShow, int idx)
    {
        GameObject obj = listJuan[idx];
        obj.SetActive(isShow);
    }

    public void UpdateWan(string pic)
    {
        // objWan.SetActive(true);
        TextureUtil.UpdateSpriteTexture(objWanFt, pic);
        //  strImageWan = pic;
        LayOut();

    }

    public void StartEat()
    {
        objErase.SetActive(true);
        objWanItemRoot.SetActive(false);
    }

}

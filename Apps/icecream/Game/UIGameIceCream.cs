using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LitJson;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
宝宝巴士 宝宝涂色 
http://as.baidu.com/software/23393827.html
http://app.mi.com/details?id=com.sinyee.babybus.paintingIII&ref=search
*/
//ps制作线稿教程：https://www.cnblogs.com/lrxsblog/p/6902377.html

public class ColorItemInfo : ItemInfo
{
    public List<Color> listColor;
    public string name;
    public string picmask;
    public string colorJson;
    public Vector2 pt;
    public Color colorOrigin;//填充前原来颜色
    public Color colorFill;//当前填充颜色
    public Color colorMask;
    public Color32 color32Fill;
    public string fileSave;
    public string fileSaveWord;
    public string addtime;
    public string date;
    public Rect rectFill;
}
public class UIGameIceCream : UIGameBase
{
    public const string STR_KEYNAME_VIEWALERT_SAVE_FINISH = "STR_KEYNAME_VIEWALERT_SAVE_FINISH";

    UIGameTopBar uiGameTopBarPrefab;
    UIGameTopBar uiGameTopBar;



    // bool isFirstUseStraw
    // {
    //     get
    //     {
    //         if (Common.noad)
    //         {
    //             return false;
    //         }
    //         //   return Common.Int2Bool(PlayerPrefs.GetInt(KEY_STR_FIRST_USE_STRAW, Common.Bool2Int(true)));
    //     }
    //     set
    //     {

    //         //  PlayerPrefs.SetInt(KEY_STR_FIRST_USE_STRAW, Common.Bool2Int(value));
    //     }
    // }

    void Awake()
    {

    }
    // Use this for initialization
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {


    }


    public void LoadPrefabBase()
    {

        {
            GameObject obj = (GameObject)Resources.Load("App/Prefab/Game/UIGameTopBar");
            if (obj != null)
            {
                uiGameTopBarPrefab = obj.GetComponent<UIGameTopBar>();
                uiGameTopBar = (UIGameTopBar)GameObject.Instantiate(uiGameTopBarPrefab);

                RectTransform rctranPrefab = uiGameTopBarPrefab.transform as RectTransform;
                //  AppSceneBase.main.AddObjToMainCanvas(uiGameTopBar.gameObject);
                uiGameTopBar.transform.parent = this.transform;
                RectTransform rctran = uiGameTopBar.transform as RectTransform;
                // 初始化rect
                rctran.offsetMin = rctranPrefab.offsetMin;
                rctran.offsetMax = rctranPrefab.offsetMax;

            }

        }



    }

    public void LayOutBase()
    {
        float x = 0, y = 0, z = 0, w = 0, h = 0;
        float scalex = 0, scaley = 0, scale = 0;
        Vector2 sizeCanvas = AppSceneBase.main.sizeCanvas;


    }


    ColorItemInfo GetItemInfo()
    {
        int idx = GameManager.gameLevel;
        if (listGuanka == null)
        {
            return null;
        }
        if (idx >= listGuanka.Count)
        {
            return null;
        }
        ColorItemInfo info = listGuanka[idx] as ColorItemInfo;
        return info;
    }

    public override int GetGuankaTotal()
    {
        ParseGuanka();
        if (listGuanka != null)
        {
            return listGuanka.Count;
        }
        return 0;
    }



    public void ShowFirstUseAlert()
    {

        // string title = Language.main.GetString("STR_UIVIEWALERT_TITLE_FIRST_USE_FUNCTION");
        // string msg = Language.main.GetString("STR_UIVIEWALERT_MSG_FIRST_USE_FUNCTION");
        // string yes = Language.main.GetString("STR_UIVIEWALERT_YES_FIRST_USE_FUNCTION");
        // string no = "no";
        // ViewAlertManager.main.ShowFull(title, msg, yes, no, false, STR_KEYNAME_VIEWALERT_FIRST_USE_FUNCTION, OnUIViewAlertFinished);

    }

    void DoBtnBack()
    {
        base.OnClickBtnBack();
        ShowAdInsert(1);
    }

    public override void OnClickBtnBack()
    {


        DoBtnBack();
    }

    void OnUIViewAlertFinished(UIViewAlert alert, bool isYes)

    {

        // if (STR_KEYNAME_VIEWALERT_SAVE == alert.keyName)
        // {
        //     if (isYes)
        //     {
        //         DoBtnSave();
        //     }
        // }

    }

    public void DoClickBtnStraw()
    {

    }

    void DoClickBtnStrawAlert()
    {
        // if (AppVersion.appCheckHasFinished && !Application.isEditor)
        // {
        //     if (isFirstUseStraw)
        //     {
        //         //show ad video
        //         AdKitCommon.main.ShowAdVideo();
        //     }
        //     else
        //     {
        //         DoClickBtnStraw();
        //     }
        // }
        // else
        // {
        //     DoClickBtnStraw();
        // }
    }

    public void OnClickBtnStraw()
    {


    }



    public override void AdVideoDidFail(string str)
    {
        ShowAdVideoFailAlert();
    }

    public override void AdVideoDidStart(string str)
    {

    }
    public override void AdVideoDidFinish(string str)
    {

    }
}


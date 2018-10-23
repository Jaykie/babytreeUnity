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
        LoadPrefab();

        ParseGuanka();
        AppSceneBase.main.UpdateWorldBg(AppRes.IMAGE_GAME_BG);




        //ShowFPS();
    }
    // Use this for initialization
    void Start()
    {


        UpdateGuankaLevel(GameManager.gameLevel);
    }
    // Update is called once per frame
    void Update()
    {


    }


    void LoadPrefab()
    {

        // {
        //     GameObject obj = (GameObject)Resources.Load("App/Prefab/Game/UIColorBoard");
        //     if (obj != null)
        //     {
        //         uiColorBoardPrefab = obj.GetComponent<UIColorBoard>();
        //         uiColorBoard = (UIColorBoard)GameObject.Instantiate(uiColorBoardPrefab);
        //         uiColorBoard.gameObject.SetActive(false);
        //         RectTransform rctranPrefab = uiColorBoardPrefab.transform as RectTransform;
        //         AppSceneBase.main.AddObjToMainCanvas(uiColorBoard.gameObject);

        //         RectTransform rctran = uiColorBoard.transform as RectTransform;
        //         // 初始化rect
        //         rctran.offsetMin = rctranPrefab.offsetMin;
        //         rctran.offsetMax = rctranPrefab.offsetMax;

        //         uiColorBoard.callBackClick = OnUIColorBoardDidClick;

        //     }
        // }
        // {
        //     GameObject obj = (GameObject)Resources.Load("App/Prefab/Game/UIColorInput");
        //     if (obj != null)
        //     {
        //         uiColorInputPrefab = obj.GetComponent<UIColorInput>();
        //         uiColorInput = (UIColorInput)GameObject.Instantiate(uiColorInputPrefab);
        //         uiColorInput.gameObject.SetActive(false);

        //         RectTransform rctranPrefab = uiColorInputPrefab.transform as RectTransform;
        //         Debug.Log("uiColorInputPrefab :offsetMin=" + rctranPrefab.offsetMin + " offsetMax=" + rctranPrefab.offsetMax);

        //         AppSceneBase.main.AddObjToMainCanvas(uiColorInput.gameObject);
        //         RectTransform rctran = uiColorInput.transform as RectTransform;
        //         Debug.Log("uiColorInput 1:offsetMin=" + rctran.offsetMin + " offsetMax=" + rctran.offsetMax);
        //         // 初始化rect
        //         rctran.offsetMin = rctranPrefab.offsetMin;
        //         rctran.offsetMax = rctranPrefab.offsetMax;
        //         Debug.Log("uiColorInput 2:offsetMin=" + rctran.offsetMin + " offsetMax=" + rctran.offsetMax);
        //         uiColorInput.callBackUpdateColor = OnUIColorInputUpdateColor;
        //     }
        // }

        // {
        //     GameObject obj = (GameObject)Resources.Load("App/Prefab/Game/UILineSetting");
        //     if (obj != null)
        //     {
        //         uiLineSettingPrefab = obj.GetComponent<UILineSetting>();
        //         uiLineSetting = (UILineSetting)GameObject.Instantiate(uiLineSettingPrefab);
        //         uiLineSetting.gameObject.SetActive(false);
        //         RectTransform rctranPrefab = uiLineSettingPrefab.transform as RectTransform;
        //         AppSceneBase.main.AddObjToMainCanvas(uiLineSetting.gameObject);
        //         RectTransform rctran = uiLineSetting.transform as RectTransform;
        //         // 初始化rect
        //         rctran.offsetMin = rctranPrefab.offsetMin;
        //         rctran.offsetMax = rctranPrefab.offsetMax;

        //         uiLineSetting.callBackSettingLineWidth = OnUILineSettingLineWidth;

        //     }
        // }
    }

    public override void UpdateGuankaLevel(int level)
    {
        InitUI();
    }

    void InitUI()
    {


        LayOut();


        OnUIDidFinish();
    }


    public override void LayOut()
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

    public override void CleanGuankaList()
    {
        if (listGuanka != null)
        {
            listGuanka.Clear();
        }
    }

    public override int ParseGuanka()
    {
        int count = 0;

        if ((listGuanka != null) && (listGuanka.Count != 0))
        {
            return listGuanka.Count;
        }

        listGuanka = new List<object>();
        int idx = GameManager.placeLevel;
        string fileName = Common.GAME_RES_DIR + "/guanka/guanka_list" + idx + ".json";
        //FILE_PATH
        string json = FileUtil.ReadStringAsset(fileName);//((TextAsset)Resources.Load(fileName, typeof(TextAsset))).text;
        // Debug.Log("json::"+json);
        JsonData root = JsonMapper.ToObject(json);
        string strPlace = (string)root["place"];
        JsonData items = root["list"];
        for (int i = 0; i < items.Count; i++)
        {
            JsonData item = items[i];
            ColorItemInfo info = new ColorItemInfo();
            string strdir = Common.GAME_RES_DIR + "/image/" + strPlace;

            info.id = (string)item["id"];
            info.pic = strdir + "/draw/" + info.id + ".png";

            info.picmask = strdir + "/mask/" + info.id + ".png";
            info.colorJson = strdir + "/json/" + info.id + ".json";
            info.icon = strdir + "/thumb/" + info.id + ".png";

            //info.pic = info.picmask;

            // string filepath = GetFileSave(info);
            // info.fileSave = filepath;

            // string picname = (i + 1).ToString("d3");
            // info.pic = Common.GAME_RES_DIR + "/animal/draw/" + picname + ".png";
            // info.picmask = Common.GAME_RES_DIR + "/animal/mask/" + picname + ".png";
            // info.colorJson = Common.GAME_RES_DIR + "/animal/draw/" + picname + ".json";
            // info.icon = Common.GAME_RES_DIR + "/animal/thumb/" + picname + ".png";

            listGuanka.Add(info);
        }

        count = listGuanka.Count;

        // Debug.Log("ParseGame::count=" + count);
        return count;
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


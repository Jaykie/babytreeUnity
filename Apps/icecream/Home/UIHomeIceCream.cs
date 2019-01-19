using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHomeIceCream : UIHomeBase
{

    public Button btnPlay;
    public Button btnFree;
    public Button btnLanguae;
    public Image ImageLogo;
    public Text textGold;
    void Awake()
    {
        //bg
        TextureUtil.UpdateImageTexture(imageBg, AppRes.IMAGE_HOME_BG, true);
        UpdateLanguage();
    }
    // Use this for initialization
    void Start()
    {
        InitUI();

        //TrophyViewController.main.Show(null, null);

    }
    // Update is called once per frame
    void Update()
    {
        UpdateBase();
    }
    public void UpdateGold()
    {
        string str = Common.gold.ToString();
        textGold.text = str;
    }

    void UpdateLanguage()
    {
        TextureUtil.UpdateButtonTexture(btnFree, Language.main.IsChinese() ? AppRes.IMAGE_BTN_FREE_cn : AppRes.IMAGE_BTN_FREE_en, true);
        TextureUtil.UpdateButtonTexture(btnLanguae, Language.main.IsChinese() ? AppRes.IMAGE_BTN_LANGUAGE_cn : AppRes.IMAGE_BTN_LANGUAGE_en, true);
    }
    public override void LayOut()
    {
        float x = 0, y = 0, w = 0, h = 0;

        Vector2 sizeCanvas = this.frame.size;
        RectTransform rctranAppIcon = uiHomeAppCenter.transform as RectTransform;


        LayoutChildBase();

    }

    void InitUI()
    {

        // string appname = Common.GetAppNameDisplay();
        // TextName.text = appname;
        // bool ret = Common.GetBool(AppString.STR_KEY_BACKGROUND_MUSIC);
        // if (ret)
        // {
        //     TTS.Speek(appname);
        // }
        UpdateGold();

        LayOut();

        OnUIDidFinish();
    }

    public void GotoGame(string name)
    {

        GameViewController.gameType = name;
        GameViewController.dirRootPrefab = "App/Prefab/Game/" + GameViewController.gameType;

        UIViewController controller = GameViewController.main;
        AudioPlay.main.PlayFile(AppRes.AUDIO_BTN_CLICK);
        if (this.controller != null)
        {
            NaviViewController navi = this.controller.naviController;
            navi.source = AppRes.SOURCE_NAVI_GUANKA;
            navi.Push(controller);

        }


    }

    public void OnClickBtnShop()
    {
        StarViewController p = StarViewController.main;
        p.SetType(StarViewController.TYPE_STAR_BUY);
        p.Show(null, null);
    }

    public void OnClickBtnRestore()
    {
        StarViewController p = StarViewController.main;
        p.SetType(StarViewController.TYPE_STAR_RESTORE);
        p.Show(null, null);
    }

    public void OnClickBtnFree()
    {
    }
    public void OnClickBtnLanguage()
    {
        SystemLanguage lan = SystemLanguage.English;
        if (Language.main.IsChinese())
        {
            lan = SystemLanguage.English;

        }
        else
        {
            lan = SystemLanguage.Chinese;
        }

        Language.main.SetLanguage(lan);
        PlayerPrefs.SetInt(AppString.STR_KEY_LANGUAGE, (int)lan);
        UpdateLanguage();
    }
    public void OnClickBtnMusic()
    {
    }
    public void OnClickBtnPlay()
    {
        if (this.controller != null)
        {
            NaviViewController navi = this.controller.naviController;
            //navi.source = AppRes.SOURCE_NAVI_GUANKA; 
            int total = GameManager.placeTotal;
            List<object> listItem = GameManager.main.ParsePlaceList();
            if (total > 1)
            {
                navi.Push(PlaceViewController.main);
            }
            else
            {
                ItemInfo info = listItem[0] as ItemInfo;
                GotoGame(info.id);
            }
        }
    }



}

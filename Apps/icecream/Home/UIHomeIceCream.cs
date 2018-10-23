using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIHomeIceCream : UIHomeBase
{

    public Button btnPlay;
    public Button btnFree;
    public Image ImageLogo;
    void Awake()
    {

        //bg
        TextureUtil.UpdateImageTexture(imageBg, AppRes.IMAGE_HOME_BG, true);

    }
    // Use this for initialization
    void Start()
    {

        InitUI();
    }
    // Update is called once per frame
    void Update()
    {
        UpdateBase();
    }


    public override void LayOut()
    {
        float x = 0, y = 0, w = 0, h = 0;

        Vector2 sizeCanvas = this.frame.size;
        float h_play = 0;
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


        LayOut();

        OnUIDidFinish();
    }


    public void OnClickBtnShop()
    {
    }

    public void OnClickBtnRestore()
    {
    }

    public void OnClickBtnFree()
    {
    }
    public void OnClickBtnLanguage()
    {
    }
    public void OnClickBtnMusic()
    {
    }
    public void OnClickBtnPlay()
    {

    }
    public void GotoGame(int mode)
    {

        UIViewController controller = null;


        {

            int total = GameManager.placeTotal;
            if (total > 1)
            {
                controller = PlaceViewController.main;
            }
            else
            {
                controller = GuankaViewController.main;
            }
        }


        AudioPlay.main.PlayFile(AppRes.AUDIO_BTN_CLICK);
        if (this.controller != null)
        {
            NaviViewController navi = this.controller.naviController;
            navi.source = AppRes.SOURCE_NAVI_GUANKA;
            navi.Push(controller);

        }


    }



}

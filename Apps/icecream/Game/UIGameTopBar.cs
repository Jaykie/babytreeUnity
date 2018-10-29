using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameTopBar : UIView
{

    public Button btnShop;
    public Button btnRestore;
    public Button btnStar;

    public Button btnFree;
    public Button btnHome;
    public Button btnMusic;

    public void OnClickBtnShop()
    {

    }
    public void OnClickBtnRestore()
    {

    }
    public void OnClickBtnStar()
    {

    }
    public void OnClickBtnFree()
    {

    }
    public void OnClickBtnHome()
    {
        GameViewController.main.gameBase.OnClickBtnBack();
    }
    public void OnClickBtnMusic()
    {

    }
}

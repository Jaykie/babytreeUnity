using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameTopBar : UIView, IPopViewControllerDelegate
{

    public Button btnShop;
    public Button btnRestore;
    public Button btnStar;

    public Button btnFree;
    public Button btnHome;
    public Button btnMusic;
    public Image imageTrophyBg;
    public Image imageTrophy;
    public void OnPopViewControllerDidClose(PopViewController controller)
    {

    }
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
    public void OnClickBtnTrophy()
    {
        TrophyViewController.main.Show(null, null);
    }
}

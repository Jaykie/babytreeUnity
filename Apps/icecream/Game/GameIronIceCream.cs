using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*铁板冰淇淋步骤：
1，炒冰淇淋

*/

public class GameIronIceCream : GameIceCream
{
    public const string IMAGE_DIR_ROOT_SingleColor = "App/UI/Game/IronIceCream/SingleColor";//0  0-1 0-2 0-3 
    public const string IMAGE_DIR_ROOT_MultiColor = "App/UI/Game/IronIceCream/MultiColor";//  0-1 0-4 0-5

    public const string IMAGE_DIR_ROOT_CupLiquid = "App/UI/Game/IronIceCream/CupLiquid";//  1 2 3 4 5
    public GameObject objPanzi;//盘子
    public GameObject objChanzi;//铲子
    public GameObject objIcecreemBlock;//冰淇凌块
    public GameObject objIcecreemLiquid;//冰淇凌液体倾倒动画
    public Rect rectMain;//local

    void Awake()
    {
        objChanzi.SetActive(false);
    }
    void Start()
    {

        LayOut();
    }

    public override void LayOut()
    {
        float x, y, w, h;
        float scale = 0;
        RectTransform rctranMainWorld = AppSceneBase.main.GetRectMainWorld();

        float topbar_canvas_h = 160f;
        float oft_h = Common.CanvasToWorldHeight(mainCam, AppSceneBase.main.sizeCanvas, topbar_canvas_h) * 2;
        w = rctranMainWorld.rect.width;
        h = rctranMainWorld.rect.height - oft_h;
        x = -w / 2;
        y = rctranMainWorld.rect.height / 2 - oft_h - h;
        rectMain = new Rect(x, y, w, h);

        float ratio = 0.8f;
        {
            SpriteRenderer rd = objPanzi.GetComponent<SpriteRenderer>();
            w = rd.sprite.texture.width / 100f;
            h = rd.sprite.texture.height / 100f;
            scale = Common.GetBestFitScale(w, h, rectMain.width, rectMain.height) * ratio;
            objPanzi.transform.localScale = new Vector3(scale, scale, 1f);
        }
        {
            SpriteRenderer rd = objChanzi.GetComponent<SpriteRenderer>();
            w = rd.sprite.texture.width / 100f;
            h = rd.sprite.texture.height / 100f;
            scale = Common.GetBestFitScale(w, h, rectMain.width, rectMain.height) * ratio;
            objChanzi.transform.localScale = new Vector3(scale, scale, 1f);
        }
        this.transform.localPosition = new Vector3(rectMain.center.x, rectMain.center.y, 0f);
    }


}

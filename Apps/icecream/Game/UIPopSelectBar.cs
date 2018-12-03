using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public delegate void OnUIPopSelectBarDidClickDelegate(UIPopSelectBar bar, UITopFoodItem item);
// 顶料
public class UIPopSelectBar : UIView
{

    public const int TOTAL_CUP = 42;
    public const int TOTAL_WAN = 10;
    public GameObject objScrollView;
    public GameObject objScrollViewContent;
    public Image imageBg;
    public Image imageHand;
    public List<UITopFoodItem> listItem;
    ScrollRect scrollRect;
    UITopFoodItem uiTopFoodItemPrefab;
    float widthItem;
    Tweener twHand;
    public UITopFoodItem.Type type;

    public static int indexFoodSort = 0;
    public static int countFoodSort = 0;

    public OnUIPopSelectBarDidClickDelegate callBackDidClick { get; set; }

    void Awake()
    {
        listItem = new List<UITopFoodItem>();
        GameObject obj = PrefabCache.main.Load(UITopFoodItem.PREFAB_TopFoodItem);
        uiTopFoodItemPrefab = obj.GetComponent<UITopFoodItem>();
        scrollRect = objScrollView.GetComponent<ScrollRect>();

        UpdateItem();
        TextureUtil.UpdateImageTexture(imageHand, AppRes.IMAGE_HAND, true);

        RectTransform rctran = this.gameObject.GetComponent<RectTransform>();
        float x, y;
        x = rctran.rect.width / 2;
        y = -rctran.rect.height / 2;
        Vector3 posEnd = new Vector3(x, y, 0);

        RectTransform rctranHand = imageHand.GetComponent<RectTransform>();

        //   Vector3 posStart 
        x = 0;
        y = rctran.rect.height / 2;
        rctranHand.anchoredPosition = new Vector2(x, y);
        float scale = Common.GetBestFitScale(rctranHand.rect.width, rctranHand.rect.height, widthItem, widthItem) * 0.8f;
        imageHand.transform.localScale = new Vector3(scale, scale, 1);
        twHand = rctranHand.DOLocalMove(posEnd, 5f).SetLoops(-1, LoopType.Restart);
    }
    public void UpdateItem()
    {
        ClearItems();
        int total = countFoodSort;
        type = UITopFoodItem.Type.SUB_FOOD;

        for (int i = 0; i < total; i++)
        {
            AddItem();
        }
    }

    void ClearItems()
    {
        foreach (UITopFoodItem item in listItem)
        {
            DestroyImmediate(item.gameObject);
        }
        listItem.Clear();
    }
    public UITopFoodItem GetItem(int idx)
    {
        UITopFoodItem item = null;
        if ((listItem != null) && (listItem.Count != 0))
        {
            item = listItem[idx];
        }
        return item;
    }
    public void AddItem()
    {
        int idx = listItem.Count;
        UITopFoodItem item = (UITopFoodItem)GameObject.Instantiate(uiTopFoodItemPrefab);
        item.transform.parent = objScrollViewContent.transform;
        item.callBackDidClick = OnUITopFoodItemDidClick;
        //this.transform;
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.localPosition = new Vector3(0, 0, 0);
        item.index = idx;
        // cmdItem.callBackTouch = OnUITouchEvent;
        item.type = type;

        //更新scrollview 内容的长度
        RectTransform rctranItem = item.GetComponent<RectTransform>();
        RectTransform rctran = objScrollViewContent.GetComponent<RectTransform>();
        RectTransform rctranScroll = objScrollView.GetComponent<RectTransform>();
        Vector2 size = rctran.sizeDelta;
        size.x = rctranScroll.rect.width;

        widthItem = size.x;
        // Debug.Log("widthItem=" + widthItem);
        size.y = widthItem * (idx + 1);

        item.width = widthItem;
        item.height = size.x;
        rctran.sizeDelta = size;

        item.UpdateItem();
        listItem.Add(item);
    }


    public void OnUITopFoodItemDidClick(UITopFoodItem item)
    {
        if (callBackDidClick != null)
        {
            imageHand.gameObject.SetActive(false);
            twHand.Pause();
            callBackDidClick(this, item);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// 顶料
public class UITopFoodBar : UIView
{
    public const int TOTAL_TOP_FOOD = 42;
    public GameObject objScrollView;
    public GameObject objScrollViewContent;
    public Image imageBg;
    public List<UITopFoodItem> listItem;
    ScrollRect scrollRect;
    UITopFoodItem uiTopFoodItemPrefab;
    float widthItem;
    void Awake()
    {
        listItem = new List<UITopFoodItem>();
        GameObject obj = PrefabCache.main.Load(UITopFoodItem.PREFAB_TopFoodItem);
        uiTopFoodItemPrefab = obj.GetComponent<UITopFoodItem>();
        scrollRect = objScrollView.GetComponent<ScrollRect>();
        int total = TOTAL_TOP_FOOD;
        for (int i = 0; i < total; i++)
        {
            AddItem();
        }
    }
    public void AddItem()
    {
        int idx = listItem.Count;
        UITopFoodItem item = (UITopFoodItem)GameObject.Instantiate(uiTopFoodItemPrefab);
        item.transform.parent = objScrollViewContent.transform;
        //this.transform;
        item.transform.localScale = new Vector3(1, 1, 1);
        item.transform.localPosition = new Vector3(0, 0, 0);
        item.index = idx;
        // cmdItem.callBackTouch = OnUITouchEvent;


        //更新scrollview 内容的长度
        RectTransform rctranItem = item.GetComponent<RectTransform>();
        RectTransform rctran = objScrollViewContent.GetComponent<RectTransform>();
        RectTransform rctranScroll = objScrollView.GetComponent<RectTransform>();
        Vector2 size = rctran.sizeDelta;
        size.y = rctranScroll.rect.height;

        widthItem = size.y;
        // Debug.Log("widthItem=" + widthItem);
        size.x = widthItem * (idx + 1);

        item.width = widthItem;
        item.height = size.y;
        rctran.sizeDelta = size;

        item.UpdateItem();
        listItem.Add(item);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Tacticsoft;

public delegate void OnUIPopSelectBarDidClickDelegate(UIPopSelectBar bar, UITopFoodItem item);
// 顶料
public class UIPopSelectBar : UIView, ITableViewDataSource
{

    public const string PREFAB_CELL_ITEM = "App/Prefab/Game/IronIceCream/UIPopSelectBarCellItem";
    public const int TOTAL_CUP = 42;
    public const int TOTAL_WAN = 10;
    public GameObject objScrollView;
    public GameObject objScrollViewContent;

    public TableView tableView;
    public Image imageBg;
    public Image imageHand;
    public List<object> listItem;

    UICellItemBase cellItemPrefab;
    UICellBase cellPrefab;//GuankaItemCell GameObject 

    ScrollRect scrollRect;
    UITopFoodItem uiTopFoodItemPrefab;
    float widthItem;
    Tweener twHand;
    public UITopFoodItem.Type type;

    public static int indexFoodSort = 0;
    public static int countFoodSort = 0;
    int oneCellNum;
    int heightCell;
    int totalItem;
    int numRows;
    public OnUIPopSelectBarDidClickDelegate callBackDidClick { get; set; }

    void Awake()
    {
        LoadPrefab();
        listItem = new List<object>();
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

        UpdateTable(false);
        tableView.dataSource = this;
    }

    void LoadPrefab()
    {
        {
            GameObject obj = PrefabCache.main.Load(AppCommon.PREFAB_UICELLBASE);
            cellPrefab = obj.GetComponent<UICellBase>();
        }
        {
            GameObject obj = PrefabCache.main.Load(PREFAB_CELL_ITEM);
            cellItemPrefab = obj.GetComponent<UICellItemBase>();
        }

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
        UpdateTable(true);
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
            item = listItem[idx] as UITopFoodItem;
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

    public void OnCellItemDidClick(UICellItemBase item)
    {
        if (item.IsLock())
        {
            return;
        }


    }
    #region ITableViewDataSource
    void UpdateTable(bool isLoad)
    {
        oneCellNum = 1;

        int total = listItem.Count;
        totalItem = total;
        Debug.Log("total:" + total);
        numRows = total / oneCellNum;
        if (total % oneCellNum != 0)
        {
            numRows++;
        }

        if (isLoad)
        {
            tableView.ReloadData();
        }

    }
    void AddCellItem(UICellBase cell, TableView tableView, int row)
    {
        Rect rctable = (tableView.transform as RectTransform).rect;

        for (int i = 0; i < oneCellNum; i++)
        {
            int itemIndex = row * oneCellNum + i;
            float cell_space = 10;
            UICellItemBase item = (UICellItemBase)GameObject.Instantiate(cellItemPrefab);
            //item.itemDelegate = this;
            Rect rcItem = (item.transform as RectTransform).rect;
            item.width = (rctable.width - cell_space * (oneCellNum - 1)) / oneCellNum;
            item.height = heightCell;
            item.transform.SetParent(cell.transform, false);
            item.index = itemIndex;
            item.totalItem = totalItem;
            item.callbackClick = OnCellItemDidClick;

            cell.AddItem(item);

        }
    }

    //Will be called by the TableView to know how many rows are in this table
    public int GetNumberOfRowsForTableView(TableView tableView)
    {
        return numRows;
    }

    //Will be called by the TableView to know what is the height of each row
    public float GetHeightForRowInTableView(TableView tableView, int row)
    {
        return heightCell;
        //return (cellPrefab.transform as RectTransform).rect.height;
    }

    //Will be called by the TableView when a cell needs to be created for display
    public TableViewCell GetCellForRowInTableView(TableView tableView, int row)
    {
        UICellBase cell = tableView.GetReusableCell(cellPrefab.reuseIdentifier) as UICellBase;
        if (cell == null)
        {
            cell = (UICellBase)GameObject.Instantiate(cellPrefab);
            cell.name = "UICellBase" + row.ToString();
            Rect rccell = (cellPrefab.transform as RectTransform).rect;
            Rect rctable = (tableView.transform as RectTransform).rect;
            Vector2 sizeCell = (cellPrefab.transform as RectTransform).sizeDelta;
            Vector2 sizeTable = (tableView.transform as RectTransform).sizeDelta;
            Vector2 sizeCellNew = sizeCell;
            sizeCellNew.x = rctable.width;

            AddCellItem(cell, tableView, row);

        }
        cell.totalItem = totalItem;
        if (oneCellNum != cell.oneCellNum)
        {
            //relayout
            cell.ClearAllItem();
            AddCellItem(cell, tableView, row);
        }
        cell.oneCellNum = oneCellNum;
        cell.rowIndex = row;
        cell.UpdateItem(listItem);
        return cell;
    }

    #endregion
}

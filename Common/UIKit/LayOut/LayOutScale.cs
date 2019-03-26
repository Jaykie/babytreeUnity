using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayOutScale : LayOutBase
{
    public Type _scaleType;

    public Type scaleType
    {
        get
        {
            return _scaleType;
        }

        set
        {
            _scaleType = value;
            LayOut();
        }

    }

    public Vector2 _offsetMin;
    public Vector2 offsetMin
    {
        get
        {
            return _offsetMin;
        }

        set
        {
            _offsetMin = value;
            LayOut();
        }

    }

    public Vector2 _offsetMax;
    public Vector2 offsetMax
    {
        get
        {
            return _offsetMax;
        }

        set
        {
            _offsetMax = value;
            LayOut();
        }

    }

    public enum Type
    {
        MIN = 0,
        MAX,
    }

    void Start()
    {
        this.LayOut();
    }
    public override void LayOut()
    {
        UpdateType(scaleType);
    }


    void UpdateType(Type ty)
    {
        _scaleType = ty;
        switch (this._scaleType)
        {
            case Type.MIN:
                {
                    this.ScaleObj(this.gameObject, false);
                }
                break;
            case Type.MAX:
                {
                    this.ScaleObj(this.gameObject, true);
                }
                break;

        }
    }


    void ScaleObj(GameObject obj, bool isMaxFit)
    {

        float x, y, w = 0, h = 0;
        SpriteRenderer rd = obj.GetComponent<SpriteRenderer>();
        if (rd != null)
        {
            w = rd.sprite.texture.width / 100f;
            h = rd.sprite.texture.height / 100f;
        }
        RectTransform rctran = this.transform.parent as RectTransform;

        var w_parent = rctran.rect.width;
        var h_parent = rctran.rect.height;
        w_parent -= (this.offsetMin.x + this.offsetMax.x);
        h_parent -= (this.offsetMin.y + this.offsetMax.y);



        float scale = 0;
        if (isMaxFit == true)
        {
            scale = Common.GetMaxFitScale(w, h, w_parent, h_parent);
        }
        else
        {
            scale = Common.GetBestFitScale(w, h, w_parent, h_parent);
        }

        obj.transform.localScale = new Vector3(scale, scale, 1f);
    }
}

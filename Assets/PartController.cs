using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using Unity.VisualScripting;

public class PartController : MonoBehaviour
{
    public bool isEmpty = false;

    public int partIndex;
    public FACIAL_PART type;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //SetPart(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("RemovePart")]
    public void RemovePart()
    {
        isEmpty = true;
        spriteRenderer.sprite = null;
    }

    public void SetType(FACIAL_PART type)
    {
        this.type = type;
    }

    [ContextMenu("SetPartDefault")]
    public void SetPartDefault()
    {
        SetPart(0);
    }

    public void RandomPart(bool skipAnim = false)
    {
        type = (FACIAL_PART)Random.Range(0,3);
        var pool = PartData.instance.GetPartSprites(type);
        if (pool == null)
        {
            spriteRenderer.sprite = null;
            return;
        }
        var randResult = Random.Range(0, pool.Count);
        SetPart(randResult, skipAnim);

    }

    public void SetPart(PartDetail detail){
        SetType(detail.type);
        SetPart(detail.partIndex);
    }

    public void SetPart(int index, bool skipAnim = false)
    {
        var pool = PartData.instance.GetPartSprites(type);
        if (pool == null)
        {
            RemovePart();
            return;
        }

        isEmpty = false;

        partIndex = index;
        spriteRenderer.sprite = PartData.instance.GetPartSprites(type)[index];

        //random pos
        spriteRenderer.transform.localPosition = Random.insideUnitCircle * PartData.instance.partPosRandomDistance;

        //random angle
        spriteRenderer.transform.Rotate(0, 0, Random.Range(-10f, 10f));

        if (!skipAnim)
        {
            Squish();
        }

    }

    public void Kill()
    {

        if (squishTween != null)
        {
            squishTween.Stop(TweenStopBehavior.Complete);
        }

        Destroy(this.gameObject);
    }

    Vector3Tween squishTween;

    public void Squish()
    {

        if (squishTween != null)
        {
            squishTween.Stop(TweenStopBehavior.Complete);
        }
        spriteRenderer.transform.localScale = Vector3.one * 0.8f;
        System.Action<ITween<Vector3>> onUpdate = (t) =>
        {
            spriteRenderer.transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> onComplete = (t) =>
        {
            spriteRenderer.transform.localScale = t.CurrentValue;
        };

        squishTween = spriteRenderer.gameObject.Tween(null, Vector3.one * 0.8f, Vector3.one, 1f, TweenScaleFunctions.EaseOutElastic, onUpdate, onComplete);
    }

    public PartDetail GetPartDetail()
    {
        PartDetail detail;
        detail.localPos = transform.localPosition;
        detail.isEmpty = isEmpty;
        detail.type = type;
        detail.partIndex = partIndex;
        return detail;
    }

}

public struct PartDetail
{
    public Vector3 localPos; 
    public bool isEmpty;
    public FACIAL_PART type;
    public int partIndex;
}
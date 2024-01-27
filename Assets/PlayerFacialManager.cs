using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using System.Linq;

public class PlayerFacialManager : MonoBehaviour
{
    [Header("Main Part")]
    public PartController part_LeftEye;
    public PartController part_RightEye;
    public PartController part_Mouth;

    [Header("Reference")]
    public GameObject obj_Core;
    public Transform subPartGroup;

    public List<PartController> subParts = new List<PartController>();

    public int max_subPart = 10;

    public void AddSubPart()
    {
        if (subParts.Count >= max_subPart) return;

        var pos = NewPartPosition();

        var clone = Instantiate(PartData.instance.part_prefab, subPartGroup);
        clone.transform.position = pos;
        var partController = clone.GetComponent<PartController>();

        partController.SetType((FACIAL_PART)Random.Range(0, 3));
        partController.RandomPart();

        subParts.Add(partController);

        Squish();

    }

    public Vector3 NewPartPosition()
    {
        var pos = transform.position;

        for (int i = 0; i < 1000; i++)
        {
            pos = transform.position + (Vector3)(Random.insideUnitCircle * PartData.instance.newPartRandomDistance);
            var result = CheckInsideOtherPart(pos,0.3f) ;
            if (!result)
            {
                //Debug.Log("found");
                break;
            }
        }

        //do
        //{
        //    pos = transform.position + (Vector3)(Random.insideUnitCircle * PartData.instance.newPartRandomDistance);
        //} while (CheckInsideOtherPart(pos, 1f));

        return pos;
    }

    public bool CheckInsideOtherPart(Vector2 pos, float radius){
        var colliders = Physics2D.OverlapCircleAll(pos, radius).ToList<Collider2D>();
        //Debug.Log("colliders count:" + colliders.Count);
        var colliders_filtered = new List<Collider2D>();
        foreach(var collider in colliders)
        {
            //Debug.Log(collider.name);
            if(collider.CompareTag("Part")) colliders_filtered.Add(collider);
        }

        //Debug.Log("filtered count: "+colliders_filtered.Count);

        if(colliders_filtered.Count > 0 ) return true;
        else return false;
    }

    public void RandomPart(int facial)
    {
        RandomPart((FACIAL_PART)facial);

    }

    public void RandomPart(FACIAL_PART facial)
    {
        switch (facial)
        {
            case FACIAL_PART.LEFT_EYE:
                part_LeftEye.RandomPart();
                break;
            case FACIAL_PART.RIGHT_EYE:
                part_RightEye.RandomPart();
                break;
            case FACIAL_PART.MOUTH:
                part_Mouth.RandomPart();
                break;
        }
        Squish();
    }

    public void SetPart(FACIAL_PART facial, int index)
    {
        switch (facial)
        {
            case FACIAL_PART.LEFT_EYE:
                part_LeftEye.SetPart(index);
                break;
            case FACIAL_PART.RIGHT_EYE:
                part_RightEye.SetPart(index);
                break;
            case FACIAL_PART.MOUTH:
                part_Mouth.SetPart(index);
                break;
        }
        Squish();
    }

    Vector3Tween coreTween;

    public void Squish()
    {

        if(coreTween != null)
        {
            coreTween.Stop(TweenStopBehavior.Complete);
        }

        obj_Core.transform.localScale = Vector3.one * 0.8f;
        System.Action<ITween<Vector3>> onUpdate = (t) =>
        {
            obj_Core.transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> onComplete = (t) =>
        {
            obj_Core.transform.localScale = t.CurrentValue;
        };

        coreTween = obj_Core.Tween(null, Vector3.one * 0.8f, Vector3.one, 1f, TweenScaleFunctions.EaseOutElastic, onUpdate, onComplete);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


public enum FACIAL_PART
{
    LEFT_EYE,
    RIGHT_EYE,
    MOUTH
}
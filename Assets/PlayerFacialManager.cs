using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;
using System.Linq;

public class PlayerFacialManager : MonoBehaviour
{

    //public GameObject pivot;
    //public Transform limbGroup;

    [Header("Type")]
    public COLOR mainColor = COLOR.RED;

    [Header("Main Part")]
    public PartController part_LeftEye;
    public PartController part_RightEye;
    public PartController part_Mouth;

    [Header("Reference")]
    public SpriteRenderer sr_body ;
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

    public void SetBodyType(int index){
        sr_body.sprite = PartData.instance.GetBobySprites(mainColor)[index];

        Squish();

    }

    public void RandomBodyType(){
        var pool = PartData.instance.GetBobySprites(mainColor);
        SetBodyType(Random.Range(0,pool.Count));
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

        sr_body.transform.localScale = Vector3.one * 0.8f;
        System.Action<ITween<Vector3>> onUpdate = (t) =>
        {
            sr_body.transform.localScale = t.CurrentValue;
        };

        System.Action<ITween<Vector3>> onComplete = (t) =>
        {
            sr_body.transform.localScale = t.CurrentValue;
        };

        coreTween = sr_body.gameObject.Tween(null, Vector3.one * 0.8f, Vector3.one, 1f, TweenScaleFunctions.EaseOutElastic, onUpdate, onComplete);
    }

    // Start is called before the first frame update
    void Start()
    {

       //var distance = 1f;
       //
       //var division = 12f;
       //
       //var delta = Mathf.PI * 2 / division;
       //
       //for (int i = 0; i < division; i++)
       //{
       //    var pos = new Vector2(
       //        distance * Mathf.Cos(delta * i),
       //        distance * Mathf.Sin(delta * i)
       //        );
       //    var clone = Instantiate(pivot, limbGroup);
       //    clone.transform.localPosition = (Vector3)pos;
       //    clone.transform.Rotate(0f, 0f, delta * i * Mathf.Rad2Deg + 90);
       //}


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum COLOR
{
    BLUE,
    BROWN,
    GREEN,
    RED,
    WHITE,
    YELLOW
}

public enum FACIAL_PART
{
    LEFT_EYE,
    RIGHT_EYE,
    MOUTH
}

public enum LIMB_PART
{
    ARM,
    LEG,
    DETAIL
}
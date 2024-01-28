using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScenePartSetter : MonoBehaviour
{
    private PlayerFacialManager playerFacial;

    void Awake()
    {
        playerFacial = GetComponent<PlayerFacialManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        SetPartFromGame(DataParser.instance.partDetail);
    }

    public void SetPartFromGame(AllPartDetail partDetail)
    {
        if (!partDetail.part_LeftEye.isEmpty)
        {
            playerFacial.part_LeftEye.SetPart(partDetail.part_LeftEye);
        }
        else
        {
            playerFacial.part_LeftEye.RemovePart();
        }

        if (!partDetail.part_RightEye.isEmpty)
        {
            playerFacial.part_RightEye.SetPart(partDetail.part_RightEye);
        }
        else
        {
            playerFacial.part_RightEye.RemovePart();
        }

        if (!partDetail.part_Mouth.isEmpty)
        {
            playerFacial.part_Mouth.SetPart(partDetail.part_Mouth);
        }
        else
        {
            playerFacial.part_Mouth.RemovePart();
        }

        foreach (var subPart in partDetail.subParts)
        {
            if (!subPart.isEmpty)
            {
                playerFacial.AddSubPart(subPart.type, subPart.partIndex);
                playerFacial.subParts[playerFacial.subParts.Count - 1].transform.localPosition = subPart.localPos;
            }
        }

        for (int i = 0; i < partDetail.limbs.Count; i++)
        {
            var limb = partDetail.limbs[i];
            var targetLimb = playerFacial.limbs[i];
            if(!limb.isEmpty){
                targetLimb.SetPart(limb.type,limb.partIndex,limb.color);
            }else{
                targetLimb.RemovePart();
            }
        }

    }

}

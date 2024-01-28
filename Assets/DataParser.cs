using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataParser : MonoBehaviour
{

    public static DataParser instance;

    void Awake(){
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }else if(instance != this){
            Destroy(this.gameObject);
        }
    }


    public AllPartDetail partDetail;
    public int lastEndingIndex = 0;

    public void SetAllPartDetail(AllPartDetail partDetail){
        this.partDetail = partDetail;
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

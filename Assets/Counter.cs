using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UIを使うためのライブラリ
public class Counter : MonoBehaviour
{
    public Text count;
    public int hitCount = 0;

    void Start()
    {
        
    }
    void Update()
    {
        this.count.text = hitCount.ToString() + "Hit"; //TextにHitCountの値を代入していく
        Debug.Log("Counter:" + hitCount + "Hit");
    }
}
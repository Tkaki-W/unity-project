using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TargetController : MonoBehaviour
{
    private float time;
    public float destroyDistance = 6.0f;
   
    void Start()
    {
        this.time = 0.0f;     
    }

    void Update()
    {
        time += Time.deltaTime;
        if(time >= destroyDistance)
        {
            Destroy(this.gameObject);
        }
    }
}
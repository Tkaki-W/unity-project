using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float speed = 25.0f;
    Counter counter;

    public void SetCounter(Counter c)
    {
        this.counter = c;
    }

    private void OnCollisionEnter(Collision collision) //Bulletが何かと衝突したとき
    {
        if (collision.gameObject.tag == "Target") //Targetというタグがついた物体だった場合
        {
            if (counter != null)
            {
                counter.hitCount++;
                Debug.Log(counter.hitCount + " Hit");
            }
        }
        Invoke("destroyBullet", 1); //一秒後にdestroyBullet()を呼び出す
    }

    private void destroyBullet()
    {
        Destroy(this.gameObject); //Objectを消す       
    }

    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.rigidbody.velocity = transform.forward * this.speed; //初速度の設定
    }

    void Update()
    {
        Debug.Log("rotation:" + this.transform.rotation); //Bulletの回転を表示
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GunController : MonoBehaviour
{
    private Rigidbody rigidbody;
    public GameObject bullet;
    public GameObject camera;
    public float speed = 0.04f;
    public float jumpForce = 350.0f;
    Counter counter;
    public float rotationSpeed = 50f;//Gunの回転速度

    public float spawnDistance = 10.0f; // Bulletの生成位置

         // マウス感度の調整用パラメータ
    public float mouseSensitivity = 100f;

    // プレイヤー本体（水平回転を担当するオブジェクト）
    public Transform playerBody;

    // Bulletの生成位置からの距離
    public float distanceFromTarget = 1f;

    // 垂直回転（ピッチ）の累積値
    private float xRotation = 0f;
    private float yRotation = 0f;

    // Start is called before the first frame update


    private void Move() //Gunの移動に関するメソッド
    {
        if (Input.GetKey(KeyCode.W)) //Wキーを押している間
        {
            if (this.transform.position.z <= -5)
            {
                transform.Translate(0, 0, this.speed);
                Debug.Log("front");
            }
        }
        if (Input.GetKey(KeyCode.S)) //Sキーを押している間
        {
            if (this.transform.position.z >= -9)
            {
                transform.Translate(0, 0, -this.speed);
                Debug.Log("back");
            }
        }
        if (Input.GetKey(KeyCode.A)) //Aキーを押している間
        {
            if (this.transform.position.x >= -9.5)
            {
                transform.Translate(-this.speed, 0, 0);
                Debug.Log("left");
            }
        }
        if (Input.GetKey(KeyCode.D)) //Dキーを押している間
        {
            if (this.transform.position.x <= 9.5)
            {
                transform.Translate(this.speed, 0, 0);
                Debug.Log("right");
            }
        }
    }

    private void Jump() //Gunのジャンプに関するメソッド
    {      
        if(Input.GetKeyDown(KeyCode.Space) && this.rigidbody.velocity.y == 0) //Spaceキーを押したとき
        {
            this.rigidbody.AddForce(Vector3.up * this.jumpForce);
            Debug.Log("jump");
        }     
    }

    private void Shoot() //GunのBulletを生成するメソッド
    {
        if(Input.GetMouseButtonDown(0)) //マウスの左クリックを押したとき
        {
            Vector3 spawnPos = transform.position + transform.forward * spawnDistance; //Bulletの生成位置をGunの前方に設定
            GameObject newBullet = Instantiate(this.bullet, spawnPos, Quaternion.identity); //BulletをnewBulletという名前で生成
            Vector3 oppositeDirection = newBullet.transform.position - this.transform.position;//newBluettから見てGunの位置を引くことで、Gunから見たBulletの位置を取得
            newBullet.transform.rotation = Quaternion.LookRotation(oppositeDirection); 
            BulletController bulletController = newBullet.GetComponent<BulletController>(); //生成されたnewBullet内のBulletControllerを取得
            bulletController.SetCounter(counter); //Startで取得したcounterを、BulletController.cs内のSetCounterという関数に代入、実行
        }
    }
    private void Rotate(){

        // フレームごとにマウスの移動量を取得
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // 垂直方向の回転（ピッチ）を更新
        // マウスを上に動かすと画面が下を向くようにするため、マイナス方向に反映させる
        xRotation -= mouseY;
        yRotation += mouseX;
        // ピッチ角は-45度〜+45度に制限して不自然な反転を防止
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);
        Debug.Log(xRotation);
        // カメラ自体（このスクリプトがアタッチされているオブジェクト）のローカル回転を設定（垂直方向のみ）
        transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        
        }
   

    void Start()
    {
        this.rigidbody = GetComponent<Rigidbody>();
        this.counter = GameObject.Find("GameDirector").GetComponent<Counter>(); //GameDirectorという名前のオブジェクトを探して、その中のCounterを取得
        Debug.Log("Start");

             // カーソルを非表示にしてロック（FPSでは必須）
        Cursor.lockState = CursorLockMode.Locked;

    }

    void Update()
    {
        
        Move();
        Jump();
        Shoot();
        Rotate();     

        
    }
}


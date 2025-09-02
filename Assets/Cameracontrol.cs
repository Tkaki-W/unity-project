using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameracontrol : MonoBehaviour
{
   public GameObject gun;
    // 対象の背面からの距離（今回は 1 ）
    public float distanceFromTarget = 1f;

    void LateUpdate()
    {
 
        // 対象オブジェクトの前方向は target.forward なので、
        // 反対方向 (-target.forward) を使えば常に背面側を取得できる
        Vector3 offset = -gun.transform.forward * distanceFromTarget;

        // 新しいカメラの位置は、対象オブジェクトの位置にこのオフセットを加えたもの
        transform.position = gun.transform.position + offset;
        // オプション: もしカメラで対象オブジェクトを常に見る場合
        transform.LookAt(gun.transform);
    }

}

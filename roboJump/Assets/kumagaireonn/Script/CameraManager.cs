using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraManager : MonoBehaviour
{
    public GameObject target;                      // 追従する対象を決める変数
    [SerializeField] GameObject newPlayer;         // ボタンを押した時の追従する対象
    [SerializeField] private float CameraSpeed = 0.25f;                       //カメラスピード
    bool isChange = false;                          //追従しているかfalse;していない.true;している
    Vector3 postposition;           // 移動後位置
    Vector3 pos;                                  // カメラの初期位置を記憶するための変数

    float rd = 0;
    Vector3 cameraPos = Vector3.zero;

    void Start()
    {
      
    }

    void Update()
    {

        if (!isChange)//false
        {
            cameraPos = target.transform.position; // cameraPosという変数を作り、追従する対象の位置を入れる
            Debug.Log(isChange);
        }

        //else//true
        //{
        //Camera.main.gameObject.transform.position. = cameraPos; //　カメラの位置に変数cameraPosの位置を入れる

        transform.position = Vector3.Lerp(cameraPos,
            target.transform.position,
            0.5f);//Lerp(最初の位置、最後の位置、速度)
        Debug.Log("transform.position");
        Debug.Log(transform.position);
        rd = Vector3.Distance(new Vector3(target.transform.position.y,
                target.transform.position.x,
                0.0f),
              new Vector3(cameraPos.y, cameraPos.x, 0.0f));//三平方の定理


        //    Debug.Log(isChange=true);
        //    Debug.Log("Distance");
        //    Debug.Log(Vector3.Distance(new Vector3(target.transform.position.y,
        //        target.transform.position.x,
        //        0.0f),
        //       new Vector3(cameraPos.y, cameraPos.x, 0.0f)));
        //    if (rd <= 10)//最後位置と現在地が０．０１以下の時
        //    {
        //        isChange = false;
        //        Debug.Log("aaaa");
        //        Debug.Log(isChange=false);
        //    }

        //}



        cameraPos.y = target.transform.position.y;   // カメラの縦位置に対象の位置を入れる
        //Debug.Log("cameraPos.y");
        //Debug.Log(cameraPos.y);

        cameraPos.z = -10; // カメラの奥行きの位置に-10を入れる
        //Debug.Log("cameraPos.z");
        //Debug.Log(cameraPos.z);
       

        //Qキーを押したら変更
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    target = newPlayer;//Targetの中をnewPlayerにする
        //    isChange = true;
        //    Debug.Log("Q");
        //}
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target; // 追従する対象を決める変数
    [SerializeField] GameObject newPlayer;   // ボタンを押した時の追従する対象
    
    bool moves=false;//追従しているかfalse;していない.true;している

    Vector3 pos;              // カメラの初期位置を記憶するための変数
                              //bool Threw = false;//true;投げた/false;投げてない

    //スタートと終わりの目印
    public Transform startMarker;
    public Transform endMarker;

    // スピード
    public float speed = 1.0F;

    //二点間の距離を入れる
    private float distance_two;

  
    void Start()
    {
        pos = Camera.main.gameObject.transform.position; //カメラの初期位置を変数posに入れる

        //二点間の距離を代入(スピード調整に使う)
        distance_two = Vector3.Distance(startMarker.position, endMarker.position);
    }

    void Update()
    {
        //if (moves)
        //{
            Vector3 cameraPos = target.transform.position; // cameraPosという変数を作り、追従する対象の位置を入れる

        // 現在の位置
        float present_Location = (Time.time * speed) / distance_two;
        //if (present_Location < distance_two)
        // もし対象の横位置が0より小さい場合
        if (target.transform.position.x < 0)
            {
                cameraPos.x = 0; // カメラの横位置に0を入れる
            }

            // もし対象の縦位置が0より小さい場合
            if (target.transform.position.y < 0)
            {
                cameraPos.y = 0;  // カメラの縦位置に0を入れる
            }

            // もし対象の縦位置が0より大きい場合
            if (target.transform.position.y > 0)
            {
                cameraPos.y = target.transform.position.y;   // カメラの縦位置に対象の位置を入れる
            }

            cameraPos.z = -10; // カメラの奥行きの位置に-10を入れる
            Camera.main.gameObject.transform.position = cameraPos; //　カメラの位置に変数cameraPosの位置を入れる
      // }
        void Move()
        {
            

            // オブジェクトの移動
            transform.position = Vector3.Lerp(startMarker.position,
                new Vector3(
                   this.endMarker.position.x,
                   this.endMarker.position.y,
                -10.0f), present_Location);
        }

        //スペースボタンを押したら変更
        if (Input.GetKeyDown(KeyCode.Q))
        {
            target = newPlayer;//Targetの中をnewPlayerにする

            Move();


        }

    }

  
}

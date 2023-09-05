using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraManager : MonoBehaviour
{


    bool isPlaPos = false;

    [SerializeField] Transform playerTr;          // プレイヤーのTransform
    [SerializeField] Transform newPlayer;         // ボタンを押した時の追従する対象
    [SerializeField] public float Speed = 1.0F;
    [SerializeField] Vector3 cameraOrgPos = new Vector3(0, 0, -10f); // カメラの初期位置位置 
    [SerializeField] Vector2 camaraMaxPos = new Vector2(5, 5); // カメラの右上限界座標
    [SerializeField] Vector2 camaraMinPos = new Vector2(-5, -5); // カメラの左下限界座標

    //二点間の距離を入れる
    private float distance_two;

    void Start()
    {
        //二点間の距離を代入(スピード調整に使う)
        distance_two = Vector3.Distance(playerTr.position, newPlayer.position);
     
    }

    void LateUpdate()
    {
        Vector3 playerPos = playerTr.position; // プレイヤーの位置
        Vector3 newPlayPos = newPlayer.position;//追従が変わったときの位置
        Vector3 camPos = transform.position; // カメラの位置

        

        if (Input.GetKeyDown(KeyCode.Q))
        {

            isPlaPos =true;
            Debug.Log("Q");
        }

        if (!isPlaPos) { 
        // 滑らかにプレイヤーの場所に追従
        camPos = Vector3.Lerp(transform.position, playerPos + cameraOrgPos, 3.0f);

        // カメラの位置を制限
        camPos.x = Mathf.Clamp(camPos.x, camaraMinPos.x, camaraMaxPos.x);
        camPos.y = Mathf.Clamp(camPos.y, camaraMinPos.y, camaraMaxPos.y);
        camPos.z = -10f;
        transform.position = camPos;
            Debug.Log(isPlaPos);
        }
        else
        {
            // 滑らかにプレイヤーの場所に追従
            camPos = Vector3.Lerp(transform.position, newPlayPos + cameraOrgPos, 3.0f);

            // カメラの位置を制限
            camPos.x = Mathf.Clamp(camPos.x, camaraMinPos.x, camaraMaxPos.x);
            camPos.y = Mathf.Clamp(camPos.y, camaraMinPos.y, camaraMaxPos.y);
            camPos.z = -10f;
            transform.position = camPos;
            Debug.Log(isPlaPos);

            // 現在の位置
            float present_Location = (Time.time * Speed) / distance_two;

            // オブジェクトの移動
            transform.position = Vector3.Lerp(playerTr.position, newPlayer.position, present_Location);
            this.transform.position =
                new Vector3(
                    this.transform.position.x,
                this.transform.position.y,
                -10f);
        }

    }  
    

}

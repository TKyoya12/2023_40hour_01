using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPosition : MonoBehaviour//元の位置に戻る
{
    private Vector3 initialPosition;
    private Vector3 currentPosition;

    public GameObject Playerstart;

    void Start()
    {
        // 初期位置（ゲームスタート時点の位置）
        initialPosition = transform.position;
        Playerstart.transform.position = transform.position;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            startposition();
        }
    }
    public void startposition()
    {
        // 現在の位置
        currentPosition = transform.position;
        // 初期位置に戻す。
        transform.position = initialPosition;
        //transform.position = transform.position;
    }
}

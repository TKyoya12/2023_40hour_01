using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Respawn : MonoBehaviour//リスポーン
{

    bool isActive = false;
    // SetActive(true)：オブジェクトを表示する
    //SetActive(false)：オブジェクトを非表示にする

    public GameObject resPawn;          //リスポンするやつ
    //public GameObject Unrespawn;        //リスポンしないやつ

    public StartPosition spawnPosition;
   // public Fixation fixation;
   // public MoveManeger moveManeger;
    public GameManager gameManager;
    


    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        //if (isThrow)//投げたかどうか
        //{

            if (Input.GetKeyDown(KeyCode.P))
            {
                spawnPosition.startposition();
            }
        //}
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using System.Linq;
using Unity.VisualScripting;

public class PlayerAppearance : MonoBehaviour
{
    MoveManeger MoveMane;

   // インスタンス化するobjオブジェクトをアサインします。
    public  GameObject obj;

    // 親オブジェクトのトランスフォームをアサインします。
    public Transform parentTran;

    // ゲームオブジェクトを生成する数を指定します。
    public int prefabNum;
    private void Start()
    {
        obj.transform.SetParent(parentTran);
        //objectの親オブジェクトをparent.transformにしている
       
    }
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Z))
        {
            MoveMane = GameObject.Find("Playr").GetComponent<MoveManeger>();//スクリプトから他のGameObjectにアタッチされたスクリプトを取得する
            //prefab = rigid2D.velocity;
        }
    }

}

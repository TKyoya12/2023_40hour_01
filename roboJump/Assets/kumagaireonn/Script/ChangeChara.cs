using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ChangeChara : MonoBehaviour
{
    private PlayerMove playermove;//別スプデータを取る
    //　現在どのキャラクターを操作しているか
    private int nowChara;
    //　操作可能なゲームキャラクター
    [SerializeField]
    private List<GameObject> charaList;




    void Start()
    {
        //　最初の操作キャラクターを0番目のキャラクターにする
        playermove = charaList[0].GetComponent<PlayerMove>();


        playermove.ChangeControl(false);
    }

    void Update()
    {
        //　Zキーが押されたら操作キャラクターを次のキャラクターに変更する
        if (Input.GetKeyDown("z"))
        {
            ChangeCharacter(nowChara);
        }
    }

    //　操作キャラクター変更メソッド
    void ChangeCharacter(int tempNowChara)
    {
        playermove = charaList[tempNowChara].GetComponent<PlayerMove>();
        //　現在操作しているキャラクターを動かなくする
        //charaList[tempNowChara].GetComponent<PlayerMove>().ChangeControl(false);
        playermove.ChangeControl(false);

        //　次のキャラクターの番号を設定
        var nextChara = tempNowChara + 1;
        if (nextChara >= charaList.Count)
        {
            nextChara = 0;
        }
        //　次のキャラクターを動かせるようにする
        //charaList[nextChara].GetComponent<PlayerMove>().ChangeControl(true);
        playermove.ChangeControl(true);

        //　現在のキャラクター番号を保持する
        nowChara = nextChara;

    }
}

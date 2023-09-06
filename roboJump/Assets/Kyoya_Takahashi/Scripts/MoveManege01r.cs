using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManege01 : MonoBehaviour
{
    //インスペクタ表示変数
    [SerializeField] private float moveSpeed; //移動力



    //private CharacterController characterController;
    // bool isActive = false;//操作されているかどうか/false;していない/true:している
    private enum Direction
    {
        Stop,   //止まっている
        Right,  //右を向いている
        Left,   //左を向いている
    }
    //private PlayerMove playermove;//別スプデータを取る

    //　現在どのキャラクターを操作しているか
    private int nowChara;
    //　操作可能なゲームキャラクター
    [SerializeField]
    private List<GameObject> charaList;
    public static GameObject ThrowObject;


    //コンポーネント保存
    private Rigidbody2D rigid2D;
    //　現在キャラクターを操作出来るかどうか
    private bool control;

    //プライベート変数
    private Direction direction = Direction.Stop;     //プレイヤーの方向
    private float speed;                                //スピード

    //初期化に使用します

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        rigid2D = GetComponent<Rigidbody2D>();
        //　最初の操作キャラクターを0番目のキャラクターにする
        // charaList[0].GetComponent<>();


        ChangeControl(false);
    }


    // Update is called once per frame
    void Update()
    {

        ////　Zキーが押されたら操作キャラクターを次のキャラクターに変更する
        //if (Input.GetKeyDown("z"))
        //{
        //    ChangeCharacter(nowChara);
        //}
        float inputH = 0;

        //左右方向キー入力を取得
        inputH = Input.GetAxisRaw("Horizontal");


        //移動方向の状態を設定
        if (inputH < 0f)
        {
            //左を向いている
            direction = Direction.Left;
        }
        else if (inputH > 0f)
        {
            //右を向いている   
            direction = Direction.Right;
        }
        else
        {
            //止まっている
            direction = Direction.Stop;
        }
        // Debug.Log(direction);
    }
    private void FixedUpdate()
    {
        switch (direction)
        {
            //移動スピードを設定
            case Direction.Stop:
                speed = 0F;
                break;
            case Direction.Left:
                speed = -moveSpeed;
                //左向きに変更
                transform.localScale = new Vector3(-1f, 1f, 1f);
                break;
            case Direction.Right:
                speed = moveSpeed;
                //右向きに変更
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
        }

        //物理加速度
        rigid2D.velocity = new Vector2(speed, rigid2D.velocity.y);

    }
    public void ChangeControl(bool controlFlag)
    {
        control = false;

    }
    //　操作キャラクター変更メソッド
    void ChangeCharacter(int tempNowChara)
    {

        charaList[tempNowChara].GetComponent<MoveManeger>();
        //　現在操作しているキャラクターを動かなくする
        //charaList[tempNowChara].GetComponent<PlayerMove>().ChangeControl(false);
        ChangeControl(false);

        //　次のキャラクターの番号を設定
        var nextChara = tempNowChara + 1;
        if (nextChara >= charaList.Count)
        {
            nextChara = 0;
        }
        //　次のキャラクターを動かせるようにする
        charaList[nextChara].GetComponent<MoveManeger>().ChangeControl(true);
        ChangeControl(true);

        //　現在のキャラクター番号を保持する
        nowChara = nextChara;


    }

}

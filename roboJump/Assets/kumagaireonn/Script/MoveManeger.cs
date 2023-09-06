using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveManeger : MonoBehaviour
{
    //インスペクタ表示変数
    [SerializeField] private float moveSpeed; //移動力


     bool isActive = false;//操作されているかどうか/false;していない/true:している
    private enum Direction
    {
        Stop,   //止まっている
        Right,  //右を向いている
        Left,   //左を向いている
    }
    
    //　現在どのキャラクターを操作しているか
    private int nowChara;
    //　操作可能なゲームキャラクター
    [SerializeField]
    public static List<GameObject> charaList = new List<GameObject>();
    public static GameObject ThrowObject;
    public int PlayerIndx = -1;

    //コンポーネント保存
    private Rigidbody2D rigid2D;
    //　現在キャラクターを操作出来るかどうか
    private bool control;


    //プライベート変数
    private Direction direction = Direction.Stop;     //プレイヤーの方向
    private float speed;                                //スピード

    private int thisIndx = -1;
    //初期化に使用します

    private void Awake()
    {
        charaList.Add(gameObject);

        thisIndx = charaList.Count - 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        rigid2D = GetComponent<Rigidbody2D>();
        //　最初の操作キャラクターを0番目のキャラクターにする
        PlayerIndx = GameObject.Find("Player").GetComponent<MoveManeger>().thisIndx;
        
            charaList[PlayerIndx].GetComponent<MoveManeger>().ChangeControl(true);
        
    }

    // Update is called once per frame
    void Update()
    {
      
        if (!control)
        {
            return;
        }

        
        if (PlayerThrow.isThrow)
        {
            ChangeCharacter(nowChara);
        }
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
        control = controlFlag;

    }
    //　操作キャラクター変更メソッド
    void ChangeCharacter(int tempNowChara)
    {
        //　現在操作しているキャラクターを動かなくする
        charaList[tempNowChara].GetComponent<MoveManeger>();


        // nextPlayer.GetComponent<MoveManeger>(); 
        //　次のキャラクターの番号を設定
        PlayerIndx = PlayerThrow.nextPlayer.GetComponent< MoveManeger>().thisIndx;

        // 
        //  var nextChara = tempNowChara + 1;
        //  if (nextChara >= charaList.Count)
        //  {
        //      nextChara = 0;
        //  }
        //  //　次のキャラクターを動かせるようにする
        //if(nextChara < charaList.Count)
        //  {
        //      isActive = false;
        //  }

        //　現在のキャラクター番号を保持する
        //   nowChara = nextChara;

        AllFalseControl(PlayerIndx);
    }

    void AllFalseControl(int tempNowChara)
    {
        for(int i=0;i<charaList.Count; i++)
        {
            if (i == tempNowChara)
            {
                charaList[i].GetComponent<MoveManeger>().ChangeControl(true);
                continue;
            }
                
        charaList[i].GetComponent<MoveManeger>().ChangeControl(false);
        }
    }
    


}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;

public class MoveManeger : MonoBehaviour
{
    //インスペクタ表示変数
    [SerializeField] private float moveSpeed; //移動力
    [SerializeField] private float groundOffsectx;
    [SerializeField] private float groundOffsecty;
    [SerializeField] private LayerMask groundLayer; //地面のレイヤーマスク

   
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

    public Random.State random;
    
    //コンポーネント保存
    private Rigidbody2D rigid2D;
    private CapsuleCollider2D CapsuleCollider2D;


    //コンポーネント保存
    //　現在キャラクターを操作出来るかどうか
    private bool control;
    private Vector3 groundCheckOffset;                //接地判定用オフセット値


    //プライベート変数
    private Direction direction = Direction.Stop;     //プレイヤーの方向
    private float speed;                                //スピード

    private int thisIndx = -1;
    //初期化に使用します


    // Forceでキューブを動かすスクリプト
    Rigidbody2D rb;
    float power = 10.0f;                         //投げる強さ
    Vector2 throwDirection = new Vector2(0.5f, 0.5f); //投げる向き

    bool canPick = false;                       //拾える状態か？
    bool isTake = false;                        //持ってる状態か

    public static bool isThrow = false;         //投げたか？

    Vector2 diffFriend = new Vector2(0.0f, 1.3f);

    public static GameObject nextPlayer = null;                   //投げるオブジェクト 

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
        Debug.Log(PlayerIndx);
       
        charaList[PlayerIndx].GetComponent<MoveManeger>().ChangeControl(true);
        
        // Rigidbodyコンポーネントを取得する
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;//速度


        rb = GetComponent<Rigidbody2D>();


        //コンポーネント取得
        rigid2D = GetComponent<Rigidbody2D>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();

      

        //接地判定用オフセット値を計算
        var centerPos = CapsuleCollider2D.offset;       //コライダーの中心座標
        var buttomOffset = CapsuleCollider2D.size.y * 0.5f;    //コライダーの縦半分サイズ
        groundCheckOffset = new Vector3(0f,
                                        centerPos.y - buttomOffset,
                                        0f);
    }

    // Update is called once per frame
    void Update()
    {
      
        if (!control)
        {
            return;
            
        }

        //Debug.Log(control);
        PlayerThrowUpdate();
       // Debug.Log(nextPlayer);

        if (isThrow)
        {
            ChangeCharacter(nowChara);
            //仮で入れる
          //  random.
           // Debug.Log(isThrow);//true
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

        //// 位置 X だけ固定
        //rigid2D.constraints = RigidbodyConstraintsD.FreezePositionX;
        //transform.rigidbody2D.constraints = RigidbodyConstraints.FreezePosition;
    }
    //　操作キャラクター変更メソッド
    void ChangeCharacter(int tempNowChara)
    {
        //　現在操作しているキャラクターを動かなくする
        charaList[tempNowChara].GetComponent<MoveManeger>();

        //　次のキャラクターの番号を設定
        PlayerIndx = nextPlayer.GetComponent<MoveManeger>().thisIndx;

        AllFalseControl(PlayerIndx);
        Debug.Log(PlayerIndx);//全体3/0
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
            charaList[PlayerIndx].GetComponent<MoveManeger>().ChangeControl(true);//1
           // Debug.Log(charaList[PlayerIndx]);
        }
    }
    /// <summary>
    /// プレイヤーの向いてる方向
    /// </summary>
    /// <returns>プレイヤーの向いてる方向</returns>
    private float getPlayerDirection()
    {
        return this.gameObject.transform.localScale.x;
    }
     /// <summary>
    /// 投げる方向を求める
    /// </summary>
    void ThrowDirection()
    {
        throwDirection = new Vector2(getPlayerDirection(), 1.0f);
    }


    private bool IsGround()
    {
        bool IsGround = false;

        //============
        //始点と終点を作成
        //============
        //プレイヤーの中心から左へずれる場所
        Vector3 lsftStartPoint = groundCheckOffset + transform.position - Vector3.right * groundOffsectx;

        //プレイヤーの中心から右へずれる場所
        Vector3 rightStartPoint = groundCheckOffset + transform.position - Vector3.right * groundOffsectx;

        //プレイヤーの中心から下へずれる場所
        Vector3 endPoint = groundCheckOffset + transform.position - Vector3.up * groundOffsecty;

        //各線分と指定したレイヤーが交差しているかを判定

        IsGround = Physics2D.Linecast(lsftStartPoint, endPoint, groundLayer)
                 || Physics2D.Linecast(rightStartPoint, endPoint, groundLayer);

        //線分を表示（デバック）
        Debug.DrawLine(lsftStartPoint, endPoint, Color.red);
        Debug.DrawLine(rightStartPoint, endPoint, Color.red);
        //接地状態を表示（デバック）
        Debug.Log("IsGround{isGround}");
        return IsGround;
    }

    void PlayerThrowUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isTake)
            {
                return;
            }
            isTake = false;
            isThrow = true;

            if(PlayerIndx==1)
            {
                Debug.Log("左クリック");
            }

            Rigidbody2D rigidbody2 = nextPlayer.GetComponent<Rigidbody2D>();
            ThrowDirection();
            rigidbody2.velocity = throwDirection * power;
        }
        //拾える状態か        
        if (Input.GetMouseButtonDown(1))
        {
            if (!canPick)
            {
                return;
            }
            isTake = true;
            canPick = false;
            //Debug.Log(isThrow);//false
        }
        if (isTake)
        {
            nextPlayer.transform.position = new Vector2(
                this.gameObject.transform.position.x + diffFriend.x,
                this.gameObject.transform.position.y + diffFriend.y);
        }

        //Debug.Log(canPick);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("CanThrow"))
        {
            if (collision.gameObject == charaList[PlayerIndx])
            {
                Debug.Log(PlayerIndx);
                return;
            }
            //Debug.Log("aaaaa");
            GetComponent<BoxCollider2D>();
            canPick = true;
            nextPlayer = collision.gameObject;
            //if(PlayerIndx != -1)
            //{
            //    Debug.Log(PlayerIndx);
            //}
            
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CanThrow"))
        {
            canPick = false;
        }
      
    }

}

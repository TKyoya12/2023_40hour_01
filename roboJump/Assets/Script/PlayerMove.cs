using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //インスペクタ表示変数
    [SerializeField] private float moveSpeed; //移動力

    private enum Direction
    {
        Stop,   //止まっている
        Right,  //右を向いている
        Left,   //左を向いている
    }

    public static GameObject ThrowObject;


    //コンポーネント保存
    private Rigidbody2D rigid2D;

    //プライベート変数
    private Direction direction = Direction.Stop;     //プレイヤーの方向
    private float speed;                                //スピード




    // Start is called before the first frame update
    void Start()
    {
        //コンポーネント取得
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = 0;

        //ゲームプレイ中のみ
        //if (GameManager.Instance.state == GameManager.States.GamePlaying)
        //{
            //左右方向キー入力を取得
            inputH = Input.GetAxisRaw("Horizontal");
            // Debug.Log(inputH);

        //}

        //Runアニメーション遷移処理(キー入力値を絶対値にする)  
        //animator.SetFloat("Speed", Mathf.Abs(inputH));

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


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}

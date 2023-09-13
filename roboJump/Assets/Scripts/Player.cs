using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float speed;

    private CapsuleCollider2D _capsuleCollider;
    private Rigidbody2D _rigidbody;

    Vector2 throwDirection = new Vector2(5.0f, 10.0f); //投げる向き

    private GameObject _throwObj;

    private enum Direction
    {
        Stop,   //止まっている
        Right,  //右を向いている
        Left,   //左を向いている
    }
    private Direction direction = Direction.Stop;     //プレイヤーの方向

    public bool _isHold;
    private bool _liftFlag;

    void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();

        _liftFlag = false;
        _isHold = false;
    }

    // Update is called once per frame
    void Update()
    {

        float inputH = 0;

        //左右方向キー入力を取得
        inputH = Input.GetAxisRaw("Horizontal");


        //移動方向の状態を設定
        if (inputH < 0f)
        {
            //左を向いている
            direction = Direction.Left;
            throwDirection = new Vector2(-5.0f, 10.0f);
        }
        else if (inputH > 0f)
        {
            //右を向いている   
            direction = Direction.Right;
            throwDirection = new Vector2(5.0f, 10.0f);
        }
        else
        {
            //止まっている
            direction = Direction.Stop;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if(_liftFlag)
            {
                _isHold = true;
            }
        }


        if(_isHold)
        {
            _throwObj.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
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
        _rigidbody.velocity = new Vector2(speed, _rigidbody.velocity.y);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CanThrow")
        {
            _liftFlag = true;
            _throwObj = collision.gameObject;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CanThrow")
        {
            _liftFlag = false;
            _throwObj = null;
        }
    }
}

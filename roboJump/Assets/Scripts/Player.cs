using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float speed;

    private CapsuleCollider2D _capsuleCollider;
    private Rigidbody2D _rigidbody;

    Vector2 throwDirection = new Vector2(5.0f, 10.0f); //���������

    private GameObject _throwObj;

    private enum Direction
    {
        Stop,   //�~�܂��Ă���
        Right,  //�E�������Ă���
        Left,   //���������Ă���
    }
    private Direction direction = Direction.Stop;     //�v���C���[�̕���

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

        //���E�����L�[���͂��擾
        inputH = Input.GetAxisRaw("Horizontal");


        //�ړ������̏�Ԃ�ݒ�
        if (inputH < 0f)
        {
            //���������Ă���
            direction = Direction.Left;
            throwDirection = new Vector2(-5.0f, 10.0f);
        }
        else if (inputH > 0f)
        {
            //�E�������Ă���   
            direction = Direction.Right;
            throwDirection = new Vector2(5.0f, 10.0f);
        }
        else
        {
            //�~�܂��Ă���
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
            //�ړ��X�s�[�h��ݒ�
            case Direction.Stop:
                speed = 0F;
                break;
            case Direction.Left:
                speed = -moveSpeed;
                //�������ɕύX
                transform.localScale = new Vector3(-1f, 1f, 1f);
                break;
            case Direction.Right:
                speed = moveSpeed;
                //�E�����ɕύX
                transform.localScale = new Vector3(1f, 1f, 1f);
                break;
        }

        //���������x
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

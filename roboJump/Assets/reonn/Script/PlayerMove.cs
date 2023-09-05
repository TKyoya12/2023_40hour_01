using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    //�C���X�y�N�^�\���ϐ�
    [SerializeField] private float moveSpeed; //�ړ���

    private enum Direction
    {
        Stop,   //�~�܂��Ă���
        Right,  //�E�������Ă���
        Left,   //���������Ă���
    }

    public static GameObject ThrowObject;


    //�R���|�[�l���g�ۑ�
    private Rigidbody2D rigid2D;

    //�v���C�x�[�g�ϐ�
    private Direction direction = Direction.Stop;     //�v���C���[�̕���
    private float speed;                                //�X�s�[�h




    // Start is called before the first frame update
    void Start()
    {
        //�R���|�[�l���g�擾
        rigid2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputH = 0;

        //�Q�[���v���C���̂�
        //if (GameManager.Instance.state == GameManager.States.GamePlaying)
        //{
            //���E�����L�[���͂��擾
            inputH = Input.GetAxisRaw("Horizontal");
            // Debug.Log(inputH);

        //}

        //Run�A�j���[�V�����J�ڏ���(�L�[���͒l���Βl�ɂ���)  
        //animator.SetFloat("Speed", Mathf.Abs(inputH));

        //�ړ������̏�Ԃ�ݒ�
        if (inputH < 0f)
        {
            //���������Ă���
            direction = Direction.Left;
        }
        else if (inputH > 0f)
        {
            //�E�������Ă���   
            direction = Direction.Right;
        }
        else
        {
            //�~�܂��Ă���
            direction = Direction.Stop;
        }
        // Debug.Log(direction);
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
        rigid2D.velocity = new Vector2(speed, rigid2D.velocity.y);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}

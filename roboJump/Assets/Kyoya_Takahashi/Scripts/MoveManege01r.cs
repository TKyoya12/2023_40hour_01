using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveManege01 : MonoBehaviour
{
    //�C���X�y�N�^�\���ϐ�
    [SerializeField] private float moveSpeed; //�ړ���



    //private CharacterController characterController;
    // bool isActive = false;//���삳��Ă��邩�ǂ���/false;���Ă��Ȃ�/true:���Ă���
    private enum Direction
    {
        Stop,   //�~�܂��Ă���
        Right,  //�E�������Ă���
        Left,   //���������Ă���
    }
    //private PlayerMove playermove;//�ʃX�v�f�[�^�����

    //�@���݂ǂ̃L�����N�^�[�𑀍삵�Ă��邩
    private int nowChara;
    //�@����\�ȃQ�[���L�����N�^�[
    [SerializeField]
    private List<GameObject> charaList;
    public static GameObject ThrowObject;


    //�R���|�[�l���g�ۑ�
    private Rigidbody2D rigid2D;
    //�@���݃L�����N�^�[�𑀍�o���邩�ǂ���
    private bool control;

    //�v���C�x�[�g�ϐ�
    private Direction direction = Direction.Stop;     //�v���C���[�̕���
    private float speed;                                //�X�s�[�h

    //�������Ɏg�p���܂�

    // Start is called before the first frame update
    void Start()
    {
        //�R���|�[�l���g�擾
        rigid2D = GetComponent<Rigidbody2D>();
        //�@�ŏ��̑���L�����N�^�[��0�Ԗڂ̃L�����N�^�[�ɂ���
        // charaList[0].GetComponent<>();


        ChangeControl(false);
    }


    // Update is called once per frame
    void Update()
    {

        ////�@Z�L�[�������ꂽ�瑀��L�����N�^�[�����̃L�����N�^�[�ɕύX����
        //if (Input.GetKeyDown("z"))
        //{
        //    ChangeCharacter(nowChara);
        //}
        float inputH = 0;

        //���E�����L�[���͂��擾
        inputH = Input.GetAxisRaw("Horizontal");


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
    public void ChangeControl(bool controlFlag)
    {
        control = false;

    }
    //�@����L�����N�^�[�ύX���\�b�h
    void ChangeCharacter(int tempNowChara)
    {

        charaList[tempNowChara].GetComponent<MoveManeger>();
        //�@���ݑ��삵�Ă���L�����N�^�[�𓮂��Ȃ�����
        //charaList[tempNowChara].GetComponent<PlayerMove>().ChangeControl(false);
        ChangeControl(false);

        //�@���̃L�����N�^�[�̔ԍ���ݒ�
        var nextChara = tempNowChara + 1;
        if (nextChara >= charaList.Count)
        {
            nextChara = 0;
        }
        //�@���̃L�����N�^�[�𓮂�����悤�ɂ���
        charaList[nextChara].GetComponent<MoveManeger>().ChangeControl(true);
        ChangeControl(true);

        //�@���݂̃L�����N�^�[�ԍ���ێ�����
        nowChara = nextChara;


    }

}

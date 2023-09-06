using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class MoveManeger : MonoBehaviour
{
    //�C���X�y�N�^�\���ϐ�
    [SerializeField] private float moveSpeed; //�ړ���


     bool isActive = false;//���삳��Ă��邩�ǂ���/false;���Ă��Ȃ�/true:���Ă���
    private enum Direction
    {
        Stop,   //�~�܂��Ă���
        Right,  //�E�������Ă���
        Left,   //���������Ă���
    }
    
    //�@���݂ǂ̃L�����N�^�[�𑀍삵�Ă��邩
    private int nowChara;
    //�@����\�ȃQ�[���L�����N�^�[
    [SerializeField]
    public static List<GameObject> charaList = new List<GameObject>();
    public static GameObject ThrowObject;
    public int PlayerIndx = -1;

    //�R���|�[�l���g�ۑ�
    private Rigidbody2D rigid2D;
    //�@���݃L�����N�^�[�𑀍�o���邩�ǂ���
    private bool control;


    //�v���C�x�[�g�ϐ�
    private Direction direction = Direction.Stop;     //�v���C���[�̕���
    private float speed;                                //�X�s�[�h

    private int thisIndx = -1;
    //�������Ɏg�p���܂�

    private void Awake()
    {
        charaList.Add(gameObject);

        thisIndx = charaList.Count - 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        //�R���|�[�l���g�擾
        rigid2D = GetComponent<Rigidbody2D>();
        //�@�ŏ��̑���L�����N�^�[��0�Ԗڂ̃L�����N�^�[�ɂ���
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
        control = controlFlag;

    }
    //�@����L�����N�^�[�ύX���\�b�h
    void ChangeCharacter(int tempNowChara)
    {
        //�@���ݑ��삵�Ă���L�����N�^�[�𓮂��Ȃ�����
        charaList[tempNowChara].GetComponent<MoveManeger>();


        // nextPlayer.GetComponent<MoveManeger>(); 
        //�@���̃L�����N�^�[�̔ԍ���ݒ�
        PlayerIndx = PlayerThrow.nextPlayer.GetComponent< MoveManeger>().thisIndx;

        // 
        //  var nextChara = tempNowChara + 1;
        //  if (nextChara >= charaList.Count)
        //  {
        //      nextChara = 0;
        //  }
        //  //�@���̃L�����N�^�[�𓮂�����悤�ɂ���
        //if(nextChara < charaList.Count)
        //  {
        //      isActive = false;
        //  }

        //�@���݂̃L�����N�^�[�ԍ���ێ�����
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

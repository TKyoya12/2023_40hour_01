using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.LowLevel;

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


    // Force�ŃL���[�u�𓮂����X�N���v�g
    Rigidbody2D rb;
    float power = 10.0f;                         //�����鋭��
    Vector2 throwDirection = new Vector2(0.5f, 0.5f); //���������

    bool canPick = false;                       //�E�����Ԃ��H
    bool isTake = false;                        //�����Ă��Ԃ�

    public static bool isThrow = false;         //���������H

    Vector2 diffFriend = new Vector2(0.0f, 1.3f);

    public static GameObject nextPlayer = null;                   //������I�u�W�F�N�g 

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
        // Rigidbody�R���|�[�l���g���擾����
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;//���x
    }

    // Update is called once per frame
    void Update()
    {
      
        if (!control)
        {
            return;
        }

        PlayerThrowUpdate();
        Debug.Log(nextPlayer);
        if (isThrow)
        {
            ChangeCharacter(nowChara);
            Debug.Log(isThrow);
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


      
        //�@���̃L�����N�^�[�̔ԍ���ݒ�
        PlayerIndx = nextPlayer.GetComponent< MoveManeger>().thisIndx;

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
    /// <summary>
    /// �v���C���[�̌����Ă����
    /// </summary>
    /// <returns>�v���C���[�̌����Ă����</returns>
    private float getPlayerDirection()
    {
        return this.gameObject.transform.localScale.x;
    }
     /// <summary>
    /// ��������������߂�
    /// </summary>
    void ThrowDirection()
    {
        throwDirection = new Vector2(getPlayerDirection(), 1.0f);
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
            Rigidbody2D rigidbody2 = nextPlayer.GetComponent<Rigidbody2D>();
            ThrowDirection();
            rigidbody2.velocity = throwDirection * power;
        }
        //�E�����Ԃ�        
        if (Input.GetMouseButtonDown(1))
        {
            if (!canPick)
            {
                return;
            }
            isTake = true;
            canPick = false;
            Debug.Log(isThrow);
        }
        if (isTake)
        {
            nextPlayer.transform.position = new Vector2(
                this.gameObject.transform.position.x + diffFriend.x,
                this.gameObject.transform.position.y + diffFriend.y);
        }

        Debug.Log(canPick);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("CanThrow"))
        {
            if (collision.gameObject == charaList[PlayerIndx])
            {
                return;
            }
            Debug.Log("aaaaa");
            GetComponent<BoxCollider2D>();
            canPick = true;
            nextPlayer = collision.gameObject;
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

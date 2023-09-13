using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.LowLevel;
using UnityEngine.SceneManagement;

public class MoveManeger : MonoBehaviour
{
    //�C���X�y�N�^�\���ϐ�
    [SerializeField] private float moveSpeed; //�ړ���
    [SerializeField] private float groundOffsectx;
    [SerializeField] private float groundOffsecty;
    [SerializeField] private LayerMask groundLayer; //�n�ʂ̃��C���[�}�X�N

   
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

    public Random.State random;
    
    //�R���|�[�l���g�ۑ�
    private Rigidbody2D rigid2D;
    private CapsuleCollider2D CapsuleCollider2D;


    //�R���|�[�l���g�ۑ�
    //�@���݃L�����N�^�[�𑀍�o���邩�ǂ���
    private bool control;
    private Vector3 groundCheckOffset;                //�ڒn����p�I�t�Z�b�g�l


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
        Debug.Log(PlayerIndx);
       
        charaList[PlayerIndx].GetComponent<MoveManeger>().ChangeControl(true);
        
        // Rigidbody�R���|�[�l���g���擾����
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;//���x


        rb = GetComponent<Rigidbody2D>();


        //�R���|�[�l���g�擾
        rigid2D = GetComponent<Rigidbody2D>();
        CapsuleCollider2D = GetComponent<CapsuleCollider2D>();

      

        //�ڒn����p�I�t�Z�b�g�l���v�Z
        var centerPos = CapsuleCollider2D.offset;       //�R���C�_�[�̒��S���W
        var buttomOffset = CapsuleCollider2D.size.y * 0.5f;    //�R���C�_�[�̏c�����T�C�Y
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
            //���œ����
          //  random.
           // Debug.Log(isThrow);//true
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

        //// �ʒu X �����Œ�
        //rigid2D.constraints = RigidbodyConstraintsD.FreezePositionX;
        //transform.rigidbody2D.constraints = RigidbodyConstraints.FreezePosition;
    }
    //�@����L�����N�^�[�ύX���\�b�h
    void ChangeCharacter(int tempNowChara)
    {
        //�@���ݑ��삵�Ă���L�����N�^�[�𓮂��Ȃ�����
        charaList[tempNowChara].GetComponent<MoveManeger>();

        //�@���̃L�����N�^�[�̔ԍ���ݒ�
        PlayerIndx = nextPlayer.GetComponent<MoveManeger>().thisIndx;

        AllFalseControl(PlayerIndx);
        Debug.Log(PlayerIndx);//�S��3/0
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


    private bool IsGround()
    {
        bool IsGround = false;

        //============
        //�n�_�ƏI�_���쐬
        //============
        //�v���C���[�̒��S���獶�ւ����ꏊ
        Vector3 lsftStartPoint = groundCheckOffset + transform.position - Vector3.right * groundOffsectx;

        //�v���C���[�̒��S����E�ւ����ꏊ
        Vector3 rightStartPoint = groundCheckOffset + transform.position - Vector3.right * groundOffsectx;

        //�v���C���[�̒��S���牺�ւ����ꏊ
        Vector3 endPoint = groundCheckOffset + transform.position - Vector3.up * groundOffsecty;

        //�e�����Ǝw�肵�����C���[���������Ă��邩�𔻒�

        IsGround = Physics2D.Linecast(lsftStartPoint, endPoint, groundLayer)
                 || Physics2D.Linecast(rightStartPoint, endPoint, groundLayer);

        //������\���i�f�o�b�N�j
        Debug.DrawLine(lsftStartPoint, endPoint, Color.red);
        Debug.DrawLine(rightStartPoint, endPoint, Color.red);
        //�ڒn��Ԃ�\���i�f�o�b�N�j
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
                Debug.Log("���N���b�N");
            }

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

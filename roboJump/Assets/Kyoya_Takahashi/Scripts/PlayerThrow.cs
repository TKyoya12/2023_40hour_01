using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    // Force�ŃL���[�u�𓮂����X�N���v�g
    Rigidbody2D rb;
    float power = 10.0f;                         //�����鋭��
    Vector2 direction = new Vector2(0.5f, 0.5f); //���������
    [SerializeField] GameObject bullet = null;

    bool canPick = false;                       //�E�����Ԃ��H
    bool isTake = false;                        //�����Ă��Ԃ�

    public static bool isThrow = false;

    Vector2 diffFriend = new Vector2(0.0f, 1.3f);

   public static GameObject nextPlayer = null;                   //������I�u�W�F�N�g 

    private void Start()
    {
        // Rigidbody�R���|�[�l���g���擾����
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;//���x

    }
    private void Update()
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
            throwDirection();
            rigidbody2.velocity = direction * power;
        }
        //�E�����Ԃ�        
        if (Input.GetMouseButtonDown(1))
        {
            if (!canPick)
            {
                return;
            }
            isTake = true;
        }
        if (isTake)
        {
            nextPlayer.transform.position = new Vector2(
                this.gameObject.transform.position.x + diffFriend.x,
                this.gameObject.transform.position.y + diffFriend.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CanThrow"))
        {
            canPick = true;
            nextPlayer = collision.gameObject;
        }
        else
        {
            canPick = false;
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
    void throwDirection()
    {
        direction = new Vector2(getPlayerDirection(), 1.0f);
    }
}

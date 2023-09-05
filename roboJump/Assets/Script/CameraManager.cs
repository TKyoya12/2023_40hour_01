using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject target; // �Ǐ]����Ώۂ����߂�ϐ�
    [SerializeField] GameObject newPlayer;   // �{�^�������������̒Ǐ]����Ώ�
    
    bool moves=false;//�Ǐ]���Ă��邩false;���Ă��Ȃ�.true;���Ă���

    Vector3 pos;              // �J�����̏����ʒu���L�����邽�߂̕ϐ�
                              //bool Threw = false;//true;������/false;�����ĂȂ�

    //�X�^�[�g�ƏI���̖ڈ�
    public Transform startMarker;
    public Transform endMarker;

    // �X�s�[�h
    public float speed = 1.0F;

    //��_�Ԃ̋���������
    private float distance_two;

  
    void Start()
    {
        pos = Camera.main.gameObject.transform.position; //�J�����̏����ʒu��ϐ�pos�ɓ����

        //��_�Ԃ̋�������(�X�s�[�h�����Ɏg��)
        distance_two = Vector3.Distance(startMarker.position, endMarker.position);
    }

    void Update()
    {
        //if (moves)
        //{
            Vector3 cameraPos = target.transform.position; // cameraPos�Ƃ����ϐ������A�Ǐ]����Ώۂ̈ʒu������

        // ���݂̈ʒu
        float present_Location = (Time.time * speed) / distance_two;
        //if (present_Location < distance_two)
        // �����Ώۂ̉��ʒu��0��菬�����ꍇ
        if (target.transform.position.x < 0)
            {
                cameraPos.x = 0; // �J�����̉��ʒu��0������
            }

            // �����Ώۂ̏c�ʒu��0��菬�����ꍇ
            if (target.transform.position.y < 0)
            {
                cameraPos.y = 0;  // �J�����̏c�ʒu��0������
            }

            // �����Ώۂ̏c�ʒu��0���傫���ꍇ
            if (target.transform.position.y > 0)
            {
                cameraPos.y = target.transform.position.y;   // �J�����̏c�ʒu�ɑΏۂ̈ʒu������
            }

            cameraPos.z = -10; // �J�����̉��s���̈ʒu��-10������
            Camera.main.gameObject.transform.position = cameraPos; //�@�J�����̈ʒu�ɕϐ�cameraPos�̈ʒu������
      // }
        void Move()
        {
            

            // �I�u�W�F�N�g�̈ړ�
            transform.position = Vector3.Lerp(startMarker.position,
                new Vector3(
                   this.endMarker.position.x,
                   this.endMarker.position.y,
                -10.0f), present_Location);
        }

        //�X�y�[�X�{�^������������ύX
        if (Input.GetKeyDown(KeyCode.Q))
        {
            target = newPlayer;//Target�̒���newPlayer�ɂ���

            Move();


        }

    }

  
}

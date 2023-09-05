using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraManager : MonoBehaviour
{
    public GameObject target;                      // �Ǐ]����Ώۂ����߂�ϐ�
    [SerializeField] GameObject newPlayer;         // �{�^�������������̒Ǐ]����Ώ�
    [SerializeField] private float CameraSpeed = 0.25f;                       //�J�����X�s�[�h
    bool isChange = false;                          //�Ǐ]���Ă��邩false;���Ă��Ȃ�.true;���Ă���
    Vector3 postposition;           // �ړ���ʒu
    Vector3 pos;                                  // �J�����̏����ʒu���L�����邽�߂̕ϐ�

    float rd = 0;
    Vector3 cameraPos = Vector3.zero;

    void Start()
    {
      
    }

    void Update()
    {

        if (!isChange)//false
        {
            cameraPos = target.transform.position; // cameraPos�Ƃ����ϐ������A�Ǐ]����Ώۂ̈ʒu������
            Debug.Log(isChange);
        }

        //else//true
        //{
        //Camera.main.gameObject.transform.position. = cameraPos; //�@�J�����̈ʒu�ɕϐ�cameraPos�̈ʒu������

        transform.position = Vector3.Lerp(cameraPos,
            target.transform.position,
            0.5f);//Lerp(�ŏ��̈ʒu�A�Ō�̈ʒu�A���x)
        Debug.Log("transform.position");
        Debug.Log(transform.position);
        rd = Vector3.Distance(new Vector3(target.transform.position.y,
                target.transform.position.x,
                0.0f),
              new Vector3(cameraPos.y, cameraPos.x, 0.0f));//�O�����̒藝


        //    Debug.Log(isChange=true);
        //    Debug.Log("Distance");
        //    Debug.Log(Vector3.Distance(new Vector3(target.transform.position.y,
        //        target.transform.position.x,
        //        0.0f),
        //       new Vector3(cameraPos.y, cameraPos.x, 0.0f)));
        //    if (rd <= 10)//�Ō�ʒu�ƌ��ݒn���O�D�O�P�ȉ��̎�
        //    {
        //        isChange = false;
        //        Debug.Log("aaaa");
        //        Debug.Log(isChange=false);
        //    }

        //}



        cameraPos.y = target.transform.position.y;   // �J�����̏c�ʒu�ɑΏۂ̈ʒu������
        //Debug.Log("cameraPos.y");
        //Debug.Log(cameraPos.y);

        cameraPos.z = -10; // �J�����̉��s���̈ʒu��-10������
        //Debug.Log("cameraPos.z");
        //Debug.Log(cameraPos.z);
       

        //Q�L�[����������ύX
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    target = newPlayer;//Target�̒���newPlayer�ɂ���
        //    isChange = true;
        //    Debug.Log("Q");
        //}
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class CameraManager : MonoBehaviour
{


    bool isPlaPos = false;

    [SerializeField] Transform playerTr;          // �v���C���[��Transform
    [SerializeField] Transform newPlayer;         // �{�^�������������̒Ǐ]����Ώ�
    [SerializeField] public float Speed = 1.0F;
    [SerializeField] Vector3 cameraOrgPos = new Vector3(0, 0, -10f); // �J�����̏����ʒu�ʒu 
    [SerializeField] Vector2 camaraMaxPos = new Vector2(5, 5); // �J�����̉E����E���W
    [SerializeField] Vector2 camaraMinPos = new Vector2(-5, -5); // �J�����̍������E���W

    //��_�Ԃ̋���������
    private float distance_two;

    void Start()
    {
        //��_�Ԃ̋�������(�X�s�[�h�����Ɏg��)
        distance_two = Vector3.Distance(playerTr.position, newPlayer.position);
     
    }

    void LateUpdate()
    {
        Vector3 playerPos = playerTr.position; // �v���C���[�̈ʒu
        Vector3 newPlayPos = newPlayer.position;//�Ǐ]���ς�����Ƃ��̈ʒu
        Vector3 camPos = transform.position; // �J�����̈ʒu

        

        if (Input.GetKeyDown(KeyCode.Q))
        {

            isPlaPos =true;
            Debug.Log("Q");
        }

        if (!isPlaPos) { 
        // ���炩�Ƀv���C���[�̏ꏊ�ɒǏ]
        camPos = Vector3.Lerp(transform.position, playerPos + cameraOrgPos, 3.0f);

        // �J�����̈ʒu�𐧌�
        camPos.x = Mathf.Clamp(camPos.x, camaraMinPos.x, camaraMaxPos.x);
        camPos.y = Mathf.Clamp(camPos.y, camaraMinPos.y, camaraMaxPos.y);
        camPos.z = -10f;
        transform.position = camPos;
            Debug.Log(isPlaPos);
        }
        else
        {
            // ���炩�Ƀv���C���[�̏ꏊ�ɒǏ]
            camPos = Vector3.Lerp(transform.position, newPlayPos + cameraOrgPos, 3.0f);

            // �J�����̈ʒu�𐧌�
            camPos.x = Mathf.Clamp(camPos.x, camaraMinPos.x, camaraMaxPos.x);
            camPos.y = Mathf.Clamp(camPos.y, camaraMinPos.y, camaraMaxPos.y);
            camPos.z = -10f;
            transform.position = camPos;
            Debug.Log(isPlaPos);

            // ���݂̈ʒu
            float present_Location = (Time.time * Speed) / distance_two;

            // �I�u�W�F�N�g�̈ړ�
            transform.position = Vector3.Lerp(playerTr.position, newPlayer.position, present_Location);
            this.transform.position =
                new Vector3(
                    this.transform.position.x,
                this.transform.position.y,
                -10f);
        }

    }  
    

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPosition : MonoBehaviour//���̈ʒu�ɖ߂�
{
    private Vector3 initialPosition;
    private Vector3 currentPosition;

    public GameObject Playerstart;

    void Start()
    {
        // �����ʒu�i�Q�[���X�^�[�g���_�̈ʒu�j
        initialPosition = transform.position;
        Playerstart.transform.position = transform.position;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            startposition();
        }
    }
    public void startposition()
    {
        // ���݂̈ʒu
        currentPosition = transform.position;
        // �����ʒu�ɖ߂��B
        transform.position = initialPosition;
        //transform.position = transform.position;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Respawn : MonoBehaviour//���X�|�[��
{

    bool isActive = false;
    // SetActive(true)�F�I�u�W�F�N�g��\������
    //SetActive(false)�F�I�u�W�F�N�g���\���ɂ���

    public GameObject resPawn;          //���X�|��������
    //public GameObject Unrespawn;        //���X�|�����Ȃ����

    public StartPosition spawnPosition;
   // public Fixation fixation;
   // public MoveManeger moveManeger;
    public GameManager gameManager;
    


    private void Update()
    {
        if (!isActive)
        {
            return;
        }
        //if (isThrow)//���������ǂ���
        //{

            if (Input.GetKeyDown(KeyCode.P))
            {
                spawnPosition.startposition();
            }
        //}
    }
}


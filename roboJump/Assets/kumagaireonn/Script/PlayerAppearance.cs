using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using System.Linq;
using Unity.VisualScripting;

public class PlayerAppearance : MonoBehaviour
{
    MoveManeger MoveMane;

   // �C���X�^���X������obj�I�u�W�F�N�g���A�T�C�����܂��B
    public  GameObject obj;

    // �e�I�u�W�F�N�g�̃g�����X�t�H�[�����A�T�C�����܂��B
    public Transform parentTran;

    // �Q�[���I�u�W�F�N�g�𐶐����鐔���w�肵�܂��B
    public int prefabNum;
    private void Start()
    {
        obj.transform.SetParent(parentTran);
        //object�̐e�I�u�W�F�N�g��parent.transform�ɂ��Ă���
       
    }
    private void Update()
    {
       if(Input.GetKeyDown(KeyCode.Z))
        {
            MoveMane = GameObject.Find("Playr").GetComponent<MoveManeger>();//�X�N���v�g���瑼��GameObject�ɃA�^�b�`���ꂽ�X�N���v�g���擾����
            //prefab = rigid2D.velocity;
        }
    }

}

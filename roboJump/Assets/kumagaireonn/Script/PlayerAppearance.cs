using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using System.Linq;
using Unity.VisualScripting;

public class PlayerAppearance : MonoBehaviour
{
   // �C���X�^���X������Prefab�I�u�W�F�N�g���A�T�C�����܂��B
    public GameObject prefab;

    GameObject obj;

    // �e�I�u�W�F�N�g�̃g�����X�t�H�[�����A�T�C�����܂��B
    public Transform parentTran;

    // �Q�[���I�u�W�F�N�g�𐶐����鐔���w�肵�܂��B
    public int prefabNum;
    private void Start()
    {
        obj.transform.SetParent(parentTran);//object�̐e�I�u�W�F�N�g��parent.transform�ɂ��Ă���
    }
}

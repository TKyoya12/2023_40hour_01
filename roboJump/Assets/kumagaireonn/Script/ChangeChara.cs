using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ChangeChara : MonoBehaviour
{
    private PlayerMove playermove;//�ʃX�v�f�[�^�����
    //�@���݂ǂ̃L�����N�^�[�𑀍삵�Ă��邩
    private int nowChara;
    //�@����\�ȃQ�[���L�����N�^�[
    [SerializeField]
    private List<GameObject> charaList;




    void Start()
    {
        //�@�ŏ��̑���L�����N�^�[��0�Ԗڂ̃L�����N�^�[�ɂ���
        playermove = charaList[0].GetComponent<PlayerMove>();


        playermove.ChangeControl(false);
    }

    void Update()
    {
        //�@Z�L�[�������ꂽ�瑀��L�����N�^�[�����̃L�����N�^�[�ɕύX����
        if (Input.GetKeyDown("z"))
        {
            ChangeCharacter(nowChara);
        }
    }

    //�@����L�����N�^�[�ύX���\�b�h
    void ChangeCharacter(int tempNowChara)
    {
        playermove = charaList[tempNowChara].GetComponent<PlayerMove>();
        //�@���ݑ��삵�Ă���L�����N�^�[�𓮂��Ȃ�����
        //charaList[tempNowChara].GetComponent<PlayerMove>().ChangeControl(false);
        playermove.ChangeControl(false);

        //�@���̃L�����N�^�[�̔ԍ���ݒ�
        var nextChara = tempNowChara + 1;
        if (nextChara >= charaList.Count)
        {
            nextChara = 0;
        }
        //�@���̃L�����N�^�[�𓮂�����悤�ɂ���
        //charaList[nextChara].GetComponent<PlayerMove>().ChangeControl(true);
        playermove.ChangeControl(true);

        //�@���݂̃L�����N�^�[�ԍ���ێ�����
        nowChara = nextChara;

    }
}

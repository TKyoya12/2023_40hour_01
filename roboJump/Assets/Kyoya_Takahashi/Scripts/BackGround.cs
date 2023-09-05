using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    private float length, startpos;
    [SerializeField] GameObject cam;
    [SerializeField]     float parallaxEffect;

    void Start()
    {
        // �w�i�摜��x���W
        startpos = transform.position.y;
        // �w�i�摜��x�������̕�
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void FixedUpdate()
    {
        // �����X�N���[���Ɏg�p����p�����[�^�[
        float temp = (cam.transform.position.y * (1 - parallaxEffect));

        // �������ʂ�^���鏈��
        // �w�i�摜��x���W��dist�̕��ړ�������
        transform.position = new Vector3(transform.position.x, startpos, transform.position.z);

        // �����X�N���[��
        // ��ʊO�ɂȂ�����w�i�摜���ړ�������
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}

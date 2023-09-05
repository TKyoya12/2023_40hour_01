using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateParabola : MonoBehaviour
{
    //�J�����I�u�W�F�N�g
    [SerializeField] Camera cam;
    //�v���C���[�I�u�W�F�N�g
    [SerializeField] GameObject player;
    //������UI
    [SerializeField] Image parabola;
    //�v���C���[�̃X�N���[�����W
    Vector3 playerScreenPos = Vector3.zero;
    //������p�x
    float throwAngle = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerScreenPos = cam.WorldToScreenPoint(player.transform.position);
        parabola.transform.position = playerScreenPos;

        Vector2 dt = Input.mousePosition - cam.WorldToScreenPoint(player.transform.position);
        float rad = Mathf.Atan2(dt.y, dt.x);
        float degree = rad * Mathf.Rad2Deg;
        throwAngle = degree;
        parabola.transform.eulerAngles = new Vector3(0.0f, 0.0f, throwAngle);
    }
}

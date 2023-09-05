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
        // 背景画像のx座標
        startpos = transform.position.y;
        // 背景画像のx軸方向の幅
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void FixedUpdate()
    {
        // 無限スクロールに使用するパラメーター
        float temp = (cam.transform.position.y * (1 - parallaxEffect));

        // 視差効果を与える処理
        // 背景画像のx座標をdistの分移動させる
        transform.position = new Vector3(transform.position.x, startpos, transform.position.z);

        // 無限スクロール
        // 画面外になったら背景画像を移動させる
        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}

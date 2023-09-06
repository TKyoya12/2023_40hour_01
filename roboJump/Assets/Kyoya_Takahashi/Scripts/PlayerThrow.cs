using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerThrow : MonoBehaviour
{
    // Forceでキューブを動かすスクリプト
    Rigidbody2D rb;
    float power = 10.0f;                         //投げる強さ
    Vector2 direction = new Vector2(0.5f, 0.5f); //投げる向き
    [SerializeField] GameObject bullet = null;

    bool canPick = false;                       //拾える状態か？
    bool isTake = false;                        //持ってる状態か

    public static bool isThrow = false;

    Vector2 diffFriend = new Vector2(0.0f, 1.3f);

   public static GameObject nextPlayer = null;                   //投げるオブジェクト 

    private void Start()
    {
        // Rigidbodyコンポーネントを取得する
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.zero;//速度

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!isTake)
            {
                return;
            }
            isTake = false;
            isThrow = true;
            Rigidbody2D rigidbody2 = nextPlayer.GetComponent<Rigidbody2D>();
            throwDirection();
            rigidbody2.velocity = direction * power;
        }
        //拾える状態か        
        if (Input.GetMouseButtonDown(1))
        {
            if (!canPick)
            {
                return;
            }
            isTake = true;
        }
        if (isTake)
        {
            nextPlayer.transform.position = new Vector2(
                this.gameObject.transform.position.x + diffFriend.x,
                this.gameObject.transform.position.y + diffFriend.y);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("CanThrow"))
        {
            canPick = true;
            nextPlayer = collision.gameObject;
        }
        else
        {
            canPick = false;
        }
    }
    /// <summary>
    /// プレイヤーの向いてる方向
    /// </summary>
    /// <returns>プレイヤーの向いてる方向</returns>
    private float getPlayerDirection()
    {
        return this.gameObject.transform.localScale.x;
    }
    /// <summary>
    /// 投げる方向を求める
    /// </summary>
    void throwDirection()
    {
        direction = new Vector2(getPlayerDirection(), 1.0f);
    }
}

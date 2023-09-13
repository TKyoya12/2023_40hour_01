using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldCollider : MonoBehaviour
{
    [SerializeField] private GameObject _playerObj;

    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = _playerObj.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "CantThrow")
        //{
        //    if (Input.GetKeyDown(KeyCode.Return))
        //    {
        //        player.SetColliderObj(collision.gameObject);
        //    }

        //}
    }
}

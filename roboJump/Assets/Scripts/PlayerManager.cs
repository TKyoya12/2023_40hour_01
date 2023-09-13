using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private GameObject _camera;

    private GameObject _players;


    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        _players = GameObject.FindGameObjectWithTag("CanThrow");

        _camera.transform.position = new Vector3(_players.transform.position.x, _players.transform.position.y,-10.0f);
    }
}

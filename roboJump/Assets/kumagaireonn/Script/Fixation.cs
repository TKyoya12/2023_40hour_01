using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fixation : MonoBehaviour//ŒÅ’è
{
    public StartPosition spawnPosition;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
 fixation();
        }
       
    }
    public void fixation()
    {
        spawnPosition.startposition();
        transform.position = transform.position;
    }
}

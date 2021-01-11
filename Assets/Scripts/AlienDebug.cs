using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienDebug : MonoBehaviour
{
    public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player.transform.position.x < transform.position.x - 0.1f)
        {
            transform.position += new Vector3(-speed * Time.deltaTime, 0);
        }
        else if (player.transform.position.x > transform.position.x + 0.1f)
        {
            transform.position += new Vector3(speed * Time.deltaTime, 0);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanDebug : MonoBehaviour
{

    public float speed = 1;
    public GameObject debugMissile;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0);
        if (Input.GetKeyDown("space")){
            Instantiate(debugMissile, transform.position, transform.rotation);
        }
    }
}

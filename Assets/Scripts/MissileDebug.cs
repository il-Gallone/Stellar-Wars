using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDebug : MonoBehaviour
{

    public float speed = 1;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime);
        if (transform.position.y > 6){
            Destroy(gameObject);
        }
    }
}

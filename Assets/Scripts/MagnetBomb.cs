using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBomb : MonoBehaviour
{

    public float speed = 0.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, - speed * Time.deltaTime);
        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }
}

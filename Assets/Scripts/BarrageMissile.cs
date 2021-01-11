using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageMissile : MonoBehaviour
{
    public float speed = 3;
    public float rotationSpeed = 120;

    public float randomTimer = 0;
    public int direction;

    // Start is called before the first frame update
    void Start()
    {
        randomTimer = Random.Range(0.1f, 0.3f);
        direction = (1 - (Random.Range(0, 2) * 2));
    }

    // Update is called once per frame
    void Update()
    {
        randomTimer -= Time.deltaTime;
        if(randomTimer <= 0)
        {
            randomTimer = Random.Range(0.1f, 0.3f);
            direction *= -1;
        }
        transform.eulerAngles += new Vector3(0, 0, direction * rotationSpeed * Time.deltaTime);
        transform.position += transform.up * speed * Time.deltaTime;
        if(Mathf.Abs(transform.position.y) >= 6 || Mathf.Abs(transform.position.x) >= 9)
        {
            Destroy(gameObject);
        }
    }
}

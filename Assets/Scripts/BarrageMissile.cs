using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageMissile : MonoBehaviour
{
    public float speed = 3;
    public float rotationSpeed = 120;

    public float randomTimer = 0;
    public int direction;

    public int damage = 3;

    Rigidbody2D rigid2D;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        randomTimer = Random.Range(0.1f, 0.3f);
        direction = (1 - (Random.Range(0, 2) * 2));
        rigid2D.angularVelocity = rotationSpeed * direction;
    }

    // Update is called once per frame
    void Update()
    {
        randomTimer -= Time.deltaTime;
        if(randomTimer <= 0)
        {
            randomTimer = Random.Range(0.1f, 0.3f);
            direction *= -1;
            rigid2D.angularVelocity = rotationSpeed * direction;
        }
        rigid2D.velocity = transform.up * speed;
        if(Mathf.Abs(transform.position.y) >= 5.2 || Mathf.Abs(transform.position.x) >= 9.8)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy Player"))
        {
            collision.gameObject.GetComponent<AlienControl>().health -= damage;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<HumanController>().mode != HumanController.ABILITY.UBER)
                GameObject.FindGameObjectWithTag("Player").GetComponent<HumanController>().UCharge += (float)(damage / 2);
            Destroy(gameObject);
        }
    }
}

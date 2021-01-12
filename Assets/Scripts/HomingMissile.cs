using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingMissile : MonoBehaviour
{
    Rigidbody2D rigid2D;
    public float speed = 3;
    public float speedScaling = 0.8f;
    public float rotationSpeed = 45;

    public int damage = 5;

    // Start is called before the first frame update
    void Start()
    {
        int startDirection = 1;
        startDirection -= Random.Range(0, 2) * 2;
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        transform.rotation = Quaternion.AngleAxis(startDirection * 85, Vector3.forward);
    }

    // Update is called once per frame
    void Update()
    {
        rigid2D.velocity = transform.up * speed;
        speed += speedScaling * Time.deltaTime;
        GameObject alien = GameObject.FindGameObjectWithTag("Enemy Player");
        Vector3 direction = alien.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.AngleAxis(angle-90, Vector3.forward), rotationSpeed * Time.deltaTime);

        if (Mathf.Abs(transform.position.y) >= 5.2 || Mathf.Abs(transform.position.x) >= 9.8)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy Player"))
        {
            collision.gameObject.GetComponent<AlienDebug>().health -= damage;
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<HumanController>().mode != HumanController.ABILITY.UBER)
                GameObject.FindGameObjectWithTag("Player").GetComponent<HumanController>().UCharge += (float)(damage / 2);
            Destroy(gameObject);
        }
    }
}

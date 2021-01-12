using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProj : MonoBehaviour
{
    public int directionSpeed = 0;
    public int damage = 0;
    Rigidbody2D rigid2D;

    // Start is called before the first frame update
    void Start()
    {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        if (directionSpeed == 0)
        {
            directionSpeed = 1;
        }
        if (damage == 0)
        {
            damage = 1;
        }
        rigid2D.velocity = transform.up * directionSpeed;
    }

    void Update()
    {
        if(Mathf.Abs(transform.position.y) >= 5.2)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy Player") && directionSpeed > 0)
        {
            collision.gameObject.GetComponent<AlienDebug>().health -= damage;
            if(GameObject.FindGameObjectWithTag("Player").GetComponent<HumanController>().mode != HumanController.ABILITY.UBER)
                GameObject.FindGameObjectWithTag("Player").GetComponent<HumanController>().UCharge += (float)(damage / 2);
            Destroy(gameObject);
        }
        if(collision.CompareTag("Player") && directionSpeed < 0)
        {
            if (collision.gameObject.GetComponent<HumanController>().mode == HumanController.ABILITY.SPEED &&
                collision.gameObject.GetComponent<HumanController>().heatMode == 0)
                collision.gameObject.GetComponent<HumanController>().health -= damage / 2;
            else
                collision.gameObject.GetComponent<HumanController>().health -= damage;
            Destroy(gameObject);
        }
    }
}

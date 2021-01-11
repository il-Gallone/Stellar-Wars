using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileDebug : MonoBehaviour
{

    public float speed = 1;
    Rigidbody2D rigid2D;

    private void Start(){
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        rigid2D.velocity = new Vector2(0, speed);
    }

    // Update is called once per frame
    void Update(){
        if (transform.position.y > 6){
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Enemy Player"){
            collision.gameObject.GetComponent<AlienControl>().health -= 2;
            Destroy(gameObject);
        }
    }
}

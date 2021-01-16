using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBombExplosion : MonoBehaviour
{

    CircleCollider2D circle;

    // Start is called before the first frame update
    void Start(){
        circle = gameObject.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update(){
        circle.radius += 2 * Time.deltaTime;
        if (circle.radius >= 2){
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.tag == "Player"){
            HumanController human = collision.gameObject.GetComponent<HumanController>();
            if (human.mode == HumanController.ABILITY.SPEED && human.speedMode == 0){
                human.health -= 5;
            }else{
                human.health -= 10;
            }
        }
    }
}
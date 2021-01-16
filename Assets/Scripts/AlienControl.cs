using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienControl : MonoBehaviour
{

    public float speed = 1;
    public enum ABILITY {SLOWSTRONG,DEFENSE,LOCKON,UBER};
    public ABILITY mode = ABILITY.SLOWSTRONG;
    public GameObject magnetBomb;
    private float SSTimer = 0;
    Rigidbody2D rigid2D;
    public int health = 500;

    private void Start(){
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x < transform.position.x - 0.1f){
            rigid2D.velocity = new Vector2(-speed, 0);
        }else if(player.transform.position.x > transform.position.x + 0.1f){
            rigid2D.velocity = new Vector2(speed, 0);
        }else{
            rigid2D.velocity = Vector2.zero;
        }

        if (mode == ABILITY.SLOWSTRONG){
            SSTimer += Time.deltaTime;
            if (SSTimer > 8){
                SSTimer -= 8;
                Instantiate(magnetBomb, transform.position, transform.rotation);
            }
        }
    }
}

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

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player.transform.position.x < transform.position.x - 0.1f){
            transform.position += new Vector3(-speed * Time.deltaTime, 0);
        }
        else if(player.transform.position.x > transform.position.x + 0.1f){
            transform.position += new Vector3(speed * Time.deltaTime, 0);
        }

        if (mode == ABILITY.SLOWSTRONG){
            SSTimer += Time.deltaTime;
            if (SSTimer > 6){
                SSTimer -= 6;
                Instantiate(magnetBomb, transform.position, transform.rotation);
            }
        }
    }
}

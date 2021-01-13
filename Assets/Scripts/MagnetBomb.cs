using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBomb : MonoBehaviour
{

    public float speed = 1f;
    public GameObject explosion;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, - speed * Time.deltaTime);

        GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile");
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < missiles.Length; i++){
            if(missiles[i].transform.position.x < transform.position.x){
                missiles[i].transform.position += new Vector3(2, 0, 0) * Time.deltaTime;
            }else{
                missiles[i].transform.position -= new Vector3(2, 0, 0) * Time.deltaTime;
            }
            if (missiles[i].transform.position.y > transform.position.y){
                missiles[i].transform.position += new Vector3(0, -5, 0) * Time.deltaTime;
            }
            if (Mathf.Abs(transform.position.x - missiles[i].transform.position.x) < 0.5f && Mathf.Abs(transform.position.y - missiles[i].transform.position.y) < 0.5f){
                Destroy(missiles[i]);
            }
        }

        if (player.transform.position.x < transform.position.x){
            player.transform.position += new Vector3(5.2f, 0, 0) * Time.deltaTime;
        }else{
            player.transform.position -= new Vector3(5.2f, 0, 0) * Time.deltaTime;
        }

        if (transform.position.y < -4){
            Instantiate(explosion,transform.position,transform.rotation);
            Destroy(gameObject);
        }
    }
}

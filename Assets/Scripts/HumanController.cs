using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public enum ABILITY {MISSILE, HEAT, SPEED, UBER};
    public ABILITY mode = ABILITY.MISSILE;

    public float speed = 5;

    public GameObject barrageMissile;
    float BMTimer = 0;

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0);
        if(transform.position.x > 6.5)
        {
            transform.position = new Vector3(6.49f, -4f);
        }
        if (transform.position.x < -6.5)
        {
            transform.position = new Vector3(-6.49f, -4f);
        }
        if (mode == ABILITY.MISSILE)
        {
            BMTimer += Time.deltaTime;
            if (BMTimer >= 0.2f)
            {
                BMTimer -= 0.2f;
                Instantiate(barrageMissile, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), 0), transform.rotation);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
    public enum ABILITY { MISSILE, HEAT, SPEED, UBER };

    public int missileMode = 0; //0 = Barrage, 1 = Homing
    public int heatMode = 0; //0 = Radiator, 1 = Coolant
    public int speedMode = 0; //0 = Ramming, 1 = Turbo
    public int uberMode = 0; //0 = Nano-Virus, 1 = Tesla, 2 = Final Hope

    public ABILITY mode = ABILITY.MISSILE;

    public int health = 100;

    public float speed = 5;

    public GameObject barrageMissile;
    public GameObject homingMissile;
    public GameObject plasmaProj;
    public GameObject coolantProj;
    public GameObject energyProj;
    public GameObject nanoVirusCloud;

    public GameObject heatField;
    public ParticleSystem coolantField;
    public SpriteRenderer armourOverlay;
    public SpriteRenderer finalHope;
    
    float MTimer = 0;
    float HTimer = 0;
    float heatFlux = 3.3f;
    float STimer = 0;
    public float swapCooldown = 0;
    float UTimer = 0;
    public float UCharge = 0;
    public float OCharge = 0;
    public bool overcharged = false;

    Rigidbody2D rigid2D;
    LineRenderer tesla;

    void Start()
    {
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        tesla = gameObject.GetComponent<LineRenderer>();
        if (mode != ABILITY.HEAT || heatMode != 0)
        {
            heatField.GetComponent<SpriteRenderer>().enabled = false;
        }
        if (mode != ABILITY.HEAT || heatMode != 1)
        {
            coolantField.Stop();
        }
        if (mode != ABILITY.SPEED || speedMode != 0)
        {
            armourOverlay.enabled = false;
        }
        if (mode == ABILITY.SPEED && speedMode == 1)
        {
            speed *= 2;
        }
        finalHope.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        rigid2D.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed, rigid2D.velocity.y);
        if (rigid2D.position.x > 6.5)
        {
            rigid2D.position = new Vector2(6.49f, rigid2D.position.y);
            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
        }
        if (rigid2D.position.x < -6.5)
        {
            rigid2D.position = new Vector2(-6.49f, rigid2D.position.y);
            rigid2D.velocity = new Vector2(0, rigid2D.velocity.y);
        }
        if (rigid2D.position.y < -4)
        {
            rigid2D.position = new Vector2(rigid2D.position.x, -4);
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, 0);
        }
        heatField.GetComponent<Rigidbody2D>().position = rigid2D.position;
        heatField.GetComponent<Rigidbody2D>().velocity = rigid2D.velocity;
        if (swapCooldown > 0)
        {
            swapCooldown -= Time.deltaTime;
        }
        if (Input.GetButtonDown("P1 Ability 1") && swapCooldown <= 0 && rigid2D.position.y < -3)
        {
            if (mode == ABILITY.HEAT)
            {
                heatField.GetComponent<SpriteRenderer>().enabled = false;
                coolantField.Stop();
            }
            if (mode == ABILITY.SPEED)
            {
                armourOverlay.enabled = false;
                if (speedMode == 1)
                {
                    speed /= 2;
                }
            }
            mode = ABILITY.MISSILE;
            swapCooldown = 20;
        }
        if (Input.GetButtonDown("P1 Ability 2") && swapCooldown <= 0 && rigid2D.position.y < -3)
        {
            if (mode == ABILITY.SPEED)
            {
                armourOverlay.enabled = false;
                if (speedMode == 1)
                {
                    speed /= 2;
                }
            }
            mode = ABILITY.HEAT;
            if (heatMode == 0)
            {
                heatField.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                coolantField.Play();
            }    
            swapCooldown = 20;
        }
        if (Input.GetButtonDown("P1 Ability 3") && swapCooldown <= 0)
        {
            if (mode == ABILITY.HEAT)
            {
                heatField.GetComponent<SpriteRenderer>().enabled = false;
                coolantField.Stop();
            }
            mode = ABILITY.SPEED;
            if (speedMode == 0)
            {
                armourOverlay.enabled = true;
            }
            else
            {
                speed *= 2;
            }
            swapCooldown = 20;
        }
        if(Input.GetButtonDown("P1 Uber"))
        {
            if (UCharge >= 60 && uberMode == 0)
            {
                if(OCharge == UCharge)
                {
                    overcharged = true;
                }
                UCharge -= 60;
                Instantiate(nanoVirusCloud, transform.position, transform.rotation);
            }
            if(UCharge >= 100 && uberMode == 1)
            {
                if (mode == ABILITY.HEAT)
                {
                    heatField.GetComponent<SpriteRenderer>().enabled = false;
                    coolantField.Stop();
                }
                if (mode == ABILITY.SPEED)
                {
                    armourOverlay.enabled = false;
                    if (speedMode == 1)
                    {
                        speed /= 2;
                    }
                }
                if (OCharge == UCharge)
                {
                    overcharged = true;
                    tesla.startWidth = 0.15f;
                    tesla.endWidth = 0.15f;
                }
                tesla.enabled = true;
                mode = ABILITY.UBER;
            }
        }
        switch (mode)
        {
            case ABILITY.MISSILE:
                {
                    MTimer += Time.deltaTime;
                    if (MTimer >= 0.2f && missileMode == 0)
                    {
                        MTimer -= 0.2f;
                        Instantiate(barrageMissile, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), 0), transform.rotation);
                    }
                    else if (MTimer >= 0.8f)
                    {
                        MTimer -= 0.8f;
                        Instantiate(homingMissile, transform.position, transform.rotation);
                    }
                    break;
                }
            case ABILITY.HEAT:
                {
                    HTimer += Time.deltaTime;
                    if (HTimer >= 1.5f)
                    {
                        HTimer -= 1.5f;
                        if (heatMode == 0)
                        {
                            GameObject proj = GameObject.Instantiate(plasmaProj, transform.position, transform.rotation);
                            proj.GetComponent<BasicProj>().directionSpeed = 3;
                            proj.GetComponent<BasicProj>().damage = 4;
                        }
                        else
                        {
                            GameObject proj = GameObject.Instantiate(coolantProj, transform.position, transform.rotation);
                            proj.GetComponent<BasicProj>().directionSpeed = 3;
                            proj.GetComponent<BasicProj>().damage = 4;
                        }
                    }
                    heatField.transform.localScale += Vector3.one * Time.deltaTime * heatFlux;
                    if (heatField.transform.localScale.x >= 10)
                    {
                        heatField.transform.localScale = new Vector3(9.9f, 9.9f, 9.9f);
                        heatFlux = -Mathf.Abs(heatFlux);
                    }
                    if (heatField.transform.localScale.x <= 2)
                    {
                        heatField.transform.localScale = new Vector3(2.1f, 2.1f, 2.1f);
                        heatFlux = Mathf.Abs(heatFlux);
                    }
                    break;
                }
            case ABILITY.SPEED:
                {
                    if (STimer < 4 || speedMode != 0)
                        STimer += Time.deltaTime;
                    if (STimer >= 4f && speedMode == 0)
                    {
                        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy Player");
                        if (rigid2D.position.x <= enemy.transform.position.x + 1 && rigid2D.position.x >= enemy.transform.position.x - 1)
                        {
                            STimer -= 8;
                            rigid2D.velocity = new Vector2(rigid2D.velocity.x, 10);
                        }
                    }
                    if (STimer >= 0.8f && speedMode == 1)
                    {
                        STimer -= 0.8f;
                        {
                            GameObject proj1 = GameObject.Instantiate(energyProj, transform.position, transform.rotation);
                            proj1.GetComponent<BasicProj>().directionSpeed = 6;
                            proj1.GetComponent<BasicProj>().damage = 2;
                            GameObject proj2 = GameObject.Instantiate(energyProj, transform.position, transform.rotation);
                            proj2.GetComponent<BasicProj>().directionSpeed = 6;
                            proj2.GetComponent<BasicProj>().damage = 2;
                            proj2.transform.rotation = Quaternion.AngleAxis(30, Vector3.forward);
                            GameObject proj3 = GameObject.Instantiate(energyProj, transform.position, transform.rotation);
                            proj3.GetComponent<BasicProj>().directionSpeed = 6;
                            proj3.GetComponent<BasicProj>().damage = 2;
                            proj3.transform.rotation = Quaternion.AngleAxis(-30, Vector3.forward);
                        }
                    }
                    if (rigid2D.velocity.y > 0 && rigid2D.position.y > 4.5f)
                    {
                        rigid2D.velocity = new Vector2(rigid2D.velocity.x, -3);
                    }
                    break;
                }
            case ABILITY.UBER:
                {
                    if(uberMode == 1)
                    {
                        UCharge -= Time.deltaTime * 10f;
                        UTimer += Time.deltaTime;
                        GameObject alien = GameObject.FindGameObjectWithTag("Enemy Player");
                        Vector3 dir = alien.transform.position - transform.position;
                        float dist = Vector3.Distance(alien.transform.position, transform.position);
                        tesla.SetPosition(0, transform.position);
                        Vector3 pos1 = (transform.position + dir.normalized * dist / 4) + Vector3.Cross(dir.normalized, Vector3.forward) * Random.Range(-1f, 1f);
                        tesla.SetPosition(1, pos1);
                        Vector3 pos2 = (transform.position + dir.normalized * 2 * dist / 4) + Vector3.Cross(dir.normalized, Vector3.forward) * Random.Range(-1f, 1f);
                        tesla.SetPosition(2, pos2);
                        Vector3 pos3 = (transform.position + dir.normalized * 3 * dist / 4) + Vector3.Cross(dir.normalized, Vector3.forward) * Random.Range(-1f, 1f);
                        tesla.SetPosition(3, pos3);
                        tesla.SetPosition(4, alien.transform.position);
                        if (UTimer >= 0.2f)
                        {
                            UTimer -= 0.2f;
                            alien.GetComponent<AlienDebug>().health -= 2;
                        }
                    }
                    if (uberMode == 2)
                    {
                        UCharge -= Time.deltaTime * 10f;
                        UTimer += Time.deltaTime;
                        while (UTimer >= 0.1f)
                        {
                            int projChance = Random.Range(0, 43);
                            UTimer -= 0.1f;
                            if (projChance <= 23)
                            {
                                Instantiate(barrageMissile, transform.position + new Vector3(Random.Range(-0.2f, 0.2f), 0), transform.rotation);
                            }
                            else if (projChance <= 29)
                            {
                                Instantiate(homingMissile, transform.position, transform.rotation);
                            }
                            else if (projChance <= 36)
                            {
                                GameObject proj = GameObject.Instantiate(plasmaProj, transform.position, transform.rotation);
                                proj.GetComponent<BasicProj>().directionSpeed = 3;
                                proj.GetComponent<BasicProj>().damage = 4;
                            }
                            else if (projChance <= 42)
                            {
                                GameObject proj1 = GameObject.Instantiate(energyProj, transform.position, transform.rotation);
                                proj1.GetComponent<BasicProj>().directionSpeed = 6;
                                proj1.GetComponent<BasicProj>().damage = 2;
                                GameObject proj2 = GameObject.Instantiate(energyProj, transform.position, transform.rotation);
                                proj2.GetComponent<BasicProj>().directionSpeed = 6;
                                proj2.GetComponent<BasicProj>().damage = 2;
                                proj2.transform.rotation = Quaternion.AngleAxis(30, Vector3.forward);
                                GameObject proj3 = GameObject.Instantiate(energyProj, transform.position, transform.rotation);
                                proj3.GetComponent<BasicProj>().directionSpeed = 6;
                                proj3.GetComponent<BasicProj>().damage = 2;
                                proj3.transform.rotation = Quaternion.AngleAxis(-30, Vector3.forward);
                            }
                        }
                    }
                    if(UCharge <= 0)
                    {
                        tesla.enabled = false;
                        if (uberMode == 1)
                        {
                            if (overcharged)
                            {
                                tesla.startWidth = 0.075f;
                                tesla.endWidth = 0.075f;
                                overcharged = false;
                                GameObject alien = GameObject.FindGameObjectWithTag("Enemy Player");
                                alien.SendMessage("Systems Damaged");
                            }
                        }
                        if(uberMode == 2)
                        {
                            PlayerDied();
                        }
                        int randMode = Random.Range(0, 3);
                        swapCooldown = 20;
                        switch (randMode)
                        {
                            case 0:
                                {
                                    mode = ABILITY.MISSILE;
                                    break;
                                }
                            case 1:
                                {
                                    mode = ABILITY.HEAT;
                                    if (heatMode == 0)
                                    {
                                        heatField.GetComponent<SpriteRenderer>().enabled = true;
                                    }
                                    else
                                    {
                                        coolantField.Play();
                                    }
                                    break;
                                }
                            case 2:
                                {
                                    mode = ABILITY.SPEED;
                                    if (speedMode == 0)
                                    {
                                        armourOverlay.enabled = true;
                                    }
                                    else
                                    {
                                        speed *= 2;
                                    }
                                    break;
                                }
                        }

                    }
                    break;
                }
        }
        UCharge += Time.deltaTime * 0.1f;
        if(uberMode == 0 && UCharge > 60)
        {
            OCharge += UCharge - 60;
            UCharge = 60;
        }
        if(uberMode == 1 && UCharge > 100)
        {
            OCharge += UCharge - 100;
            UCharge = 100;
        }
        if(OCharge > UCharge)
        {
            OCharge = UCharge;
        }
        if (health <= 0)
        {
            if (uberMode == 2)
            {
                if (mode == ABILITY.HEAT)
                {
                    heatField.GetComponent<SpriteRenderer>().enabled = false;
                    coolantField.Stop();
                }
                if (mode == ABILITY.SPEED)
                {
                    armourOverlay.enabled = false;
                    if (speedMode == 1)
                    {
                        speed /= 2;
                    }
                }
                mode = ABILITY.UBER;
                finalHope.enabled = true;
            }
            else
            {
                PlayerDied();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy Player"))
        {
            rigid2D.velocity = new Vector2(rigid2D.velocity.x, -3);
            collision.gameObject.GetComponent<AlienDebug>().health -= 30;
            UCharge += 15;
            health -= 10;
        }
    }

    private void PlayerDied()
    {

    }
}

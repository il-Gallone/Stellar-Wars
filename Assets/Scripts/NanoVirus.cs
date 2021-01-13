using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NanoVirus : MonoBehaviour
{
    ParticleSystem particleSystem;
    float maxSize;
    public float minSpeed = 0.1f;
    float virusTimer = 0;
    float virusTotal = 0;
    ParticleSystem.ShapeModule shape;
    ParticleSystem.MainModule main;
    Rigidbody2D rigid2D;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = gameObject.GetComponent<ParticleSystem>();
        rigid2D = gameObject.GetComponent<Rigidbody2D>();
        maxSize = particleSystem.shape.radius;
        main = particleSystem.main;
        shape = particleSystem.shape;
        shape.radius = 0.5f;

    }

    // Update is called once per frame
    void Update()
    {
        if(shape.radius < maxSize)
        {
            shape.radius += Time.deltaTime;
            main.startSpeedMultiplier -= Time.deltaTime / (maxSize*2); 
            if (shape.radius >= maxSize)
            {
                shape.radius = maxSize;
                shape.randomDirectionAmount = 1;
            }
            if(main.startSpeedMultiplier <= minSpeed)
            {
                main.startSpeedMultiplier = minSpeed;
            }
        }
        GameObject alien = GameObject.FindGameObjectWithTag("Enemy Player");
        rigid2D.velocity = (alien.transform.position - transform.position).normalized;
        if(Vector3.Distance(alien.transform.position, transform.position) <= 0.1f)
        {
            rigid2D.velocity = Vector2.zero;
            transform.position = alien.transform.position;
        }
        if(virusTotal >= 20)
        {
            particleSystem.Stop();
            if(virusTimer >= 8)
            {
                Destroy(gameObject);
            }
        }
        else if(virusTimer >= 2)
        {
            virusTimer -= 2;
            int effectChance = Random.Range(0, 4);
            switch (effectChance)
            {
                case 0:
                    {
                        alien.SendMessage("WeaponsFailure");
                        break;
                    }
                case 1:
                    {
                        alien.SendMessage("DefensesDown");
                        break;
                    }
                case 2:
                    {
                        alien.SendMessage("ControlsInverted");
                        break;
                    }
                case 3:
                    {
                        alien.SendMessage("SystemDegradation");
                        break;
                    }
            }
            HumanController human = GameObject.FindGameObjectWithTag("Player").GetComponent<HumanController>();
            if (human.overcharged)
            {
                human.health += 5;
            }

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        virusTimer += Time.deltaTime;
        virusTotal += Time.deltaTime;
    }
}

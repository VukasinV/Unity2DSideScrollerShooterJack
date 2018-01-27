using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolAi : MonoBehaviour
{
    bool inRange = false;
    public Animation animation;
    public int direction;
    CharacterWalk player;
    bool patroling;
    Collider2D[] collider;
    public bool chase = false;
    public Animator anim;
    public Transform[] patrolPoints;
    public float speed;
    Transform currentPatrolPoint;
    int currentPatrolIndex;
    int waitTime;
    public Transform target;
    Transform targetPos;
    int targetHp;
    Vector3 targetDir;
    public float chaseRange;
    public int hp;
    bool dead = false;
    public float attackRange;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;
    public GameObject projectile;
    public float webForce;
    int horizontal;
    public float awarenessRange;
    public float distanceToTarget;
    public Transform raycastPoint;

    private bool right;


    // Use this for initialization
    void Start()
    {

        currentPatrolIndex = 0;
        currentPatrolPoint = patrolPoints[currentPatrolIndex];
        collider = GetComponents<Collider2D>();
        player = GetComponent<CharacterWalk>();
    }


    // Update is called once per frame
    void Update()
    {

        //proverava rastojanje do igraca
        if (target != null)
        {
            targetHp = target.GetComponent<CharacterWalk>().hp;
            distanceToTarget = Vector3.Distance(transform.position, target.position);
        }
        else distanceToTarget = 10;

        //provera da li je "enemy" svestan igraca - if not then patrol
        if (distanceToTarget > awarenessRange && !dead)
        {
            Patrol();
        }


        //ako je igrac u vidnom polju neprijatelja - juri
        if (distanceToTarget < awarenessRange && distanceToTarget > attackRange && target != null && !dead)
        {
            Chase();
        }

        if (targetHp <= 0)
        {
            target = null;
        }

        if (hp <= 0)
        {
            anim.SetInteger("Walking", 0);
            anim.SetBool("Jump", false);
            anim.SetBool("Dead", true);
            dead = true;
            var col = GetComponents<BoxCollider2D>();
            for (int i = 0; i < col.Length; i++)
            {
                col[i].enabled = false;
            }
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponentInChildren<CapsuleCollider2D>().enabled = false;
            speed = 0;
            transform.Translate(Vector3.left * Time.deltaTime * speed);

        }

    }

    public void Patrol()
    {
        inRange = false;
        patroling = true;
        chase = false;
        speed = 1;
        if (anim.GetBool("Attacking") == true) anim.SetBool("Attacking", false);
        anim.SetInteger("Walking", 1);
        //provera da li smo dosli do patrolPoint-a
        if (Vector3.Distance(transform.position, currentPatrolPoint.position) < .5f)
        {
            //stigli smo do patrolPointa - idi do sledeceg

            //provera da li imamo jos patrolPointova - ako nema, idi na pocetak
            if (currentPatrolIndex + 1 < patrolPoints.Length)
            {
                currentPatrolIndex++;
            }
            else
            {
                currentPatrolIndex = 0;
            }
            currentPatrolPoint = patrolPoints[currentPatrolIndex];
        }

        //okreni se ka trenutnom patrolPointu
        //trazenje smera vektora koji ima smer ka patrolPointu
        Vector3 patrolPointDir = currentPatrolPoint.position - transform.position;
        Quaternion newRotation;
        //saznaj da li je patrolPoint levo ili desno od "enemy"-a
        if (patrolPointDir.x < 0f)
        {
            newRotation = Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = newRotation;
            transform.Translate(-transform.right * Time.deltaTime * speed, Space.Self);
            right = false;
        }
        if (patrolPointDir.x > 0f)
        {
            newRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = newRotation;
            transform.Translate(transform.right * Time.deltaTime * speed, Space.Self);
            right = true;
        }

    }
    void Chase()
    {
        inRange = true;
        patroling = false;
        chase = true;
        anim.SetInteger("Walking", 1);
        anim.SetBool("Attacking", false);
        Vector3 targetDir = target.position - transform.position;
        Quaternion newRotation;
        if (targetDir.x < 0f)
        {
            newRotation = Quaternion.Euler(0f, 180f, 0f);
            transform.rotation = newRotation;
            transform.Translate(-transform.right * Time.deltaTime * speed, Space.Self);
            right = false;
        }
        if (targetDir.x > 0f)
        {
            newRotation = Quaternion.Euler(0f, 0f, 0f);
            transform.rotation = newRotation;
            transform.Translate(transform.right * Time.deltaTime * speed, Space.Self);
            right = true;
        }
        anim.SetInteger("Walking", 1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (patroling && (collision.collider.tag == "Enemy" || (collision.collider.tag == "EnemyDagger")))
        {
            for (int i = 0; i < collider.Length; i++)
            {
                Physics2D.IgnoreCollision(collision.collider, collider[i], true);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {

        patroling = false;
        if (col.tag == "Player" && Time.time > lastAttackTime + attackDelay && target != null && !dead)
        {
            inRange = true;
            anim.SetInteger("Walking", 0);
            anim.SetBool("Attacking", true);
            Quaternion knifeRotation;
            Quaternion newRotation;
            if (col.tag == "Player" && Time.time > lastAttackTime + attackDelay)
            {

                knifeRotation = Quaternion.Euler(0f, 0f, 90f);
                projectile.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                targetDir = (target.position - transform.position) * webForce;

                GameObject newWeb = Instantiate(projectile, transform.position, transform.rotation);


                if (targetDir.x > 0f)
                {
                    newWeb.GetComponent<Rigidbody2D>().AddRelativeForce(targetDir);


                    projectile.GetComponent<Animator>().SetBool("Throwing", true);
                    newRotation = Quaternion.Euler(0f, 0f, 0f);
                    transform.rotation = newRotation;

                }
                else
                {
                    newWeb.GetComponent<Rigidbody2D>().AddRelativeForce(targetDir);
                    projectile.GetComponent<Animator>().SetBool("Throwing", true);
                    newRotation = Quaternion.Euler(0f, 180f, 0f);
                    transform.rotation = newRotation;
                }
                lastAttackTime = Time.time;
            }
            else
            {
                Patrol();
            }

        }

    }

    public void takeDamage(int damage)
    {
        print(target.GetComponent<CharacterWalk>().dir.x);
        if (inRange && (transform.rotation.y == 0 && target.GetComponent<CharacterWalk>().dir.x == 1))
        {
            Debug.Log("ISTE SU IM ROTACIJE");
            return;
        }
        if (inRange && (transform.rotation.y != 0 && target.GetComponent<CharacterWalk>().dir.x == -1))
        {
            Debug.Log("ISTE SU IM ROTACIJE");
            return;
        }
        if (hp > 0)
        {
            hp -= damage;
            Debug.Log("ENEMY HP: " + hp);
        }
    }
}

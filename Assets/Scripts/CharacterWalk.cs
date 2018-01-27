using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWalk : MonoBehaviour
{
    public Vector3 dir;
    private Transform pBody;
    public bool OnLadder;
    public Animation anim;
    Weapon weapon;

    [SerializeField]
    private float climbSpeed;
    private float climbVelocity;
    private float gravityStore;
    private Rigidbody2D pRigid;
    public RestardGame theGameManager;
    public Animator animator;
    public int hp;
    int flag = 0;
    public GameObject deathScreenUI;

    public float speed = 1f;

    public float jump;

    bool grounded = false;

    public bool _Direction;
    // Update is called once per frame

    private void Start()
    {
        weapon = GetComponentInChildren<Weapon>();
        theGameManager = FindObjectOfType<RestardGame>();
        pRigid = GetComponent<Rigidbody2D>();
        gravityStore = pRigid.gravityScale;
    }

    void Awake()

    {
        pBody = transform.FindChild("CharacterRig");
        pRigid = gameObject.GetComponent<Rigidbody2D>();

        _Direction = true;


    }
    void Update()
    {
        dir = pBody.localScale;
        print(dir);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dead"))
        {
            deathScreenUI.SetActive(true);
        }

        
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jack_Jump"))
        {

            grounded = true;
        }
        else grounded = false;

        if (Input.GetAxis("Horizontal") != 0)
        {


            if (Input.GetAxis("Horizontal") < 0)
            {

                transform.Translate(Vector3.left * Time.deltaTime * speed);
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jack_Jump"))
                    animator.SetInteger("Walking", 1);
            }
            if (Input.GetAxis("Horizontal") > 0)
            {

                transform.Translate(Vector3.right * Time.deltaTime * speed);
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jack_Jump"))
                    animator.SetInteger("Walking", 1);
            }
        }
        else
        {
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Jack_Jump"))
                animator.SetInteger("Walking", 0);
        }


        if (hp <= 0 && flag == 0)
        {
            animator.SetInteger("Walking", 0);
            animator.SetBool("Jump", false);
            animator.SetBool("Dead", true);
            flag = 1;


            speed = 0;
            transform.Translate(Vector3.left * Time.deltaTime * speed);

        }

        if (flag == 1)
        {

        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) && hp > 0)
        {
            if (grounded && hp > 0 && !animator.GetCurrentAnimatorStateInfo(0).IsName("Jack_Jump") && animator.GetBool("Jump") == false)
            {
                animator.SetBool("Jump", true);
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, jump);
                grounded = false;
            }
        }

        if (OnLadder == true)
        {
            pRigid.gravityScale = 0;
            climbVelocity = climbSpeed * Input.GetAxisRaw("Vertical");

            if (Input.GetKey(KeyCode.W))
            {
                pRigid.velocity = new Vector2(pRigid.velocity.x, climbVelocity);

            }
            else if (Input.GetKey(KeyCode.S))
            {
                pRigid.velocity = new Vector2(pRigid.velocity.x, -climbSpeed);

            }
            else
            {
                pRigid.velocity = new Vector2(0, 0.2f);
            }
        }

        if (!OnLadder == false)
        {
            pRigid.gravityScale = gravityStore;
        }
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Mapa" && animator.GetBool("Jump") == true)
        {
            animator.SetBool("Jump", false);
            grounded = true;
        }
        
        if (col.tag == "Death")
        {
            hp = 0;
        }
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "MovingPlatform")
        {
            Debug.Log("Sada stoji na platformi");
            transform.parent = col.transform;
            animator.SetInteger("Walking", 0);
            grounded = true;
            animator.SetBool("Jump",false);
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.gameObject.tag == "MovingPlatform")
        {
            transform.parent = null;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag != "Enemy" && col.tag != "EnemyDagger")
            grounded = false;
    }

    public void Flipp()
    {
        if (hp > 0)
        {
            
            _Direction = !_Direction;

            Vector3 theScale = pBody.localScale;

            theScale.x *= -1f;
            
            pBody.localScale = theScale;
        }
    }

    public void takeDamage(int damage)
    {
        if (hp > 0)
        {
            hp -= damage;
            //Debug.Log(hp);
        }

    }
}

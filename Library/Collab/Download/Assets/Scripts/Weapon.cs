using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float bulletForce;
    public float fireRate = 0;
    public float Damage = 10;
    public LayerMask whatToHit;

    public Transform BulletTrailPrefab;
    public Transform MuzzleFlashPrefab;
    float timeToSpawnEffect = 0;
    public float effectSpawnRate = 10;
    float timeToFire = 0;
    Transform firePoint;
    CharacterWalk player;
    int playerHp;

    public AudioSource audioSource;
    private AudioClip audioClip;
    public GameObject weaponPrefab;

    void playShootSound()
    {
        audioSource = weaponPrefab.GetComponent<AudioSource>();
        audioClip = audioSource.clip;
        audioSource.PlayOneShot(audioClip);
    }

    // Use this for initialization
    void Awake()
    {
        firePoint = transform.FindChild("FirePoint");
        if (firePoint == null)
        {

        }

        player = GetComponentInParent<CharacterWalk>();
    }

    // Update is called once per frame
    void Update()
    {
        playerHp = player.hp;

        if (fireRate == 0 && playerHp > 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire && playerHp > 0)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        Vector2 mousePosition = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePosition - firePointPosition, 10, whatToHit);

        if (Time.time >= timeToSpawnEffect)
        {
            Effect();
            timeToSpawnEffect = Time.time + 1 / effectSpawnRate;
        }

        Debug.DrawLine(firePointPosition, (mousePosition - firePointPosition) * 100, Color.cyan);

        if (hit.collider != null)
        {
            Debug.DrawLine(firePointPosition, hit.point, Color.red);
            Debug.Log("We hit" + hit.collider.name + " and did " + Damage + " damage.");
            if (hit.collider.tag == "Enemy")
            {
                hit.transform.SendMessage("takeDamage", Damage);
            }
        }
    }

    void Effect()
    {
        playShootSound();
        if (player._Direction == false)
        {
            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
            Vector3 lookPos = mousePosition - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(BulletTrailPrefab, firePoint.position, newRotation);
        }
        else
        {
            Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, Camera.main.ScreenToWorldPoint(Input.mousePosition).z);
            Vector3 lookPos = mousePosition - transform.position;
            float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
            Quaternion newRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Instantiate(BulletTrailPrefab, firePoint.position, newRotation);
        }
        Transform clone = Instantiate(MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
        clone.parent = firePoint;
        float size = Random.Range(0.1f, 0.3f);
        clone.localScale = new Vector3(size, size, size);
        Destroy(clone.gameObject, 0.02f);
    }
}
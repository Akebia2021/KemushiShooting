using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] float shotCounter;
    [SerializeField] float minTimeBetweenShots = 0.2f;
    [SerializeField] float maxTimeBetweenShots = 3f;
    [SerializeField] GameObject projectile;
    [SerializeField] float bulletSpeed = 3f;
    [SerializeField] GameObject explosion;

    [Header("Stats")]
    [SerializeField] float health = 100;

    [Header("SFX")]
    [SerializeField] AudioClip fire;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip recieveHit;



    // Start is called before the first frame update
    void Start()
    {
        ResetShotCounter();
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndShoot();

    }

    //shotCounterから毎フレームごとの経過時間(deltaTime)
    //を引いて0になったら弾を発射
    private void CountDownAndShoot()
    {
        shotCounter -= Time.deltaTime;
        if(shotCounter <= 0f)
        {
            Fire();
            ResetShotCounter();
        }
    }
    private void ResetShotCounter()
    {
        shotCounter = UnityEngine.Random.Range(minTimeBetweenShots, maxTimeBetweenShots);
    }


    private void Fire()
    {
        GameObject bullet = Instantiate(projectile,
               transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity =
            new Vector2(0, -bulletSpeed);
        AudioSource.PlayClipAtPoint(fire, transform.position, 1f);
    }

    //DamageDealerを複製し、このObjectのHealthをへらす
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }

    private void ProcessHit(DamageDealer damageDealer)
    {
        AudioSource.PlayClipAtPoint(recieveHit, transform.position, 1f);
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            Destroy(gameObject);
            var effect = Instantiate(explosion, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(death, transform.position, 1f);
            Destroy(effect, 1f);
        }
    }
}

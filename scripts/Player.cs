using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //HeaderをつければInspectorでの表示が見やすくなる
    [Header("Player Movement")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float padding = 0.2f;

    [Header("Projectile")]
    [SerializeField] private GameObject projectile;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletFirePeriod = 0.1f;

    [Header("Health")]
    [SerializeField] private float health = 500;

    [Header("SFX")]
    [SerializeField] AudioClip fire;
    [SerializeField] AudioClip death;

    Coroutine firingCoroutine;

    private float xmin;
    private float xmax;
    private float ymin;
    private float ymax;

    //transform componentへの参照(ポインタ)
    //tr はポインタ。trへの代入は元のComponentを直接書き換える。
    Transform tr;

    void Start()
    {
        tr = GetComponent<Transform>();
     
        StartUpMoveBoundaries();

    }

    private void StartUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        //ビューポート（１～ー１）をワールド座標に変換 
        xmin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xmax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;
        ymin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        ymax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
  
    }

    void Update()
    {
        Move();
        Fire();

        
    }

    private void Fire()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            firingCoroutine = StartCoroutine(FireContinuously());
           
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    private void Move()
    {
        //Time.deltaTimeでFrameレートに関わらず同じ挙動を表現。
        var deltaX = Input.GetAxis("Horizontal")*Time.deltaTime*moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        var newXPos = transform.position.x + deltaX;
        var newYPos = transform.position.y + deltaY;

        newXPos = Mathf.Clamp(newXPos, xmin, xmax);
        newYPos = Mathf.Clamp(newYPos, ymin, ymax);
        tr.position = new Vector2(newXPos, newYPos);
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {

            GameObject bullet = Instantiate(projectile,
                transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity =
                new Vector2(0, bulletSpeed);
            //play sound effect
            AudioSource.PlayClipAtPoint(fire, transform.position, 1f);
            yield return new WaitForSeconds(bulletFirePeriod);
        }
    }

    //衝突判定(弾)
    private void OnTriggerEnter2D(Collider2D other)
    {
        DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();
        if (!damageDealer) { return; }
        ProcessHit(damageDealer);
    }
    //ヒット処理とObject破壊
    private void ProcessHit(DamageDealer damageDealer)
    {
        health -= damageDealer.GetDamage();
        damageDealer.Hit();
        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(death, transform.position, 1f);
            Destroy(gameObject);

        }
    }
    

}

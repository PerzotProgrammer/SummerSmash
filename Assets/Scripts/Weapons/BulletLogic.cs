using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private Rigidbody2D Rb;
    private GameObject Player;
    private GameObject Target;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Target = Player.GetComponentInChildren<WeaponLogic>().GetTarget();
        Move();
    }

    private void FixedUpdate()
    {
        CheckDistToPlayer();
    }

    private void Move()
    {
        Vector3 movementVector = Target.transform.position - transform.position;
        Rb.velocity = new Vector2(movementVector.x, movementVector.y).normalized * speed;
    }

    private void CheckDistToPlayer()
    {
        float distance = (Player.transform.position - transform.position).magnitude;
        if (distance > 25) Destroy(gameObject);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<EntityBase>().InflictDamage(damage);
        }
    }
}
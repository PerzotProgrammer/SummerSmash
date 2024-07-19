using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour
{
    [SerializeField] private float speed;
    private float DistanceToPlayer;
    private Rigidbody2D Rb;
    private GameObject Player;
    private GameObject Target;

    void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Target = Player.GetComponentInChildren<WeaponLogic>().GetTarget();
        Debug.Log(Target);
        Move();
    }

    void Update()
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
        if (distance > 25)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemies"))
        {
            Destroy(gameObject);
        }
    }
}
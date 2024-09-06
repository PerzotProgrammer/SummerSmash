using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float speed;
    [SerializeField] protected float despawnDistance;
    protected Rigidbody2D Rb;
    protected GameObject Player;
    protected GameObject Target;

    private void Start()
    {
        Rb = GetComponent<Rigidbody2D>();
        Player = GameObject.Find("Player");
        Target = Player.GetComponentInChildren<WeaponLogic>().GetTarget();
        Move();
    }

    private void Move()
    {
        Vector3 movementVector = Target.transform.position - transform.position;
        Rb.velocity = new Vector2(movementVector.x, movementVector.y).normalized * speed;
    }

    protected void CheckDistToPlayer()
    {
        float distance = (Player.transform.position - transform.position).magnitude;
        if (distance > despawnDistance) Destroy(gameObject);
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);
}
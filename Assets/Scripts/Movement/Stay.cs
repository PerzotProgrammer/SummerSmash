using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stay : MonoBehaviour
{
    private Rigidbody2D Rb;

    void Start()
    {
        Rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Rb.velocity = new Vector2(0, 0);
    }
}
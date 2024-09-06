using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBulletLogic : BulletBase
{
    private bool AnimState;
    
    private void FixedUpdate()
    {
        CheckDistToPlayer();
        if (!AnimState) StartCoroutine(nameof(RotateAnimCoroutine));
    }
    
    private IEnumerator RotateAnimCoroutine()
    {
        AnimState = true;
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        yield return new WaitForSeconds(0.5f);
        AnimState = false;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemies"))
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<EntityBase>().InflictDamage(damage, true);
        }
    }
}
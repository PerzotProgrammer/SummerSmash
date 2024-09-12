using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class WeaponLogic : MonoBehaviour
{
    [SerializeField] private GameObject[] weapons;
    private GameObject CurrentWeapon;
    private GameObject ClosestEnemy;
    private float ClosestDistance;
    private bool IsFacingLeft;
    private int CurrentWeaponIndex;
    public static int[] MagazineState;


    private void Start()
    {
        CurrentWeaponIndex = 0;
        SetWeapon(CurrentWeaponIndex);
        InitMagazine();
    }

    private void Update()
    {
        FindTarget();
        MoveWeapon();

        if (Input.anyKeyDown) ChangeWeaponCheck();
    }

    private void FindTarget()
    {
        ClosestDistance = Mathf.Infinity;
        ClosestEnemy = null;
        foreach (EnemiesLogic enemy in EntityBase.Enemies)
        {
            if (!enemy) continue;
            float distance = (enemy.transform.position - transform.position).sqrMagnitude;
            if (distance < ClosestDistance)
            {
                ClosestDistance = distance;
                ClosestEnemy = enemy.gameObject;
            }
        }
    }

    private void MoveWeapon()
    {
        if (!ClosestEnemy) return;
        // Sposób sprawdzenia, czy obiekt istnieje w Unity. Cs-owe == null lub is null nie działa poprawnie. 
        Vector2 direction = ClosestEnemy!.transform.position - transform.position;
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(rotation, Vector3.forward);
        if (((rotation > 90 || rotation < -90) && !IsFacingLeft) || // Obrót w lewo
            ((rotation > -90 && rotation < 90) && IsFacingLeft)) // Obrót w prawo
            FlipTexture();
        // Kąty broni:
        // góra: 90
        // prawo: 0
        // dół: -90
        // lewo: -180
    }

    private void FlipTexture()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * -1, transform.localScale.z);
        IsFacingLeft = !IsFacingLeft;
    }

    private void SetWeapon(int weaponIndex)
    {
        CurrentWeaponIndex = weaponIndex;
        if (CurrentWeapon is not null) Destroy(CurrentWeapon);
        CurrentWeapon = Instantiate(weapons[weaponIndex], transform);
        CurrentWeapon.transform.localPosition = new Vector3(CurrentWeapon.transform.localPosition.x + 0.6f,
            CurrentWeapon.transform.localPosition.y, 0);
    }

    private void ChangeWeaponCheck()
    {
        // !!! SetWeapon() przyjmuje index więc musi być o jeden mniejszy
        if (Input.GetKeyDown("1")) SetWeapon(0);
        else if (Input.GetKeyDown("2")) SetWeapon(1);
    }

    private void InitMagazine()
    {
        MagazineState = new int[weapons.Length];
        for (int i = 0; i < MagazineState.Length; i++) MagazineState[i] = -1;
    }

    public GameObject GetTarget()
    {
        return ClosestEnemy;
    }

    public int GetWeaponIndex()
    {
        return CurrentWeaponIndex;
    }
}
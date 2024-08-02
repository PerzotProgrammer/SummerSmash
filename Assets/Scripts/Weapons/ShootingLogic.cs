using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLogic : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private int maxMagazineSize;
    [SerializeField] private float reloadTime;
    [SerializeField] private float shootingCooldown;
    private WeaponLogic WeaponLogic;
    private int LoadedBullets;
    private bool CanShoot;
    private bool IsReloading;

    private void Start()
    {
        WeaponLogic = GameObject.FindGameObjectWithTag("WeaponParent").GetComponent<WeaponLogic>();
        LoadedBullets = maxMagazineSize;
        CanShoot = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && WeaponLogic.GetTarget() is not null) StartCoroutine(nameof(Shoot));
        if (Input.GetButtonDown("Fire2") && LoadedBullets != maxMagazineSize) StartCoroutine(nameof(Reload));
        // TODO: PRZEŁADOWANIE NA RAZIE NA PRAWYM MYSZY
    }


    private IEnumerator Shoot()
    {
        if (CanShoot && LoadedBullets > 0 && !IsReloading)
        {
            LoadedBullets -= 1;
            Instantiate(bullet, transform.position, transform.rotation);
            CanShoot = false;
            yield return new WaitForSeconds(shootingCooldown);
            CanShoot = true;
        }
    }

    private IEnumerator Reload()
    {
        IsReloading = true;
        yield return new WaitForSeconds(reloadTime);
        LoadedBullets = maxMagazineSize;
        IsReloading = false;
    }

    public int GetMagazineSize()
    {
        return maxMagazineSize;
    }

    public int GetLoadedBullets()
    {
        return LoadedBullets;
    }

    public bool IsOnReload()
    {
        return IsReloading;
    }
}
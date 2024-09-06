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
        WeaponLogic = GameObject.Find("WeaponParent").GetComponent<WeaponLogic>();
        LoadedBullets = maxMagazineSize;
        CanShoot = true;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && WeaponLogic.GetTarget() is not null) StartCoroutine(nameof(ShootCoroutine));
        if (Input.GetKeyDown("r") && LoadedBullets != maxMagazineSize) StartCoroutine(nameof(ReloadCoroutine));
    }


    private IEnumerator ShootCoroutine()
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

    private IEnumerator ReloadCoroutine()
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

    public void SetLoadedBullets(int bulletsAmount)
    {
        LoadedBullets = bulletsAmount;
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
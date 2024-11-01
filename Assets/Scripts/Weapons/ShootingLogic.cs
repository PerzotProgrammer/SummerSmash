using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLogic : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float reloadTime;
    [SerializeField] private float shootingCooldown;
    [SerializeField] private int startingMagazineSize;
    [SerializeField] private bool isAutomatic;
    public int MaxMagazineSize { get; private set; }
    public int LoadedBullets { get; private set; }
    public bool IsReloading { get; private set; }
    private WeaponLogic WeaponLogic;
    private AudioSource AudioSource;
    private bool CanShoot;
    private int WeaponIndex;

    private void Start()
    {
        MaxMagazineSize = startingMagazineSize;
        WeaponLogic = GameObject.Find("WeaponParent").GetComponent<WeaponLogic>();
        AudioSource = GetComponent<AudioSource>();
        WeaponIndex = WeaponLogic.CurrentWeaponIndex;
        if (WeaponLogic.MagazineState[WeaponIndex] == -1) WeaponLogic.MagazineState[WeaponIndex] = MaxMagazineSize;
        LoadedBullets = WeaponLogic.MagazineState[WeaponIndex];
        CanShoot = true;
    }

    private void Update()
    {
        bool isActionPressed = isAutomatic ? Input.GetButton("Fire1") : Input.GetButtonDown("Fire1");

        if (isActionPressed && WeaponLogic.Target) StartCoroutine(nameof(ShootCoroutine), reloadTime);
        if (Input.GetKeyDown("r") && LoadedBullets != MaxMagazineSize)
            StartCoroutine(nameof(ReloadCoroutine), reloadTime);
    }


    private IEnumerator ShootCoroutine()
    {
        if (CanShoot && LoadedBullets > 0 && !IsReloading)
        {
            LoadedBullets -= 1;
            Instantiate(bullet, transform.position, transform.rotation);
            AudioSource.Play();
            CanShoot = false;
            yield return new WaitForSeconds(shootingCooldown);
            CanShoot = true;
        }
    }

    private IEnumerator ReloadCoroutine(float time)
    {
        IsReloading = true;
        yield return new WaitForSeconds(time);
        LoadedBullets = MaxMagazineSize;
        IsReloading = false;
    }

    private void OnDestroy()
    {
        WeaponLogic.MagazineState[WeaponIndex] = LoadedBullets;
    }

    public void InstaReload()
    {
        StartCoroutine(nameof(ShootCoroutine), 0);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingLogic : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private int maxMagazineSize;
    [SerializeField] private float reloadTime;
    private WeaponLogic WeaponLogic;
    private int LoadedBullets;
    private float TimeElapsed;

    private void Start()
    {
        WeaponLogic = GameObject.FindGameObjectWithTag("WeaponParent").GetComponent<WeaponLogic>();
        LoadedBullets = maxMagazineSize;
        reloadTime = -reloadTime; // Flip znaku, aby łatwiej się ustawiało
    }

    private void Update()
    {
        TimeElapsed += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && WeaponLogic.GetTarget() is not null && LoadedBullets > 0 &&
            TimeElapsed > 0.5) Shoot();
        else if (Input.GetButtonDown("Fire2")) Reload(); // TODO: PRZEŁADOWANIE NA RAZIE NA PRAWYM MYSZY
    }

    private void Shoot()
    {
        LoadedBullets -= 1;
        TimeElapsed = 0;
        Instantiate(bullet, transform.position, transform.rotation);
    }

    private void Reload()
    {
        TimeElapsed = reloadTime;
        LoadedBullets = maxMagazineSize; // TODO: LEPSZE ROZWIĄZANIE CZASU PRZEŁADOWANIA (Coroutine czy coś (wszędzie by się to przydało :P))
    }

    public int GetMagazineSize()
    {
        return maxMagazineSize;
    }

    public int GetLoadedBullets()
    {
        return LoadedBullets;
    }
}
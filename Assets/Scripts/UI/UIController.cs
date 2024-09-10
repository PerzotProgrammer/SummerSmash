using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    private TextMeshProUGUI HpCounter;
    private TextMeshProUGUI BulletCounter;
    private TextMeshProUGUI KillCounter;
    private TextMeshProUGUI ReloadIndicator;
    private PlayerLogic PlayerLogic;
    private ShootingLogic ShootingLogic;

    private void Start()
    {
        HpCounter = GameObject.Find("HpCounter").GetComponent<TextMeshProUGUI>();
        BulletCounter = GameObject.Find("BulletCounter").GetComponent<TextMeshProUGUI>();
        KillCounter = GameObject.Find("KillCounter").GetComponent<TextMeshProUGUI>();
        ReloadIndicator = GameObject.Find("ReloadIndicator").GetComponent<TextMeshProUGUI>();
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
    }

    private void Update()
    {
        if (!ShootingLogic) UpdateShootingLogic();

        HpCounter.text = $"HP: {PlayerLogic.GetHp()}";
        BulletCounter.text = $"Bullets: {ShootingLogic.GetLoadedBullets()}/{ShootingLogic.GetMagazineSize()}";
        KillCounter.text = $"Total Kills: {EntityBase.KillCounter}";
        ReloadIndicator.text = ShootingLogic.IsOnReload() ? "Reloading..." : "READY!";
    }

    public void UpdateShootingLogic()
    {
        ShootingLogic = GameObject.Find("BulletSpawner").GetComponent<ShootingLogic>();
    }
}
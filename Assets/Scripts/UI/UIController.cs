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
    private TextMeshProUGUI GameOverHeading;
    private PlayerLogic PlayerLogic;
    private ShootingLogic ShootingLogic;

    private void Start()
    {
        HpCounter = GameObject.Find("HpCounter").GetComponent<TextMeshProUGUI>();
        BulletCounter = GameObject.Find("BulletCounter").GetComponent<TextMeshProUGUI>();
        KillCounter = GameObject.Find("KillCounter").GetComponent<TextMeshProUGUI>();
        ReloadIndicator = GameObject.Find("ReloadIndicator").GetComponent<TextMeshProUGUI>();
        GameOverHeading = GameObject.Find("GameOverHeading").GetComponent<TextMeshProUGUI>();
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
        ShootingLogic = GameObject.Find("BulletSpawner").GetComponent<ShootingLogic>();
        // GameObject.Find() jest chyba lepszą metodą niż GameObject.FindWithTag() jeżeli jest tylko jeden obiekt danego typu
    }

    private void Update()
    {
        if (PlayerLogic.IsAlive())
        {
            HpCounter.text = $"HP: {PlayerLogic.GetHp()}";
            BulletCounter.text = $"Bullets: {ShootingLogic.GetLoadedBullets()}/{ShootingLogic.GetMagazineSize()}";
            KillCounter.text = $"Total Kills: {EntityBase.KillCounter}";
            ReloadIndicator.text = ShootingLogic.IsOnReload() ? "Reloading..." : "READY!";
            GameOverHeading.text = "";
        }
        else
        {
            HpCounter.text = "";
            BulletCounter.text = "";
            KillCounter.text = "";
            ReloadIndicator.text = "";
            GameOverHeading.text = $"game over christopher Total Kills: {EntityBase.KillCounter}";
        }
    }
}
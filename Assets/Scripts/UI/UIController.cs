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
    private TextMeshProUGUI WaveIndicator;
    private PlayerLogic PlayerLogic;
    private ShootingLogic ShootingLogic;
    private WaveSystem WaveSystem;

    private void Start()
    {
        HpCounter = GameObject.Find("HpCounter").GetComponent<TextMeshProUGUI>();
        BulletCounter = GameObject.Find("BulletCounter").GetComponent<TextMeshProUGUI>();
        KillCounter = GameObject.Find("KillCounter").GetComponent<TextMeshProUGUI>();
        ReloadIndicator = GameObject.Find("ReloadIndicator").GetComponent<TextMeshProUGUI>();
        WaveIndicator = GameObject.Find("WaveIndicator").GetComponent<TextMeshProUGUI>();
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
        WaveSystem = GameObject.Find("Spawner").GetComponent<WaveSystem>();
    }

    private void Update()
    {
        if (!ShootingLogic) UpdateShootingLogic();

        HpCounter.text = $"HP: {PlayerLogic.GetHp()}";
        BulletCounter.text = $"Bullets: {ShootingLogic.GetLoadedBullets()}/{ShootingLogic.GetMagazineSize()}";
        KillCounter.text = $"Total Kills: {EntityBase.KillCounter}";
        if (WaveSystem.IsOnWaveCooldown())
        {
            WaveIndicator.text = $"WAVE CLEARED\nCURRENT GOAL: {WaveSystem.GetEnemiesInWaveCount()} KILLS";
        }
        else WaveIndicator.text = $"WAVE {WaveSystem.WaveNumber}";

        ReloadIndicatorState();
    }

    private void ReloadIndicatorState()
    {
        if (ShootingLogic.IsOnReload()) ReloadIndicator.text = "Reloading...";
        else if (ShootingLogic.GetLoadedBullets() == 0) ReloadIndicator.text = "EMPTY!";
        else ReloadIndicator.text = "READY!";
    }

    public void UpdateShootingLogic()
    {
        ShootingLogic = GameObject.Find("BulletSpawner").GetComponent<ShootingLogic>();
    }
}
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

        HpCounter.text = $"HP: {PlayerLogic.Hp}";
        BulletCounter.text = $"Bullets: {ShootingLogic.LoadedBullets}/{ShootingLogic.MaxMagazineSize}";
        KillCounter.text = $"Total Kills: {EntityBase.KillCounter}";
        if (WaveSystem.WaveCooldown)
        {
            WaveIndicator.text = $"WAVE CLEARED, MAX HP INCREASED!\nCURRENT GOAL: {WaveSystem.GetEnemiesInWaveCount()} KILLS";
        }
        else WaveIndicator.text = $"WAVE {WaveSystem.WaveNumber}";

        ReloadIndicatorState();
    }

    private void ReloadIndicatorState()
    {
        if (ShootingLogic.IsReloading) ReloadIndicator.text = "Reloading...";
        else if (ShootingLogic.LoadedBullets == 0) ReloadIndicator.text = "EMPTY!";
        else ReloadIndicator.text = "READY!";
    }

    public void UpdateShootingLogic()
    {
        ShootingLogic = GameObject.Find("BulletSpawner").GetComponent<ShootingLogic>();
    }
}
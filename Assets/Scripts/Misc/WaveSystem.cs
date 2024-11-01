using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private int enemiesInWaveCount;
    private PlayerLogic PlayerLogic;
    private WeaponLogic WeaponLogic;
    private AudioSource WaveClearedSound;
    public bool WaveCooldown { get; private set; }
    public static int WaveNumber;

    void Start()
    {
        PlayerLogic = GameObject.Find("Player").GetComponent<PlayerLogic>();
        WeaponLogic = GameObject.Find("WeaponParent").GetComponent<WeaponLogic>();
        WaveClearedSound = GetComponent<AudioSource>();
        WaveNumber = 1;
        WaveCooldown = false;
    }

    void Update()
    {
        if (EntityBase.KillCounter >= enemiesInWaveCount && !WaveCooldown)
        {
            StartCoroutine(nameof(WaveClearedCoroutine));
        }
    }

    private IEnumerator WaveClearedCoroutine()
    {
        PlayerLogic.AddMaxHp(20);
        PlayerLogic.HealHp(PlayerLogic.MaxHp / 2);
        WeaponLogic.ReloadAllMagazines();
        WaveCooldown = true;
        enemiesInWaveCount = (enemiesInWaveCount + 10) + EntityBase.KillCounter; // Potem tu się da inny system
        DespawnAllEnemies();
        WaveNumber++;
        WaveClearedSound.Play();
        yield return new WaitForSeconds(10);
        WaveCooldown = false;
    }

    private void DespawnAllEnemies()
    {
        foreach (EnemiesLogic enemy in EntityBase.Enemies) Destroy(enemy.gameObject);
    }


    public int GetEnemiesInWaveCount()
    {
        return enemiesInWaveCount;
    }
}
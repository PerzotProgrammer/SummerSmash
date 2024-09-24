using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField] private int enemiesInWaveCount;
    public static int WaveNumber;
    private bool WaveCooldown;

    void Start()
    {
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
        WaveCooldown = true;
        enemiesInWaveCount = (enemiesInWaveCount + 10) + EntityBase.KillCounter; // Potem tu się da inny system
        DespawnAllEnemies();
        WaveNumber++;
        yield return new WaitForSeconds(10);
        WaveCooldown = false;
    }

    private void DespawnAllEnemies()
    {
        foreach (EnemiesLogic enemy in EntityBase.Enemies) Destroy(enemy.gameObject);
    }

    public bool IsOnWaveCooldown()
    {
        return WaveCooldown;
    }

    public int GetEnemiesInWaveCount()
    {
        return enemiesInWaveCount;
    }
}
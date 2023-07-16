using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemyCount = 4;   // Maksymalna liczba wrogów
    public float spawnInterval = 4f;   // Interwa³ miêdzy tworzeniem wrogów
    public float spawnRange = 5f;   // Zakres losowego po³o¿enia spawnu

    private int currentEnemyCount = 0;   // Aktualna liczba wrogów

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());   // Rozpoczêcie rutyny tworzenia wrogów
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemyCount)   // Sprawdzenie czy liczba wrogów nie przekroczy³a maksimum
            {
                SpawnEnemy();   // Tworzenie nowego wroga
                currentEnemyCount++;   // Zwiêkszenie liczby wrogów
            }

            yield return new WaitForSeconds(spawnInterval);   // Oczekiwanie na kolejny interwa³
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnOffset = Random.insideUnitCircle * spawnRange;   // Losowy offset wzglêdem spawnera
        Vector3 spawnPosition = transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0);   // Obliczanie pozycji spawnu
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);   // Tworzenie wroga

        // Dodanie obs³ugi zdarzenia OnEnemyKilled z wroga typu SmallEnemy
        SmallEnemy smallEnemy = newEnemy.GetComponent<SmallEnemy>();
        if (smallEnemy != null)
        {
            smallEnemy.OnEnemyKilled += HandleEnemyKilled;
        }
        // Dodanie obs³ugi zdarzenia OnEnemyKilled z wroga typu RegularEnemy
        RegularEnemy regularEnemy = newEnemy.GetComponent<RegularEnemy>();
        if (regularEnemy != null)
        {
            regularEnemy.OnEnemyKilled += HandleRegularEnemyKilled;
        }
    }

    private void HandleEnemyKilled(SmallEnemy enemy)
    {
        currentEnemyCount--;
    }
    private void HandleRegularEnemyKilled(RegularEnemy enemy)
    {
        currentEnemyCount--;
    }

}

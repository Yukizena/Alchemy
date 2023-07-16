using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemyCount = 4;   // Maksymalna liczba wrog�w
    public float spawnInterval = 4f;   // Interwa� mi�dzy tworzeniem wrog�w
    public float spawnRange = 5f;   // Zakres losowego po�o�enia spawnu

    private int currentEnemyCount = 0;   // Aktualna liczba wrog�w

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());   // Rozpocz�cie rutyny tworzenia wrog�w
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemyCount)   // Sprawdzenie czy liczba wrog�w nie przekroczy�a maksimum
            {
                SpawnEnemy();   // Tworzenie nowego wroga
                currentEnemyCount++;   // Zwi�kszenie liczby wrog�w
            }

            yield return new WaitForSeconds(spawnInterval);   // Oczekiwanie na kolejny interwa�
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnOffset = Random.insideUnitCircle * spawnRange;   // Losowy offset wzgl�dem spawnera
        Vector3 spawnPosition = transform.position + new Vector3(spawnOffset.x, spawnOffset.y, 0);   // Obliczanie pozycji spawnu
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);   // Tworzenie wroga

        // Dodanie obs�ugi zdarzenia OnEnemyKilled z wroga typu SmallEnemy
        SmallEnemy smallEnemy = newEnemy.GetComponent<SmallEnemy>();
        if (smallEnemy != null)
        {
            smallEnemy.OnEnemyKilled += HandleEnemyKilled;
        }
        // Dodanie obs�ugi zdarzenia OnEnemyKilled z wroga typu RegularEnemy
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

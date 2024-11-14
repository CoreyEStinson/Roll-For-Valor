using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyData[] enemyTypes;
    public Transform[] spawnPoints;

    public void SpawnEnemyAtPoint(int enemyIndex, int spawnPointIndex)
    {
        if (enemyIndex < enemyTypes.Length && spawnPointIndex < spawnPoints.Length)
        {
            EnemyData selectedEnemy = enemyTypes[enemyIndex];
            Instantiate(selectedEnemy.enemyPrefab, spawnPoints[spawnPointIndex].position, Quaternion.identity);
        }
    }

    public void SpawnRandomEnemy()
    {
        int randomEnemy = Random.Range(0, enemyTypes.Length);
        int randomPoint = Random.Range(0, spawnPoints.Length);
        SpawnEnemyAtPoint(randomEnemy, randomPoint);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    private Wave currentWave;
    public List<Wave> waves = new List<Wave>();
    public int currentWaveNumber = 0;
    private HashSet<EnemyController> enemiesLeft;
    // Start is called before the first frame update
    void Start()
    {
        enemiesLeft = new HashSet<EnemyController>();
        StartWave();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartWave()
    {
        if(currentWave != null)
        {
            Debug.Log("Wave in progress?");
            return;
        }

        if(currentWaveNumber >= waves.Count)
        {
            Debug.Log("No waves to start");
            return;
        }
        currentWave = Instantiate(waves[currentWaveNumber]);
        SpawnWave();
    }

    private void SpawnWave()
    {
        currentWave.beforeWave.Invoke();

        foreach (Enemy enemy in currentWave.enemies)
        {
            EnemyController e = Instantiate(enemy.enemyPrefab, enemy.spawnPosition, Quaternion.identity).GetComponent<EnemyController>();
            enemiesLeft.Add(e);
            e.health.OnDeath += ( () => OnEnemyDeath(e));
        }
    }

    private void OnEnemyDeath(EnemyController enemy)
    {
        Debug.Log("Enemy died: rip " + enemy.name);
        Debug.Log("Enemies left: " + enemiesLeft.Count);
        enemiesLeft.Remove(enemy);
        if (enemiesLeft.Count == 0)
        {
            EndWave();
        }
    }

    private void EndWave()
    {
        if(currentWave != null)
        {
            currentWave.onWaveEnd.Invoke();
            currentWaveNumber++;
        }
    }

    public void Log(string message)
    {
        Debug.Log(message);
    }
}
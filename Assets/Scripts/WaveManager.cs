using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    private Wave currentWave;
    public Wave[] waves;
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

        if(currentWaveNumber >= waves.Length)
        {
            Debug.Log("No waves to start");
            Debug.Log(waves.Length);
            return;
        }
        Debug.Log("Starting wave " + currentWaveNumber);
        currentWave = Instantiate(waves[currentWaveNumber]);
        SpawnWave();
    }

    private void SpawnWave()
    {
        Debug.Log("Spawning wave " + currentWaveNumber);
        currentWave.onWaveEnd.AddListener( () => { Statistics.WavesSurvived++; });
        currentWave.beforeWave.Invoke();

        foreach (Enemy enemy in currentWave.enemies)
        {
            Debug.Log("Spawning enemy " + enemy.enemyPrefab.name);
            EnemyController e = Instantiate(enemy.enemyPrefab, enemy.spawnPosition, Quaternion.identity).GetComponent<EnemyController>();
            enemiesLeft.Add(e);
            e.health.OnDeath += ( () => OnEnemyDeath(e));
            e.health.OnDeath += ( () => Log("En3my died: rip " + e.name));
        }
        Debug.Log("Enemies left: " + enemiesLeft.Count);
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
            currentWaveNumber++;
            currentWave.onWaveEnd.Invoke();
            currentWave = null;
            StartCoroutine(PauseBeforeWave());
        }
    }
    IEnumerator PauseBeforeWave()
    {
        yield return new WaitForSeconds(3);
        StartWave();
    }
    public void Log(string message)
    {
        Debug.Log(message);
    }
}

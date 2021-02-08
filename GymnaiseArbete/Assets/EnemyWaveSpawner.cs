using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyWaveSpawner : MonoBehaviour
{
    public List<GameObject> spawnPosistions;
    public List<GameObject> enemyTypes;
    
    public int maxEnemies;
    public int enemiesInScene;
    private int waveCounter;

    public float lowestSpawnValue;
    public float spawnPointPool;
    private float spawnRate;

    private bool spawning;

    private Text waveCounterTxt;
    private GameObject waveCounterObj;
    private Text waveAnnouncerTxt;
    private GameObject waveAnnouncerObj;

    public float timeTillNextWave;
    public bool countingDown = false;
    



    private void Awake()
    {
        waveCounterObj = GameObject.Find("RoundCounterUI");
        waveAnnouncerObj = GameObject.Find("WaveAnnouncerUI");
        waveCounterTxt = GameObject.Find("RoundCounterUI").GetComponent<Text>();
        waveAnnouncerTxt = GameObject.Find("WaveAnnouncerUI").GetComponent<Text>();
        lowestSpawnValue = 100;
    }


    // Start is called before the first frame update
    void Start()
    {

        //Letar igenom alla fiende typer efter den som har det lägsta spawnvärdet
        foreach (GameObject enemyType in enemyTypes)
        {
            if(enemyType.GetComponent<Stats>().SpawnCost < lowestSpawnValue)
            {
                lowestSpawnValue = enemyType.GetComponent<Stats>().SpawnCost;
            }
        }

        //Sätter så att inga fiender spawnar direkt när spelet börjar
        spawning = false;

        //Ställer in textruta till runda 1
        waveCounter = 1;
        waveCounterTxt.text = "Wave: " + waveCounter.ToString();
        
        //Sätter spawnPointPool värdet och startar spelet
        spawnPointPool = 10;
        spawnRate = 0.5f;
        BeginGame();
    }

   
    void Update()
    {
        Clock();
    }


    void NextWave()
    {

    }

    void BeginGame()
    {
        CountDownTimer();
        Invoke("SpawnWave", timeTillNextWave);
    }

    void CountDownTimer()
    {
        if (timeTillNextWave > 0)
        {
            timeTillNextWave -= Time.deltaTime;
            DisplayTime(timeTillNextWave);
        }
        else
        {
            timeTillNextWave = 0;
            countingDown = false;
        }
    }

    void SpawnEnemy()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        int randomEnemyID = Random.Range(1, enemyTypes.Count);
        int randomSpawnPos = (Random.Range(1, spawnPosistions.Count));

        GameObject enemyToSpawn = enemyTypes[randomEnemyID-1];

        if (enemyToSpawn.GetComponent<Stats>().SpawnCost < spawnPointPool)
        {
            Instantiate(enemyToSpawn, spawnPosistions[randomSpawnPos-1].gameObject.transform.position, Quaternion.identity);
            enemiesInScene++;
        }
        else
        {
            SpawnEnemy();
            return;
        }
    }

    void SpawnWave()
    {
        
        if (lowestSpawnValue < spawnPointPool)
        {
            if (enemiesInScene < maxEnemies && spawning == false)
            {
                spawning = true;
                StartCoroutine(SpawnEnemyCooldown());
            }
            if (enemiesInScene == maxEnemies)
            {
                spawning = false;
                StopCoroutine(SpawnEnemyCooldown());
            }
        }
        else
        {
            NextWave();
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        string seconds = ((timeToDisplay%60)).ToString("f2");

        waveAnnouncerTxt.text = "Seconds until next wave: " + seconds;
    }

    void Clock()
    {
        if (countingDown = true && timeTillNextWave > 0)
        {
            if (!waveAnnouncerObj.activeSelf)
            {
                waveAnnouncerObj.SetActive(true);
            }
            CountDownTimer();
        }
        else if (waveAnnouncerObj.activeSelf)
        {
            waveAnnouncerObj.SetActive(false);
            countingDown = false;
            timeTillNextWave = 5;
        }
    }
    IEnumerator SpawnEnemyCooldown()
    {
        yield return new WaitForSeconds(spawnRate);
        SpawnEnemy();
        spawning = false;
        SpawnWave();
        
    }

}

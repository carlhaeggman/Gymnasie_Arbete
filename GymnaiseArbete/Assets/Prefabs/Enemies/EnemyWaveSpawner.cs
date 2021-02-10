using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class EnemyWaveSpawner : MonoBehaviour
{
    private List<GameObject> spawnPosistions = new List<GameObject>();
    public List<GameObject> enemyTypes = new List<GameObject>();
    
    public int maxEnemies;
    public int enemiesInScene;
    private int waveCounter;

    public float lowestSpawnValue;
    public float spawnPointPool;
    private float spawnRate;
    float lastSpawnPointPool;
   
    private TMP_Text waveCounterTxt;
    private GameObject waveCounterObj;
    private TMP_Text waveAnnouncerTxt;
    private GameObject waveAnnouncerObj;
    private TMP_Text enemySpawnPointTxt;
    private TMP_Text enemiesAliveTxt;

    public float timeTillNextWave;

    public bool countingDown = false;
    private bool canSpawn;
    private bool waveActive;

    private void Awake()
    {
        enemySpawnPointTxt = GameObject.Find("EnemyPointUI").GetComponent<TMP_Text>();
        waveCounterObj = GameObject.Find("RoundCounterUI");
        waveAnnouncerObj = GameObject.Find("WaveAnnouncerUI");
        waveCounterTxt = GameObject.Find("RoundCounterUI").GetComponent<TMP_Text>();
        waveAnnouncerTxt = GameObject.Find("WaveAnnouncerUI").GetComponent<TMP_Text>();
        enemiesAliveTxt = GameObject.Find("EnemiesAliveUI").GetComponent<TMP_Text>();

       
        lowestSpawnValue = 100;
    }


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject spawnPointFound in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            spawnPosistions.Add(spawnPointFound);
            Debug.Log(spawnPosistions.Count);
            Debug.Log(spawnPointFound.name);
        }

        
        //Letar igenom alla fiende typer efter den som har det lägsta spawnvärdet
        foreach (GameObject enemyType in enemyTypes)
        {
            if(enemyType.GetComponent<Stats>().SpawnCost < lowestSpawnValue)
            {
                lowestSpawnValue = enemyType.GetComponent<Stats>().SpawnCost;
            }
        }

        //Sätter så att spawning systemet funkar när första rundan börjar
        canSpawn = true;

        //Ställer in textruta till wave 1
        waveCounter = 1;
        //waveCounterTxt.text = "Wave " + waveCounter.ToString();
        waveCounterTxt.SetText("Wave " + waveCounter.ToString());
        
        //Sätter upp startvärden inklusive spawnPointPool värdet och startar spelet
        spawnPointPool = 5;
        spawnRate = 1f;
        waveActive = false;
        BeginGame();
        lastSpawnPointPool = spawnPointPool;
    }

   
    void Update()
    {
        //Sätter slumpmässigt seed till Random funktionen
        Random.InitState(System.DateTime.Now.Millisecond + 1);
        if(timeTillNextWave <= 0)
        {
            waveActive = true;
        }

        //Sätter spawnpointtext komponenten och enemiesalive text komponenten
        enemySpawnPointTxt.SetText("Enemy spawn points remaing: " + spawnPointPool);
        enemiesAliveTxt.SetText("Enemies alive: " + enemiesInScene);
        //Kalkylerar tidför CountDownTimer funktion
        Clock();
        //Kör Spawnwave funktion om waveActive är true
        if(waveActive == true && enemiesInScene < maxEnemies)
        {
            SpawnWave();
        }
    }

    //Funktion som startar spel
    void BeginGame()
    {
        CountDownTimer();
        waveActive = true;
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
       
        int randomEnemyID = Random.Range(1, enemyTypes.Count);
        int randomSpawnPos = (Random.Range(1, spawnPosistions.Count));

        GameObject enemyToSpawn = enemyTypes[randomEnemyID-1];

        if ((spawnPointPool - enemyToSpawn.GetComponent<Stats>().SpawnCost) > -1)
        {
            GameObject lastEnemy = Instantiate(enemyToSpawn, spawnPosistions[randomSpawnPos-1].gameObject.transform.position, Quaternion.identity);
            lastEnemy.transform.parent = GameObject.Find("TYPE: Shooter").transform;

            spawnPointPool -= enemyToSpawn.GetComponent<Stats>().SpawnCost;
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
        if(countingDown == false)
        {
            if ((spawnPointPool - lowestSpawnValue) > -1)
            {
                if (enemiesInScene < maxEnemies && canSpawn == true)
                {
                    canSpawn = false;
                    StartCoroutine(SpawnEnemyCooldown());
                }
                if (enemiesInScene == maxEnemies)
                {
                    waveActive = false;
                    StopCoroutine(SpawnEnemyCooldown());
                }
            }
            else if (enemiesInScene <= 0)
            {
                NextWave();
            }
        }   
       
    }

    IEnumerator SpawnEnemyCooldown()
    {
        SpawnEnemy();
        canSpawn = false;

        yield return new WaitForSeconds(spawnRate);

        StopCoroutine(SpawnEnemyCooldown());
        canSpawn = true;

    }

    void NextWave()
    {
        spawnPointPool =Mathf.Floor(lastSpawnPointPool * 1.5f);
        lastSpawnPointPool = spawnPointPool;
        timeTillNextWave = 10;
        waveCounter++;

        countingDown = true;
        waveCounterTxt.SetText("Wave: " + waveCounter.ToString());
       

    }

    void DisplayTime(float timeToDisplay)
    {
        string seconds = ((timeToDisplay%60)).ToString("f2");

        waveAnnouncerTxt.SetText("Wave " +waveCounter+ " starting in: "  + seconds + " Seconds");
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
            
        }
    }
}

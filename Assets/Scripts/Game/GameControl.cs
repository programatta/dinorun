using System.Collections;
using UnityEngine;

public class GameControl : MonoBehaviour
{
    public static bool gameStopped = false;
    
    public static GameControl Instance = null;

    [SerializeField] GameObject[] obstacles;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float spawnRate = 2f;
    [SerializeField] float timeToBoost = 5f;


    void Awake()
    {
        mAudioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
            Instance = this;
        else if( Instance != this)
            Destroy(gameObject);

        Score = 0 ;
        gameStopped = false;
        IsGameOver = false;
        Time.timeScale = 1f;
        HighScore = PlayerPrefs.GetInt("highScore");
        nextSpawn = Time.time + spawnRate;
        nextBoost = Time.unscaledTime + timeToBoost;
        mAudioSource.mute = SoundControl.Instance.IsMusicMuted;
        mAudioSource.volume = SoundControl.Instance.MusicVolume;
    }

    // Update is called once per frame
    void Update()
    {
        mAudioSource.mute = SoundControl.Instance.IsMusicMuted;
        mAudioSource.volume = SoundControl.Instance.MusicVolume;

        if(!gameStopped)
            IncreaseScore();

        if(Time.time > nextSpawn)
            SpawnObstacle();
        
        if(Time.unscaledTime > nextBoost && !gameStopped)
            BoostTime();
    }

    public bool IsGameOver {get; private set;}
    public int HighScore {get; private set;}
    public int Score {get; private set;}

    public void NofifyWhenDead(IKillable killable)
    {
        mKillable = killable;
    }

    public void DinoHit()
    {
        if(Score > HighScore)
            PlayerPrefs.SetInt("highScore",Score);
        StartCoroutine(KillDino());
    }

    public void RestartGame()
    {
        SceneLoader.Instance.LoadNextLevel("GamePlay");
    }

    public void GoMainMenu()
    {
        SceneLoader.Instance.LoadNextLevel("MainMenu");
    }

    public void PauseResumeGame()
    {
        if(!gameStopped)
        {
            Time.timeScale = 0;
            gameStopped = true;
        }
        else
        {
            Time.timeScale = 1;
            gameStopped = false;
        }
    }


    /*=======================================================================*/
    /*                              Private Section                          */
    /*=======================================================================*/
    void IncreaseScore()
    {
        if( Time.unscaledTime > nextScoreIncrese)
        {
            Score += 1;
            nextScoreIncrese = Time.unscaledTime + 1;
        }
    }

    void SpawnObstacle()
    {
        nextSpawn = Time.time + spawnRate;
        int pos = Random.Range(0, obstacles.Length);
        Instantiate(obstacles[pos], spawnPoint.position, Quaternion.identity);
    }

    void BoostTime()
    {
        nextBoost = Time.unscaledTime+timeToBoost;
        Time.timeScale += 0.25f;
    }

    IEnumerator KillDino()
    {
        gameStopped = true;
        mKillable.Die();
        yield return new WaitForSeconds(0.45f);
        IsGameOver = true;   
        Time.timeScale = 0;
    }

    float nextBoost;
    float nextSpawn;
    float nextScoreIncrese = 0f;
    IKillable mKillable;
    AudioSource mAudioSource;
}

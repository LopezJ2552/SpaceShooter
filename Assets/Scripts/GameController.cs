using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public bool hardMode;
    public AudioClip victoryTune;
    public AudioClip gameOverTune;
    public AudioClip backgroundTune;
    public AudioSource musicSource;
    public AudioSource soundSource;
    public AudioClip sirenSound;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public Text hardModeText;
    public int score;
    public float Goal;
    private bool restart;
    private bool gameOver;
    private bool won;

    void Start ()
    {
        score = 0;
        restart = false;
        gameOver = false;
        hardMode = false;
        won = false;

        restartText.text = "";
        gameOverText.text = "";
        hardModeText.text = "Press H to activate 'Hard Mode'";

        musicSource.clip = backgroundTune;
        musicSource.Play();

        UpdateScore ();
        StartCoroutine (SpawnWaves ());
    }

    void Update ()
    {
        if (restart)
        {
            if (Input.GetKeyDown (KeyCode.F))
            {
                SceneManager.LoadScene("Main");
            }
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if ((hardMode == false) && (Input.GetKey(KeyCode.H)))
        {
            hardMode = true;
            soundSource.PlayOneShot(sirenSound, 1.0f);
            hardModeText.text = "'Hard Mode' engaged!";
            Debug.Log("Hardmode Enabled");
        }
    }
    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            if (hardMode)
            {
            for (int i = 0; i < hazardCount * 2; i++)
            {   
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait); 
            }
            }
            else
            {
            for (int i = 0; i < hazardCount; i++)
            {   
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait); 
            }
            }
        yield return new WaitForSeconds (waveWait);

        if (gameOver)
        {
            restartText.text = "Press 'F' for Restart";
            restart = true;
            break;
        }
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore ();
    }

    void UpdateScore ()
    {
      scoreText.text = "Points: " + score;
      if (score >= Goal)
      {
        gameOverText.text = "You win! Game created by Javier Lopez";
        gameOver = true;
        restart = true;
        won = true;

        if ((musicSource.playOnAwake == true) && (won == true))
        {
            musicSource.clip = victoryTune;
            musicSource.volume = 0.2f;
            musicSource.Play();
            musicSource.playOnAwake = false;
        }
      }
    }

    public void GameOver ()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;

        if (won == false)
        {
        musicSource.clip = gameOverTune;
        musicSource.loop = false;
        musicSource.Play();
        }
    }
}

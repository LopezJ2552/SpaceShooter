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
    public AudioClip victoryTune;
    public AudioClip gameOverTune;
    public AudioSource musicSource;

    public Text scoreText;
    public Text restartText;
    public Text gameOverText;
    public int score;
    public float Goal;
    private bool restart;
    private bool gameOver;
    void Start ()
    {
        score = 0;
        restart = false;
        gameOver = false;
        restartText.text = "";
        gameOverText.text = "";
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
    }
    IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds (startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range (0,hazards.Length)];
                Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate (hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds (spawnWait);
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

        if (musicSource.playOnAwake == true)
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
        musicSource.clip = gameOverTune;
        musicSource.loop = false;
        musicSource.Play();
    }
}

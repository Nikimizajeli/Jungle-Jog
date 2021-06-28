using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] Text livesText;
    [SerializeField] Text coinsText;

    int coinsCollected = 0;

    private void Awake()
    {
        int numberOfGameSessions = FindObjectsOfType<GameSession>().Length;
        if(numberOfGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        coinsText.text = coinsCollected.ToString();
    }

    public void AddCoin(int pointsToAdd)
    {
        coinsCollected += pointsToAdd;
        coinsText.text = coinsCollected.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if(playerLives > 1)
        {
            StartCoroutine(TakeLife());
        }
        else
        {
            StartCoroutine(RestartToMenu());
        }

    }

    IEnumerator TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator RestartToMenu()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
        SceneManager.LoadScene(0);
        
    }

    public void ResetGameSession()
    {
        Destroy(gameObject);
    }
}

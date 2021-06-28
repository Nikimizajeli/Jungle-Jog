using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] GameObject portalPrefab;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] float levelExitSlowMode = 0.2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(OpenPortalToNextLevel());
    }

    IEnumerator OpenPortalToNextLevel()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        Instantiate(portalPrefab, transform.position, Quaternion.identity);
        Time.timeScale = levelExitSlowMode;
        
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        ResetPersistents();
        Time.timeScale = 1f;
       

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

    }

    private void ResetPersistents()
    {
        if (!FindObjectOfType<ScenePersistent>()) { return; }
        FindObjectOfType<ScenePersistent>().ResetScenePersistents();
        
    }
}

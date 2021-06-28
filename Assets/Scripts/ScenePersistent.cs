using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersistent : MonoBehaviour
{
    int startingSceneIndex;
    private void Awake()
    {
        if(FindObjectsOfType<ScenePersistent>().Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(startingSceneIndex != currentSceneIndex)
        {
            Destroy(gameObject);
        }
    }

public void ResetScenePersistents()
    {
        Destroy(gameObject);
    }

}

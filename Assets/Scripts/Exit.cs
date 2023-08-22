using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(1);

        int a_CurrentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int a_NextLevelIndex = a_CurrentLevelIndex + 1;

        if (a_NextLevelIndex == SceneManager.sceneCountInBuildSettings)
        {
            a_NextLevelIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(a_NextLevelIndex);
    }
}

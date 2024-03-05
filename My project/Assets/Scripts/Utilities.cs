using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

using UnityEngine.SceneManagement;

public static class Utilities
{
    public static int playerDeaths = 0;

    public static string UpdateDeathCount(ref int countReference)
    {
        countReference += 1;
        return "Next rime you'll be at number " + countReference;
    }

    public static void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1.0f;

        Debug.Log("Player Deaths: " + playerDeaths);
        string message = UpdateDeathCount(ref playerDeaths);
        Debug.Log("Player Deaths: " + playerDeaths);
    }

    public static bool RestartLevel(int sceneIndex)
    {
        if(sceneIndex < 0)
        {
            throw new System.ArgumentException("Scene index cannot be negative");
        }
        
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1.0f;

        return true;
    }
}


/*public class Utilities : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
*/
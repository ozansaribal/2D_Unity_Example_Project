using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // I've added the SceneManagement feature to the script. Because I had to control the flow between the levels properly.

public class LevelExit : MonoBehaviour {

    // Here, I've created variables that I might wanna change their values according to the different situations within the Unity inspector.

    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] float LevelExitSlowMoFactor = 0.2f;

    // This function provides that when an interaction occurs with the "LevelExit" sprite, then start a coroutine. 

    void OnTriggerEnter2D(Collider2D other)
    {

        // With coroutine, the function can execute all values within a range. 
        // Normally, all frames are executed in a single frame(in a second) with a regular function.
        // With the coroutine mechanism, the intermediate frames or values will be executed by frames one by one

        StartCoroutine(LoadNextLevel());

    }

    // I've created this method with IEnumerator. Because a method that works with a coroutine needs to be created by IEnumerator.

    IEnumerator LoadNextLevel()
    {
        // Here, I've determined a time flow. With this code line, I wanted to determine the time flow while the character makes any movements.

        Time.timeScale = LevelExitSlowMoFactor;

        // Here, "yield" works with "StartCoroutine" mechanism. This code line provides a time delay to load the next level.

        yield return new WaitForSecondsRealtime(LevelLoadDelay);

        // After changing the time flow to load the next level, I had to change the time flow back to the normal setting as 1f

        Time.timeScale = 1f;

        // Here, I created a variable. This variable works with index numbers. 
        // I determined the index numbers of the levels to order them properly. 
        // So, I got the necessary index numbers of the levels and I assigned these numbers into the "CurrentSceneIndex" variable.

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // With the previous code line, I got the necessary information about the levels with index numbers.
        // After that, I've used the "LoadScene" function to call the next level according to the our "currentSceneIndex" variable.
        // In each usage of the "LoadNextLevel" method, this code line provides the next level to play.

        SceneManager.LoadScene(currentSceneIndex + 1);

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour {

    // Here, I've created a variable to define the index value of the starting scene

    int startingSceneIndex;

    // I had to use the Awake function here. Because the process within this method should be called immediately.

    private void Awake()
    {
        // Here, I got the length of a particular object type which is used for the number of the possible scenes.

        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;

        // If the scenes are higher than 1, then I'll not need them. That's why, I've destroyed them.

        if (numScenePersist > 1)
        {

            Destroy(gameObject);

        }

        // Otherwise, I mustn't use the "destroy" process while the scene is being loaded.

        else
        {

            DontDestroyOnLoad(gameObject);

        }

    }

    // Use this for initialization
    void Start () {

        //  Here, I had to get the information that gives us the index number of the starting scene.

        startingSceneIndex = SceneManager.GetActiveScene().buildIndex;

	}
	
	// Update is called once per frame
	void Update () {

        //  Here, I had to get the information that gives us the index number of the current scene just like I did for the starting scene.

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //  Here, I've checked whether the index values of the current scene and the starting scene are matched or not.
        //  If they are not matched, then destroy the game object that is defined within the "ScenePersist" game object like "Coin".

        if (currentSceneIndex != startingSceneIndex)
        {

            Destroy(gameObject);

        }

	}
}

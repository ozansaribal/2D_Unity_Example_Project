using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

    // Here, I determined how many lives the main character contain at the beginning of the game

    [SerializeField] int playerLives = 3;

    // Here, I determined the score that the main character gets from the coins.

    [SerializeField] int score = 0;

    // Here, I've created a variable for the text that is used for the number of lives.

    [SerializeField] Text livesText;

    // Here, I've created a variable for the text that is used for the number of the score.

    [SerializeField] Text scoreText;
    
    // This method begins to work whenever this script is executed or is called by the people who play the game

    private void Awake()
    {
        //  With this coe line, I've wanted to know how many "GameSession" object exists.

        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        //  Here, I've said that if the number is already more than one, then destroy the new one that tries to create itself.

        if (numGameSessions > 1)
        {

            Destroy(gameObject);

        }
        //  Otherwise, do not destroy this particular one which is the necessary gameObject each time that I load.
        //  Even if the new gameObjects try to appear, this else statement's method continues to work.

        else
        {

            DontDestroyOnLoad(gameObject);

        }
    }


    // Use this for initialization
    void Start () {

        // Here, I had to assign the current number of lives into the text variable. Because I wanted people to see how many lives exist in the game.

        livesText.text = playerLives.ToString();

        // Here, I had to assign the current score into the text variable. Because I wanted people to see how many scores that they've taken in the game.

        scoreText.text = score.ToString();

	}

    // Here, I've created a method that handles with the calculation of the score.

    public void AddToScore(int pointsToAdd)
    {

        // Firstly, I've added the value of the parameter into the current score. Also, I made the score keep up-to-date.

        score += pointsToAdd;

        // Then, I've kept up-to-date the text value of the current score. 
        // Without this code line, the text value of the score would show the value within the Unity inspector

        scoreText.text = score.ToString();

    }

    //  This method is created for the process that tells us what happens if the player dies.
    //  If the main character interacts with the hazards or the enemy character, this method is executed.

    public void ProcessPlayerDeath()
    {
        //  With this method, I had to use if-else statements. 
        //  So, if the main character interacts with the enemy or a random hazard, then the "playerLives" amount is reduced by one.

        if (playerLives > 1)
        {

            TakeLife();

        }

        //  Otherwise, the whole GameSession is reset.

        else
        {

            ResetGameSession();

        }
    }

    //  This is the method that reduces the number of lives of the main character and reloads the current level.

    private void TakeLife()
    {
        // Here, I've reduced the number of lives of the main character.

        playerLives--;

        // Then, I had to reload the current level. To do this, I got the current level's index number. 
        // After that, I've assigned the particular index number into a variable to use the index number for reloading process.

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Finally, I've used the current level's index number within the "LoadScene" method to reload the current level.

        SceneManager.LoadScene(currentSceneIndex);

        // Here, I had to fix a bug. After the main character dies, both numbers are renewed as the old numbers.
        // So, I've updated the number of lives for both the text and the script.

        livesText.text = playerLives.ToString();

    }

    // Here, I wrote the method that resets the gameSession.

    private void ResetGameSession()
    {
        //  This method firstly loads the main menu screen to reset the gameSession.

        SceneManager.LoadScene(0);

        //  After changing the scene, the current gameObject is destroyed to reset the whole details of the gameSession.

        Destroy(gameObject);

    }

}

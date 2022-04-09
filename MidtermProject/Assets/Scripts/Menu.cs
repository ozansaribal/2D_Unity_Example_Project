using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Here, I've added the SceneManagement library again. Because I've worked the flow between the menu and first level.

public class Menu : MonoBehaviour {

    // Here, I've created a method which makes people switch to the next level.

    public void StartFirstLevel()
    {
        // Here, I've loaded the next level which its index number is 1. 
        // This code line seems so simple. But the start button only provides the flow from main menu to the first level.

        SceneManager.LoadScene(1);

    }

    // Here, I wanted to turn back into the Main Menu screen from the success screen. 

    public void LoadMainMenu()
    {
        // This method makes people turn back into the main menu screen.

        SceneManager.LoadScene(0);

    }

}

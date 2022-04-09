using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

    // I've added an AudioClip variable with the SerializeField feature. 
    // The reason to use the SerializeField is that I had to connect the audio file to the game object which is called "coin" within the Unity Inspector.
    // So, this variable enables a connection between the audio file and the coins.

    [SerializeField] AudioClip coinPickUpSFX;

    // Here, I wanted to determine a variable that control the value which adds to the score when the main character interact with the coins.

    [SerializeField] int pointsForCoinPickup = 100;

    //  I've created this method. Because in a game, whenever the main character interacts with a coin, the coin must be destroyed from the game itself.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // After creating the "pointsForCoinPickup", I've called the "AddToScore" method. 
        // Because I had to implement the connection between the coins and the main character.
        // With this code line, whenever the main character interacts with a coin, the score is increased as whatever the value of the "pointsForCoinPickup" equals.

        FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);

        // I've used PlayClipAtPoint method. This method works with a 3D vector. But the game is 2D.
        // The reason to use this method is that if I use a simple method for 2D mechanism, then the audio will continue to play despite the coin will disappear
        // So, I had to play the audio according to the camera that works with the main character. That's why, I've used a method that works with a 3D vector.
        // So, "Destroy(gameObject)" line will still make the coins disappear. But the audio will not be played forever. 

        AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);

        //  That's why, I wanted to destroy the coin that is interacted with the main character. To destroy it, I've used this code line.

        Destroy(gameObject);
    }

}

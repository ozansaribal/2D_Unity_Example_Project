using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput; // I defined the crossplatforminput here. Because I imported this within unity. So, I had to do this definition.

public class Player : MonoBehaviour {

    // [SerializeField] is used for showing a private variable's value on Inspector. Normally, I can't change the value of a private variable. Still, this can't be accessed by another script.

    // config
    [SerializeField] float runSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(25f, 25f); // this code line makes the player move according to the values of the vector components when the player's dead.

    // state
    bool isAlive = true;

    // Cached component references. I've set up the references for rigid body and animator with these statements

    Rigidbody2D myRigidBody; // I cached the current rigidbody values.

    Animator myAnimator; // I cached the animator that I use for animations like idling, running....

    CapsuleCollider2D myBodyCollider; // I cached the collider2D to use colliders more effective in the project.

    BoxCollider2D myFeet; // I've used this collider variable for preventing the wall jump.

    float gravityScaleAtStart; // I need this variable. Because we use this for managing the gravity at ladder.

    // I put here messages then methods.

	// Use this for initialization
	void Start () {

        myRigidBody = GetComponent<Rigidbody2D>(); // After caching the rigidbody values, I defined these values into a variable.

        myAnimator = GetComponent<Animator>(); // After caching, I used the same variable to get necessary components just like I did the same with rigidbody2D.

        myBodyCollider = GetComponent<CapsuleCollider2D>(); // After caching, I used the collider variable to get the proper component values of the necessary colliders.

        myFeet = GetComponent<BoxCollider2D>(); // After I've cached the box collider variable, I got the necessary components from the box collider 2D itself.

        gravityScaleAtStart = myRigidBody.gravityScale; // After the definition of gravity variable, we set current gravity scale as our gravity scale float variable. 
	}
	
	// Update is called once per frame
	void Update ()
    {
        // Here, I said that if the isAlive variable is not true, then do not proceed within the Update function after this if statement.

        if (!isAlive) { return; }

        // Every actions on game are checked by Update function. So, I need to call every method that I've created within Update function to see the current results for each frame.

        Run();

        ClimbLadder();

        Jump();

        FlipSprite();

        Die();

	}

    private void Run()  // If I type "void Run", then I click Tab button, this method will appear. Because in Unity tools, I see that scopes are defined as private.
    {
        // Here, I called horizontal axis within CrossPlatformInputManager using a string value. Then, I stored axis values within a float variable. Axis values are between -1 and 1.

        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");

        // Here, I created a 2D vector. The vector needs to hold two variables for the velocity calculation. These are identified by float. These are x and y components respectively.
        // controlThrow defines x vector component using "Horizontal" axis values. 
        // Actually, I do not need the y vector component for "Run" method. Still, I defined y component using rigidbody's current velocity value.

        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y);

        // The variable on the left that is written as "myRigidBody.velocity" defines the overall velocity of our character.
        // Then, I set the overall velocity as our player's velocity which is identified above using a new Vector.

        myRigidBody.velocity = playerVelocity;

        // Here, I wanted to define the running animation situation according to the movement of the character. 
        // I said that if the character has speed due to the x component, then the character performs the running animation.

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        // According to this SetBool method, if the playerHasHorizontalSpeed is true, then perform the "Running" animation.

        myAnimator.SetBool("Running", playerHasHorizontalSpeed); 

    }

    private void ClimbLadder()
    {
        // Here, I said that if the character is not touching the "Climbing" layer, then do not execute anything. return means that do not consider this statement, move into the next statement.

        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            // I added this line. Because after the character leaves the ladder, it still executes the climbing animation. I fixed the bug with this line.

            myAnimator.SetBool("Climbing", false);

            // Here, I said that if the character is not climbing, then gravity scale value is the same as the beginning gravity scale value. 

            myRigidBody.gravityScale = gravityScaleAtStart;

            return;
        }

        // Here, I had to provide the necessary axis values for the climbing mechanics. So, I got the "Vertical" axis using the input settings. Because character'll move up and down in the Ladder.

        float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");

        // Here, I created a 2D vector that has 2 parameters to use for the mechanics. 
        // First parameter is the x component of the velocity. I used the current velocity values for the x component. Because I won't use the velocity's x component values for the climbing.

        Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, controlThrow * climbSpeed);

        // Here, I set the new vector value which is called "climbVelocity" as the character's current velocity value for this method.

        myRigidBody.velocity = climbVelocity;

        // Here, I said that if the character is climbing, then the gravity scale of the character will equal to zero. 
        // With this line, I prevent the character from sliding down from the ladder.

        myRigidBody.gravityScale = 0f;

        // Here, I wanted to control the movement. I checked whether the character has speed on vertical axis or not.

        bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;

        // Then, I arranged the bool value according to the animator. Here, if the "playerHasVerticalSpeed" value is true, then animation will be executed. Else, no animation will be executed.

        myAnimator.SetBool("Climbing", playerHasVerticalSpeed);

    }

    private void Jump()
    {
        // This statement says that if our collider is not touching the layers which is called "Ground", then execute the process which is defined within the if statement's scope.

        if (!myFeet.IsTouchingLayers(LayerMask.GetMask("Ground"))) 
        {
            return; // with this line, we say that the condition in the if statement is not the expected result, then move on to the next statement which is the another if statement.
        }

        // Here, I detected whether the person who plays the game pressed the necessary button or not. 
        // The necessary button is defined by the string variable which is called "Jump" on Project Settings-Input.

        if (CrossPlatformInputManager.GetButtonDown("Jump"))
        {
            // Here, I created a new 2D Vector which defines the features of the velocity of Jumping.

            Vector2 jumpVelocityToAdd = new Vector2(0f, jumpSpeed);

            myRigidBody.velocity += jumpVelocityToAdd;
        }
    }

    private void Die()
    {
        // This if statement tells us that If the player's touching the mask that is known as "Enemy", then do something

        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            // Here, I said that if the player is dead, then the player's isAlive statement should not be true anymore.

            isAlive = false;

            // Here, I triggered the Dying animation. After the triggering, I throw off the player according to the deathKick vector components.

            myAnimator.SetTrigger("Dying");

            // Here, I get the components according to the necessary rigidbody 2D. Then, I throw off the player according to the deathKick vector components.

            GetComponent<Rigidbody2D>().velocity = deathKick;

            // I had to use the "ProcessPlayerDeath" somehow. So, I called the method using "FindObjectOfType<>".
            // With the "FindObjectOfType<>", I've called the "GameSession". Then, I've gained an access to the necessary method with this game object.

            FindObjectOfType<GameSession>().ProcessPlayerDeath();

        }


    }

    private void FlipSprite()
    {
        // Here, I define a bool variable. Then, I say that if player's horizontal speed is greater than zero, our variable is true. 

        // Mathf.Abs returns the absolute value of the variable. Math.Epsilon defines the zero value for the bool variable.

        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        // Here, I check whether the player moves horizontally or not. In this part, my aim is to reverse the current scaling of x axis.

        if (playerHasHorizontalSpeed) 
        {
            // I used "transform.localScale", that's how I can access our scale on our sprite. Then, I identified a new Vector for this scaling variable.

            // This vector returns us -1 or 1 due to Mathf.Sign pattern. This pattern works according to the value of x component of velocity. 

            // If the value is less than zero, Mathf.Sign returns -1. If the value is equal or greater than zero, Mathf.Sign returns us 1.

            // Here, I do not change the scale of y axis. I keep the value as 1. Because I do not want the character to flip up and down.

            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), 1f);
        }

    }


}

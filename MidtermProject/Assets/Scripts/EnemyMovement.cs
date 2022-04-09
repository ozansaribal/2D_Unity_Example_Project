using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    // I had to control the speed of the movement. So, I created this variable with SerializeField which provides us the controlling within the inspector.

    [SerializeField] float moveSpeed = 1f;

    // I had to control the movement of the character. So, I created a rigidbody variable for the character.

    Rigidbody2D myRigidBody;


	// Use this for initialization
	void Start () {

        // After the creating of the rigidbody variable, I had to get the components of the necessary variable.

        myRigidBody = GetComponent<Rigidbody2D>();  

	}
	
	// Update is called once per frame
	void Update () {

        if (isFacingRight())
        {

            // I created a vector class member for the rigidbody's velocity components. I only determined the x component with moveSpeed. Because I wanted the enemy character not to move vertically.

            myRigidBody.velocity = new Vector2(moveSpeed, 0f);

        }

        else
        {
            // If the character does not facing right, then the character needs to go to the opposite direction.

            myRigidBody.velocity = new Vector2(-moveSpeed, 0f);
        }
	}

    bool isFacingRight()
    {
        // This method is used for the enemy movement. 
        // Because if the x component is positive, then it means that the enemy moves to the right. It means that if statement in the Update function is true. 
        // On the other hand, if the scale is negative, then the enemy should move to the left. So, else statement make the enemy move left.

        return transform.localScale.x > 0;

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Here, I wanted to say that if the enemy character moves to the right or left, then this code line provides that the enemy character moves to the opposite direction.

        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);

    }

}

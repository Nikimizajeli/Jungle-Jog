using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // config
    [SerializeField] float movementSpeed = 20f;
    [SerializeField] float jumpHeight = 20f;
    [SerializeField] float climbingSpeed = 20f;
    [SerializeField] GameObject feet;

    //state
    bool isAlive = true;
    

    //cached component references
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    Collider2D myCollider;
    float startingGravityScale;


    private void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();

        startingGravityScale = myRigidBody.gravityScale;
    }

    private void Update()
    {
        if (!isAlive) { return; }

        MovePlayer();
        FlipSprite();
        Jump();
        ClimbLadder();
        Die();
    }
    private void MovePlayer()
    {
        var deltaX = Input.GetAxis("Horizontal") * movementSpeed;
        myRigidBody.velocity = new Vector2(deltaX, myRigidBody.velocity.y);

        bool playerHasHorizontalVelocity = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
       
        myAnimator.SetBool("isWalking", playerHasHorizontalVelocity);
                  
    }

    private void Jump()
    {
        if (!feet.GetComponent<Collider2D>().IsTouchingLayers(LayerMask.GetMask("Ground"))){ return; }
        else
        {

            if (Input.GetButtonDown("Jump"))
            {
                var velocityToAdd = new Vector2(0f, jumpHeight);
                myRigidBody.velocity += velocityToAdd;
            }

        }
    }

    private void ClimbLadder()
    {
        if (!myCollider.IsTouchingLayers(LayerMask.GetMask("Ladders"))) 
        {
            myRigidBody.gravityScale = startingGravityScale;
            myAnimator.SetBool("isClimbing", false);
            
            return;
        }
        myRigidBody.gravityScale = 0;
        
        var deltaY = Input.GetAxis("Vertical") * climbingSpeed;
        myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, deltaY);
                        
        bool playerHasVerticalVelocity = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalVelocity);
        
    }

    private void FlipSprite()
    {
        bool playerHasHorizontalVelocity = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x), transform.localScale.y);
        }
    }

    private void Die()
    {
        if (myCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            
            isAlive = false;
            myAnimator.SetTrigger("deathTrigger");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }
}

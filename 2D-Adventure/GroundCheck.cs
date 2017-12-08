using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    //This is is made in order to check if player is on the ground. This is crusial for triggering jump/ground/idle animations.
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public Hero Player;

	void FixedUpdate ()
    {
        bool isGround = Physics2D.OverlapCircle(this.groundCheck.position, this.groundCheckRadius, this.whatIsGround);
        Player.isGround = isGround;
        //Debug.LogFormat("{0}, {1}, {2}, {3}", isGround, this.groundCheck.position, this.groundCheckRadius, this.whatIsGround);

        Player.animator.SetBool("isGround", Player.isGround);
        Player.animator.SetFloat("vSpeed", Player.rb.velocity.y);
    }
}

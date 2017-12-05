using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
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

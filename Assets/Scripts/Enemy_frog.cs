using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_frog : Enemy
{
    private Rigidbody2D rb;
    // private Animator anim;
    private Collider2D coll;
    public LayerMask ground;
    public Transform leftpoint,rightpoint;
    public float speed, jumpforce;
    private float leftx, rightx;

    private bool Faceleft = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        // transform.DetachChildren();
        // anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }

    void Movement() {
        if (Faceleft) {
            if (coll.IsTouchingLayers(ground)) {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(-speed, jumpforce);
            }
            
            if (transform.position.x < leftx) {
                transform.localScale = new Vector3(-1,1,1);
                Faceleft = false;
            }
        } else {
            if (coll.IsTouchingLayers(ground)) {
                anim.SetBool("jumping", true);
                rb.velocity = new Vector2(speed, jumpforce);
            }

            if (transform.position.x > rightx) {
                transform.localScale = new Vector3(1,1,1);
                Faceleft = true;
            }
        }
    }

    void SwitchAnim () {
        if (anim.GetBool("jumping")) {
            if (rb.velocity.y < 0) {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("falling")) {
            anim.SetBool("falling", false);
        }
    }
}

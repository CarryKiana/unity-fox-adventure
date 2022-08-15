using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_eagle : Enemy
{
    private Rigidbody2D rb;
    // private Collider2D coll;
    public Transform toppoint, bottompoint;
    public float speed;
    private float topy, bottomy;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        // coll = GetComponent<Collider2D>();
        topy = toppoint.position.y;
        bottomy = bottompoint.position.y;
        Destroy(toppoint.gameObject);
        Destroy(bottompoint.gameObject);
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement() {
        if (transform.position.y < bottomy) {
            rb.velocity = new Vector2(rb.velocity.x, speed);
        } else if (transform.position.y >= topy) {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
        }
    }
}

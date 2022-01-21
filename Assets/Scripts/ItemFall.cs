using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFall : MonoBehaviour
{

    private bool onGround = false;
    private bool scratched = false;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    public Rigidbody2D rb;
    public Collider2D coll;

    private Vector3 vec;

    public int damage = 5;

    private bool launched = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    void FixedUpdate()
    {
        if (FallPlenty.instance.getStatut() && !launched)
            launch();
        if (!scratched)
        {
            if (launched)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.position = new Vector3(rb.position.x, rb.position.y - 0.2f, Time.time);
                onGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            }
            if (onGround)
            {
                scratched = true;
                rb.position = new Vector3(rb.position.x, rb.position.y - 0.2f, Time.time);
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                coll.enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            if (launched)
            {
                // dégat
                PlayerHealth.instance.TakeDamage(damage);
            }
        }
    }

    public void launch()
    {
        launched = true;
    }
}

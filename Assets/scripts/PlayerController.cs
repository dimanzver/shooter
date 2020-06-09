using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    [SerializeField] protected LayerMask groundLayermask;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        if(moveX >= 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
            transform.position = new Vector3(transform.position.x + 2.84f, transform.position.y, transform.position.z);
        }else if(moveX < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            transform.position = new Vector3(transform.position.x - 2.84f, transform.position.y, transform.position.z);
        }

        //transform.localRotation = Quaternion.Euler(0, moveX >= 0 ? 0 : 180, 0);
        //GetComponentInChildren<SpriteRenderer>().flipX = moveX < 0;
        rb.MovePosition(rb.position + Vector2.right * moveX * speed * Time.deltaTime);


        //jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnSurface())
        {
            rb.AddForce(Vector2.up * 7000);
        }
    }

    bool isOnSurface()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + .1f, groundLayermask);
        return hit.collider != null;
    }
}

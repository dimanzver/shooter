using UnityEngine;

public class PlayerController : Human
{
    public GameObject fireAim;

    protected Vector3 aimPosCached;
    protected Rigidbody2D rb;

    // Start is called before the first frame update
    protected override void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (Game.paused)
            return;
        base.Update();
        aimPosCached = fireAim.transform.position;
        move();
        //jump
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnSurface())
        {
            rb.AddForce(Vector2.up * 35000);
        }
        bindClick();
        rotateToCursor();
    }

    void rotateToCursor()
    {
        float angle = getFireAngle();
        rotateTo(angle);
    }

    Vector3 getMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    float getFireAngle()
    {
        Vector3 pos = shootPointCenter.transform.position;
        Vector3 diff = aimPosCached - pos;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return angle;
    }

    void bindClick()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        float angle = getFireAngle();
        shoot(angle, "player");
    }

    void move()
    {
        float moveX = Input.GetAxis("Horizontal");
        rb.MovePosition(rb.position + Vector2.right * moveX * speed * Time.deltaTime);
    }

    bool isOnSurface()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + .1f, groundLayermask);
        return hit.collider != null;
    }
}

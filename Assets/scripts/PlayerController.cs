using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    protected Rigidbody2D rb;
    protected BoxCollider2D boxCollider;
    [SerializeField] protected LayerMask groundLayermask;
    protected GameObject shotPointCenter;
    public GameObject controls;
    public GameObject fireAim;
    public bool flipped = false;

    protected Vector3 aimPosCached;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        shotPointCenter = GameObject.FindGameObjectWithTag("ShootPointCenter");
    }

    // Update is called once per frame
    void Update()
    {
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
        /*Vector3 mousePos = getMousePosition();
        Vector3 diff = mousePos - shotPointCenter.transform.position;
        float z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        float s = Mathf.Sqrt(diff.y * diff.y + diff.x * diff.x);
        if (s < 1)
            return;*/
        //Debug.Log(z);
        //controls.transform.rotation = Quaternion.Euler(0f, 0f, z);
        float angle = getFireAngle();
        if (angle > 90 || angle < -90)
        {
            flip(true);
            angle = angle > 90 ?
                     angle - 180 :
                     180 + angle;
        }
        else
        {
            flip(false);
        }

        controls.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    Vector3 getMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
    }

    float getFireAngle()
    {
        Vector3 pos = shotPointCenter.transform.position;
        Vector3 diff = aimPosCached - pos;
        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        return angle;
    }

    void bindClick()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        float angle = getFireAngle();
        Debug.Log(angle);
        /*
        Vector2 pos = getMousePosition();
        Vector3 startPos = shotPointCenter.transform.position;
        float deltaX = pos.x - startPos.x;
        float deltaY = pos.y - startPos.y;*/
        //Debug.Log(Math.Atan(deltaY / deltaX) * 180.0 / Math.PI);
    }

    void move()
    {
        float moveX = Input.GetAxis("Horizontal");
        if(moveX < 0)
        {
            flip(true);
        }
        else if(moveX > 0)
        {
            flip(false);
        }
        rb.MovePosition(rb.position + Vector2.right * moveX * speed * Time.deltaTime);
    }

    bool isOnSurface()
    {
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, boxCollider.bounds.extents.y + .1f, groundLayermask);
        return hit.collider != null;
    }

    protected void flip(bool isFlip)
    {
        transform.localScale = new Vector3(isFlip ? -1 : 1, 1, 1);
        flipped = isFlip;
    }
}

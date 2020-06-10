using UnityEngine;

public class Enemy : Human
{
    public float rechargeTime = 1f;

    protected bool moving = true;
    protected float fireTimeout = 0f;
    protected bool enabledObj = false;
    protected GameObject player;

    // Start is called before the first frame update
    protected override void Start()
    {
        player = GameObject.FindWithTag("player");
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (health <= 0f)
        {
            Destroy(gameObject);
            return;
        }

        move();
        tryFindPlayer();
    }

    void move()
    {
        if (!moving)
            return;
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, new Vector2(direction, 0), boxCollider.bounds.extents.x * 2, groundLayermask);
        if(hit.collider != null)
        {
            flip(-direction);
        }

        transform.Translate(Vector3.right * direction * speed);
    }

    void OnBecameVisible()
    {
        enabledObj = true;
    }

    void OnBecameInvisible()
    {
        enabledObj = false;
    }

    void flip(int directionX)
    {
        direction = directionX;
    }

    void tryFindPlayer()
    {
        if (!enabledObj)
        {
            moving = true;
            return;
        }

        Vector3 playerPos = player.transform.position;
        Vector3 pos = transform.position;
        Vector3 diff = playerPos - pos;
        float s = Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y);

        //проверяем, есть ли препятствия на пути к игроку
        //если есть, значит, игрок не виден и стрелять не в кого
        RaycastHit2D hit = Physics2D.Raycast(boxCollider.bounds.center, diff, s, groundLayermask);
        if (hit.collider != null)
        {
            moving = true;
            return;
        }

        float angle = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        moving = false;
        rotateTo(angle);
        tryShoot(angle);
    }

    void tryShoot(float angle)
    {
        if(fireTimeout > 0)
        {
            fireTimeout -= Time.deltaTime;
            return;
        }

        fireTimeout = rechargeTime;
        shoot(angle, "enemy");
    }
}

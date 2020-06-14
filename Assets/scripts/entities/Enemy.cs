using UnityEngine;

public class Enemy : Human
{
    public float rechargeTime = 1f;
    [SerializeField] protected LayerMask mineLayermask;

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
        if (Game.paused)
            return;
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

        if (checkNeedFlip())
        {
            flip(-direction);
        }

        if(checkMayMoveInDirection(direction))
            transform.Translate(Vector3.right * direction * speed);
    }

    bool checkNeedFlip()
    {
        //проверяем невозможность двигаться дальше и возможность двигаться в обратную сторону
        return !checkMayMoveInDirection(direction) && checkMayMoveInDirection(-direction, 2f);
    }

    bool checkMayMoveInDirection(int moveDirection, float multiplier = 1f)
    {
        //если при движении по заданному направлению будут препятствия, то двигаться нельзя
        RaycastHit2D hit = Physics2D.Raycast(
            boxCollider.bounds.center, 
            new Vector2(moveDirection, 0),
            boxCollider.bounds.extents.x * multiplier * 2, 
            groundLayermask
        );
        if (hit.collider != null)
            return false;

        //если при движении по заданному направлению будут мины, то двигаться нельзя
        RaycastHit2D minesHit = Physics2D.Raycast(
                        boxCollider.bounds.center + Vector3.down * boxCollider.bounds.extents.y / 2,
                        new Vector2(moveDirection, 0),
                        boxCollider.bounds.extents.x * multiplier * 2,
                        mineLayermask
                    );
        if (minesHit.collider != null)
            return false;

        //проверяем наличие дальше пустоты
        Vector3 startEmptyHit = boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x * moveDirection, 0, 0);
        RaycastHit2D notEmptyHit = Physics2D.Raycast(startEmptyHit, Vector2.down, boxCollider.bounds.extents.y * 2, groundLayermask);

        //двигаться можно, если дальше есть земля
        if (notEmptyHit.collider == null)
            return false;

        return true;
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
        /*if (!enabledObj)
        {
            moving = true;
            return;
        }*/

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

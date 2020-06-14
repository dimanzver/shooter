using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 direction;
    public float speed = 4f;
    public float damage = 1f;
    public string fromTag = "player";
    public float lifeTime = 10f;

    protected float time = 0;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    void FixedUpdate()
    {
        checkCollisions();
    }

    // Update is called once per frame
    void Update()
    {
        if(time > lifeTime)
        {
            Destroy(gameObject);
            return;
        }
        time += Time.deltaTime;
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void checkCollisions()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, transform.position, 0.1f);
        foreach(RaycastHit2D hit in hits)
        {
            //пуля сталкивается только с поверхностью, игроками, минами
            int hitLayer = hit.collider.Layer();
            if (
                hitLayer != LayerMask.NameToLayer("Surface") &&
                hitLayer != LayerMask.NameToLayer("Human") &&
                hitLayer != LayerMask.NameToLayer("mine")
            )
                continue;


            if (hit.collider.CompareTag(fromTag))
                continue;

            //если это мина, то взрываем её
            if(hitLayer == LayerMask.NameToLayer("mine"))
            {
                Mine mine = hit.collider.GetComponent<Mine>();
                mine.blow();
            }
            else
            {
                //иначе наносим урон
                if (fromTag == "player")
                {
                    Enemy enemy = hit.collider.GetComponent<Enemy>();
                    if (enemy)
                        enemy.health -= damage;
                }
                else if (fromTag == "enemy")
                {
                    PlayerController player = hit.collider.GetComponent<PlayerController>();
                    if (player)
                        player.health -= damage;
                }
            }

            Destroy(gameObject);
            break;
        }
    }
}
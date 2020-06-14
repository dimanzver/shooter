using UnityEngine;
using System.Collections;

public class Mine : MonoBehaviour
{
    public float time = 60f;
    public float damage = 5f;
    public float radius = 2f;
    public float force = 4000f;
    [SerializeField] protected Transform explosionPrefab;

    protected TextMesh text;

    void Start()
    {
        GameObject textObject = transform.Find("Text").gameObject;
        text = textObject.GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if(time <= 0f)
        {
            blow();
            return;
        }

        //update time text
        int minutes = ((int)time) / 60;
        int seconds = (int)time - minutes * 60;
        string timeText = (minutes > 9 ? "" : "0") + minutes + ":" +
                        (seconds > 9 ? "" : "0") + seconds;
        text.text = timeText;
    }

    public void blow()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach(Collider2D hit in colliders)
        {
            Rigidbody2D rb = hit.GetComponent<Rigidbody2D>();
            if (rb == null)
                continue;

            Vector3 direction = hit.transform.position - transform.position;
            direction.z = 0;
            float s = Mathf.Sqrt(Mathf.Pow(direction.x, 2) + Mathf.Pow(direction.y, 2));
            float relativeForce = -force * (radius - s) / radius;
            hit.attachedRigidbody.AddForce(direction.normalized * relativeForce);

            if (hit.CompareTag("enemy"))
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if (enemy)
                    enemy.health -= damage;
            }
            else if (hit.CompareTag("player"))
            {
                PlayerController player = hit.GetComponent<PlayerController>();
                if (player)
                    player.health -= damage;
            }
        }

        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.Layer() == LayerMask.NameToLayer("Surface"))
            return;
        blow();
    }
}

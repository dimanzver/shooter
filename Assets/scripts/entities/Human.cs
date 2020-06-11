using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
    public float speed = 0.1f;
    public float health = 3f;
    public float maxHealth = 3f;
    public GameObject controls;
    public Transform bullet;
    public int direction = -1;

    protected GameObject healthScale;
    protected BoxCollider2D boxCollider;
    [SerializeField] protected LayerMask groundLayermask;
    protected GameObject shootPointCenter;
    protected GameObject shootPointStart;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        shootPointCenter = transform.Find("Control/Arm/shootPointCenter").gameObject;
        shootPointStart = transform.Find("Control/Arm/shootPointStart").gameObject;
        healthScale = transform.Find("healthScale").gameObject;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        transform.localScale = new Vector3(direction, 1, 1);
        updateHealthScale();
    }

    protected void updateHealthScale()
    {
        healthScale.transform.localScale = new Vector3(direction, 1, 1);
        HealthScale healthScaleScript = healthScale.GetComponent<HealthScale>();
        healthScaleScript.health = health;
        healthScaleScript.maxHealth = maxHealth;
    }

    protected void rotateTo(float angle)
    {
        if (angle > 90 || angle < -90)
        {
            direction = -1;
            angle = angle > 90 ?
                     angle - 180 :
                     180 + angle;
        }
        else
        {
            direction = 1;
        }

        controls.transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    protected void shoot(float angle, string fromTag)
    {
        Vector3 cameraPos = Camera.main.WorldToScreenPoint(shootPointStart.transform.position);
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(cameraPos.x, cameraPos.y, -Camera.main.transform.position.z - 1));
        GameObject bulletObj = Instantiate(bullet, pos, Quaternion.identity).gameObject;
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        Vector2 shotDrection = new Vector2();
        shotDrection.x = Mathf.Cos(angle * Mathf.Deg2Rad);
        shotDrection.y = Mathf.Sin(angle * Mathf.Deg2Rad);
        bulletScript.direction = shotDrection;
        bulletScript.fromTag = fromTag;
    }
}

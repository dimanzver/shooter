using UnityEngine;

public class Stake : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("player"))
        {
            PlayerController playerScript = collision.collider.GetComponent<PlayerController>();
            playerScript.health = 0;
        }
        else if(collision.collider.CompareTag("enemy"))
        {
            Enemy enemyScript = collision.collider.GetComponent<Enemy>();
            enemyScript.health = 0;
        }
    }
}

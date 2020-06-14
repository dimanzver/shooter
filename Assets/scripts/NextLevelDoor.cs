using UnityEngine;

public class NextLevelDoor : MonoBehaviour
{

    protected GameObject player;
    protected float actionRadius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!Game.paused)
            checkPlayerNearly();
    }

    void checkPlayerNearly()
    {
        Vector2 diff = player.transform.position - transform.position;
        float s = Mathf.Sqrt(Mathf.Pow(diff.x, 2) + Mathf.Pow(diff.y, 2));
        if (s > actionRadius)
            return;

        Game.endLevel();
    }
}

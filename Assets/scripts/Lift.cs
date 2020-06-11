using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour
{
    [SerializeField] protected float startY;
    [SerializeField] protected float endY;
    [SerializeField] protected float speed;
    [SerializeField] protected int nextDirection = 1;

    protected int direction = 0;
    protected int position = 0;
    protected GameObject lift;

    // Start is called before the first frame update
    void Start()
    {
        lift = transform.Find("Lift").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        if (direction == 0)
            return;
        float remain = getMoveRemain();
        float delta = speed * Time.deltaTime;
        if(delta < remain)
        {
            lift.transform.Translate(Vector3.up * direction * delta);
        }
        else
        {
            lift.transform.Translate(Vector3.up * direction * remain);
            direction = 0;
        }
    }

    public void changeDirection()
    {
        direction = nextDirection;
        nextDirection *= -1;
    }

    float getMoveRemain()
    {
        if (direction == 0)
            return 0f;

        if (direction > 0)
            return endY - lift.transform.position.y;
        return lift.transform.position.y - startY;
    }
}

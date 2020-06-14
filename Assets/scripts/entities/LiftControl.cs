using UnityEngine;

public class LiftControl : MonoBehaviour
{
    [SerializeField] protected GameObject lift;

    protected GameObject player;
    protected float usingRadius = 2f;
    protected bool mayUse = false;
    protected SpriteRenderer usingSprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("player");
        usingSprite = transform.Find("Interaction").gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        checkPlayerNearly();
        //если можно использовать лифт, то показываем это
        usingSprite.sortingOrder = mayUse ? 1 : -1;

        //если можно использовать лифт и нажата клавиша E
        if (mayUse)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                lift.GetComponent<Lift>().changeDirection();
            }
        }
    }

    void checkPlayerNearly()
    {
        //проверяем, рядом ли игрок
        Vector3 playerPosDiff = transform.position - player.transform.position;
        float s = Mathf.Sqrt(Mathf.Pow(playerPosDiff.x, 2) + Mathf.Pow(playerPosDiff.y, 2));
        mayUse = s <= usingRadius;
    }
}

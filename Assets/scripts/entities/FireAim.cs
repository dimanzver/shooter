using UnityEngine;

public class FireAim : MonoBehaviour
{
    protected Vector3 mousePos;
    public GameObject shootPointCenter;

    void Start()
    {
        mousePos = Input.mousePosition;
        shootPointCenter = GameObject.FindGameObjectWithTag("ShootPointCenter");
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.paused)
            return;
        if(Input.mousePosition != mousePos)
        {
            mousePos = Input.mousePosition;
            move();
        }
    }

    void move()
    {
        Vector3 mouseWorldPos = getMouseWorldPosition();
        Vector3 diff = mouseWorldPos - shootPointCenter.transform.position;
        if (Mathf.Abs(diff.y) < 1 && Mathf.Abs(diff.x) < 1)
            return;
        mouseWorldPos.z = -1;
        transform.position = mouseWorldPos;
    }

    Vector3 getMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, -Camera.main.transform.position.z + transform.position.z));
    }
}

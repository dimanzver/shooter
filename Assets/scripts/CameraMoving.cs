using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x - 2, transform.position.y, transform.position.z);
    }
}

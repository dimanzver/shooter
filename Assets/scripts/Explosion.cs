using UnityEngine;

public class Explosion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("destroy", 1.0f);
    }

    void destroy()
    {
        Destroy(gameObject);        
    }


}

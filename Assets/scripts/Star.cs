using UnityEngine;
using UnityEngine.UI;

public class Star : MonoBehaviour
{
    public bool active = false;
    [SerializeField] Sprite inactiveSprite;

    // Start is called before the first frame update
    void Start()
    {
        if (!active)
        {
            Image image = GetComponent<Image>();
            image.sprite = inactiveSprite;
        }
    }
}

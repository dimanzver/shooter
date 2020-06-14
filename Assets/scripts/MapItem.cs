using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public int level = 1;
    public int levelPoints = 0;
    public bool available = false;
    [SerializeField] protected Transform starPrefab;
    [SerializeField] protected Texture2D pointerTexture;

    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(delegate() {
            Game.startLevel(level);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        });
    }

    public void init()
    {
        GetComponent<Button>().interactable = available;
        updateStars();   
    }

    void updateStars()
    {
        GameObject starsList = transform.Find("Stars").gameObject;
        for(int i = 1; i <= 3; i++)
        {
            GameObject star = Instantiate(starPrefab).gameObject;
            star.transform.parent = starsList.transform;
            star.GetComponent<Star>().active = i <= levelPoints;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (available)
            Cursor.SetCursor(pointerTexture, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }
}

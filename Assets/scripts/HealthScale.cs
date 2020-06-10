using UnityEngine;

public class HealthScale : MonoBehaviour
{
    protected GameObject healthScaleProgress;
    public float health;
    public float maxHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthScaleProgress = transform.Find("healthScaleProgress").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        float healthRelative = health / maxHealth;
        healthScaleProgress.transform.localScale = new Vector3(healthRelative > 0 ? healthRelative : 0, 1, 1);
    }
}

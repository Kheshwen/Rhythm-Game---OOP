using UnityEngine;

public class Hit : MonoBehaviour
{
    public int hitPoints;
    private SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
        SetColorByHP();
    }

    public void SetColorByHP()
    {
        if (renderer == null) renderer = GetComponent<SpriteRenderer>();

        if (hitPoints >= 3)
            renderer.color = Color.blue;
        else if (hitPoints == 2)
            renderer.color = Color.green;
        else if (hitPoints == 1)
            renderer.color = Color.red;
    }

    private void OnMouseDown()
    {
        hitPoints--;
        SetColorByHP();

        if (hitPoints <= 0)
            Destroy(gameObject);
    }
}

using UnityEngine;

public class Hit : MonoBehaviour
{
<<<<<<< HEAD
    public int hitPoints;
=======
    // This variable will be set by your BeatSpawner
    public int hitPoints;

>>>>>>> 2c187b1a05f503d8586a5f5b2c5be9268f54fe7a
    private SpriteRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
<<<<<<< HEAD
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
=======
    }

    // This runs on every click
    private void OnMouseDown()
    {
        // 1. First, we reduce the HP
        hitPoints--;

        // 2. Now, we check the NEW HP value

        if (hitPoints == 2)
        {
            // It was 3, now it's 2. Change color to Green.
            renderer.color = Color.green;
        }
        else if (hitPoints == 1)
        {
            // It was 2, now it's 1. Change color to Red.
            renderer.color = Color.red;
        }
        else if (hitPoints <= 0)
        {
            // It was 1, now it's 0. Destroy it.
            Destroy(gameObject);
        }
>>>>>>> 2c187b1a05f503d8586a5f5b2c5be9268f54fe7a
    }
}
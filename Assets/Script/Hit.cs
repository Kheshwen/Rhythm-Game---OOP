using UnityEngine;

public enum MovementDirection
{
    Static,
    Right,
    UpRight,
    Up,
    UpLeft,
    Left,
    DownLeft,
    Down,
    DownRight
}

public class Hit : MonoBehaviour
{
    public int hitPoints;
    public float moveSpeed = 3f;

    private SpriteRenderer renderer;
    private MovementDirection moveDirection;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(int hp, MovementDirection dir)
    {
        hitPoints = hp;
        moveDirection = dir;

        // Color by HP
        renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            if (hp == 1)       renderer.color = Color.red;
            else if (hp == 2)  renderer.color = Color.green;
            else               renderer.color = Color.blue;
        }
    }

    void Update()
    {
        if (moveDirection != MovementDirection.Static)
        {
            Vector2 directionVector = Vector2.zero;

            switch (moveDirection)
            {
                case MovementDirection.Right:      directionVector = Vector2.right; break;
                case MovementDirection.UpRight:    directionVector = new Vector2(1, 1).normalized; break;
                case MovementDirection.Up:         directionVector = Vector2.up; break;
                case MovementDirection.UpLeft:     directionVector = new Vector2(-1, 1).normalized; break;
                case MovementDirection.Left:       directionVector = Vector2.left; break;
                case MovementDirection.DownLeft:   directionVector = new Vector2(-1, -1).normalized; break;
                case MovementDirection.Down:       directionVector = Vector2.down; break;
                case MovementDirection.DownRight:  directionVector = new Vector2(1, -1).normalized; break;
            }

            transform.position += (Vector3)directionVector * moveSpeed * Time.deltaTime;
        }

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        pos.x = Mathf.Clamp01(pos.x);
        pos.y = Mathf.Clamp01(pos.y);
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }

    private void OnMouseDown()
    {
        hitPoints--;

        if (hitPoints == 2)       renderer.color = Color.green;
        else if (hitPoints == 1)  renderer.color = Color.red;
        else if (hitPoints <= 0)
        {
            Destroy(gameObject);
            ScoreManager.instance.AddPoint();
        }
    }
}

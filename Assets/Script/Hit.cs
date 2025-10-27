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
    public float moveSpeed = 5f;

    private SpriteRenderer renderer;
    private MovementDirection moveDirection;
    private Vector2 directionVector;

    // Bounding Box (Set these in Inspector or calculate dynamically)
    private float screenLeft;
    private float screenRight;
    private float screenBottom;
    private float screenTop;

    void Awake() // Changed from Start to Awake for earlier initialization
    {
        renderer = GetComponent<SpriteRenderer>();
        CalculateScreenBounds(); // Calculate screen boundaries once
    }

    void Start()
    {
        // If directionVector is zero and not static, recalc (safety)
        if (moveDirection != MovementDirection.Static && directionVector == Vector2.zero)
        {
            SetMoveDirection(moveDirection);
        }
    }

    // Calculates the screen boundaries in world units
    private void CalculateScreenBounds()
    {
        Camera mainCamera = Camera.main;
        float halfHeight = mainCamera.orthographicSize;
        float halfWidth = mainCamera.aspect * halfHeight;

        // Give a little padding for the sprite itself
        float paddingX = renderer.bounds.extents.x;
        float paddingY = renderer.bounds.extents.y;

        screenLeft = -halfWidth + paddingX;
        screenRight = halfWidth - paddingX;
        screenBottom = -halfHeight + paddingY;
        screenTop = halfHeight - paddingY;
    }

    public void Initialize(int hp, MovementDirection dir)
    {
        hitPoints = hp;
        moveDirection = dir;
        SetMoveDirection(dir);

        // Ensure renderer is always initialized
        if (!renderer) renderer = GetComponent<SpriteRenderer>();

        // Set color based on HP
        if (renderer != null)
        {
            if (hp == 1) renderer.color = Color.red;
            else if (hp == 2) renderer.color = Color.green;
            else renderer.color = Color.blue; // Default for 3+ HP
        }
    }

    private void SetMoveDirection(MovementDirection dir)
    {
        switch (dir)
        {
            case MovementDirection.Right: directionVector = Vector2.right; break;
            case MovementDirection.UpRight: directionVector = new Vector2(1, 1).normalized; break;
            case MovementDirection.Up: directionVector = Vector2.up; break;
            case MovementDirection.UpLeft: directionVector = new Vector2(-1, 1).normalized; break;
            case MovementDirection.Left: directionVector = Vector2.left; break;
            case MovementDirection.DownLeft: directionVector = new Vector2(-1, -1).normalized; break;
            case MovementDirection.Down: directionVector = Vector2.down; break;
            case MovementDirection.DownRight: directionVector = new Vector2(1, -1).normalized; break;
            default: directionVector = Vector2.zero; break; // Static
        }
    }

    void Update()
    {
        // make sure it only moves if direction is valid
        if (moveDirection != MovementDirection.Static)
        {
            transform.Translate(directionVector * moveSpeed * Time.deltaTime);
            CheckForScreenBounce(); // Check for screen edges
        }
    }

    private void CheckForScreenBounce()
    {
        Vector3 currentPos = transform.position;

        // Bounce off left/right edges
        if (currentPos.x < screenLeft || currentPos.x > screenRight)
        {
            directionVector.x *= -1; // Reverse X direction
            // Snap to edge to prevent getting stuck
            transform.position = new Vector3(Mathf.Clamp(currentPos.x, screenLeft, screenRight), currentPos.y, currentPos.z);
        }

        // Bounce off top/bottom edges
        if (currentPos.y < screenBottom || currentPos.y > screenTop)
        {
            directionVector.y *= -1; // Reverse Y direction
            // Snap to edge to prevent getting stuck
            transform.position = new Vector3(currentPos.x, Mathf.Clamp(currentPos.y, screenBottom, screenTop), currentPos.z);
        }
    }

    private void OnMouseDown()
    {
        hitPoints--;

        // Update color based on new HP
        if (hitPoints == 2) renderer.color = Color.green;
        else if (hitPoints == 1) renderer.color = Color.red;
        else if (hitPoints <= 0)
        {
            Destroy(gameObject);
            ScoreManager.instance.AddPoint(); // Add 1 point
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum MovementDirection
{
    Static,
    Right,      // 0 degrees
    UpRight,    // 45 degrees
    Up,         // 90 degrees
    UpLeft,     // 135 degrees
    Left,       // 180 degrees
    DownLeft,   // 225 degrees
    Down,       // 270 degrees
    DownRight   // 315 degrees
}
public class Hit : MonoBehaviour
{
<<<<<<< dvd

    // This variable will be set by your BeatSpawner
    public int hitPoints;
    public float moveSpeed = 3f;

=======
    public int hitPoints;
>>>>>>> main
    private SpriteRenderer renderer;
    private MovementDirection moveDirection;

    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
<<<<<<< dvd
=======
        SetColorByHP();
>>>>>>> main
    }

    // This is our new "mission briefing" function
    public void Initialize(int hp, MovementDirection dir)
    {
        // 1. Receive and store the orders
        this.hitPoints = hp;
        this.moveDirection = dir;

        // 2. Set the initial color
        renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            if (hp == 1)
            {
                renderer.color = Color.red;
            }
            else if (hp == 2)
            {
                renderer.color = Color.green;
            }
            else if (hp >= 3) // For 3 or more HP
            {
                renderer.color = Color.blue;
            }
        }
    }
    // This is the "autopilot" that runs every frame
    void Update()
    {
        // If the direction is Static, do nothing.
        if (moveDirection == MovementDirection.Static)
        {
            return;
        }

<<<<<<< dvd
        // 1. Figure out which way to move
        Vector2 directionVector = Vector2.zero;
        switch (moveDirection)
        {
            case MovementDirection.Right:
                directionVector = Vector2.right;
                break;
            case MovementDirection.UpRight:
                directionVector = new Vector2(1, 1).normalized;
                break;
            case MovementDirection.Up:
                directionVector = Vector2.up;
                break;
            case MovementDirection.UpLeft:
                directionVector = new Vector2(-1, 1).normalized;
                break;
            case MovementDirection.Left:
                directionVector = Vector2.left;
                break;
            case MovementDirection.DownLeft:
                directionVector = new Vector2(-1, -1).normalized;
                break;
            case MovementDirection.Down:
                directionVector = Vector2.down;
                break;
            case MovementDirection.DownRight:
                directionVector = new Vector2(1, -1).normalized;
                break;
        }

        // 2. Apply the movement
        // We multiply by moveSpeed and Time.deltaTime for smooth, consistent speed
        transform.position += (Vector3)directionVector * moveSpeed * Time.deltaTime;
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
            ScoreManager.instance.AddPoint();
        }
=======
        if (hitPoints <= 0)
            Destroy(gameObject);
>>>>>>> main
    }
}

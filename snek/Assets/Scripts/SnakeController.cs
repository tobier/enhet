using System;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float movesPerSecond = 1.0f;
    private float tick = 0.0f;

    public GameObject segmentPrefab;

    private Vector2 direction = Vector2.right;
    private Vector2 queuedDirection = Vector2.zero;
    private Transform head;
    private bool ate = false;

    // Start is called before the first frame update
    void Start()
    {
        tick = 0.0f;
        direction = Vector2.right;
        head = transform.GetChild(0);
        ate = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("up") && direction != Vector2.down)
        {
            queuedDirection = Vector2.up;
        }
        else if (Input.GetKey("down") && direction != Vector2.up)
        {
            queuedDirection = Vector2.down;
        }
        else if (Input.GetKey("left") && direction != Vector2.right)
        {
            queuedDirection = Vector2.left;
        }
        else if (Input.GetKey("right") && direction != Vector2.left)
        {
            queuedDirection = Vector2.right;
        }
        else if (Input.GetKey("space"))
        {
            ate = true;
        }

        // Is it time to move?
        if ((tick += Time.deltaTime) < (1.0f / movesPerSecond))
        {
            return;
        }

        // If input is queued, and it's time to move, then change the direction
        if (queuedDirection != Vector2.zero)
        {
            direction = queuedDirection;
            queuedDirection = Vector2.zero;
        }

        if (ate)
        {
            // Add a new segment
            var segment = Instantiate(segmentPrefab, head.position, Quaternion.identity, transform);
            segment.name = "Segment";
            ate = false;
        }
        // Move the head and segments
        for (int i = transform.childCount - 1; i > 0; i--)
        {
            var segment = transform.GetChild(i);
            var parent = transform.GetChild(i - 1);
            segment.position = parent.position;
        }
        head.Translate(direction);

        // Reset the update timer
        tick = 0.0f;
    }
}

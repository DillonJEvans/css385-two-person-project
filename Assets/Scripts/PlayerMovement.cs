using UnityEngine;

using static InputUtils;


[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [Tooltip("Speed, in units per second.")]
    public float speed = 5f;
    [Tooltip(
        "How long it takes to accelerate, in seconds.\n" +
        "Used as the `smoothTime` parameter for SmoothDamp()."
    )]
    public float accelerationTime = 0.1f;

    [Header("Controls")]
    [Tooltip("The name of the input axis for moving in the X direction.")]
    public string xAxisName = "Horizontal";
    [Tooltip("The name of the input axis for moving in the Y direction.")]
    public string yAxisName = "Vertical";
    [Tooltip("Whether to use GetInput() or GetInputRaw().")]
    public bool useRawInput = true;
    [Tooltip(
        "The deadzone of input.\n" +
        "Only used when useRawInput is true."
    )]
    public float deadzone = 0.19f;


    private Collider2D collider2d = null;
    private Rigidbody2D rb2d = null;

    private Vector2 direction = Vector2.zero;
    private Vector2 acceleration = Vector2.zero;

    private Vector3 colliderOffset = Vector3.zero;


    private void Start()
    {
        collider2d = GetComponent<Collider2D>();
        rb2d = GetComponent<Rigidbody2D>();
        colliderOffset = collider2d.bounds.center - transform.position;
    }

    private void Update()
    {
        UpdateDirection();
        RestrictPosition();
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = speed * direction;
        rb2d.velocity = Vector2.SmoothDamp(
            rb2d.velocity,
            targetVelocity,
            ref acceleration,
            accelerationTime
        );
    }


    private void UpdateDirection()
    {
        Vector2 input = new(
            GetInputFromAxis(xAxisName, useRawInput),
            GetInputFromAxis(yAxisName, useRawInput)
        );
        direction = ApplyDeadzone(input, deadzone);
        direction = NormalizeIfNeeded(input);
    }

    private void RestrictPosition()
    {
        Bounds bounds = collider2d.bounds;
        Vector3 localPosition = transform.localPosition;
        localPosition += colliderOffset;

        Camera camera = Camera.main;
        Vector2 halfScreenSize = new(
            camera.orthographicSize * camera.aspect,
            camera.orthographicSize
        );
        halfScreenSize.x -= bounds.extents.x;
        halfScreenSize.y -= bounds.extents.y;
        Vector2 minimum = -halfScreenSize;
        Vector2 maximum = halfScreenSize;

        localPosition.x = Mathf.Clamp(localPosition.x, minimum.x, maximum.x);
        localPosition.y = Mathf.Clamp(localPosition.y, minimum.y, maximum.y);

        localPosition -= colliderOffset;
        transform.localPosition = localPosition;
    }
}

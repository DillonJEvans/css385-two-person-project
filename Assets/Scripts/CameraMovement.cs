using UnityEngine;


public class CameraMovement : MonoBehaviour
{
    [Tooltip("Velocity, in units per second.")]
    public Vector2 velocity = Vector2.up;


    private void Update()
    {
        Vector3 positionDelta = Time.deltaTime * velocity;
        transform.localPosition += positionDelta;
    }
}

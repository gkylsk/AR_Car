using UnityEngine;

public class CarController : MonoBehaviour
{
    public static CarController Instance { get; private set; }

    [Header("Movement Settings")]
    public float moveSpeed = 2f;
    public float turnSpeed = 120f;

    private bool isMovingForward;
    private bool isMovingBackward;
    private bool isTurningLeft;
    private bool isTurningRight;

    private void Awake()
    {
        // This makes the latest spawned car the one the buttons control.
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Update()
    {
        if (isMovingForward)
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (isMovingBackward)
            transform.Translate(Vector3.back * moveSpeed * Time.deltaTime);

        if (isTurningLeft)
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);

        if (isTurningRight)
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
    }

    // Button press
    public void StartForward() => isMovingForward = true;
    public void StopForward() => isMovingForward = false;

    public void StartBackward() => isMovingBackward = true;
    public void StopBackward() => isMovingBackward = false;

    public void StartTurnLeft() => isTurningLeft = true;
    public void StopTurnLeft() => isTurningLeft = false;

    public void StartTurnRight() => isTurningRight = true;
    public void StopTurnRight() => isTurningRight = false;

    // Optional: tap-to-move one step methods
    public void MoveForwardStep(float step = 0.5f) => transform.Translate(Vector3.forward * step);
    public void MoveBackwardStep(float step = 0.5f) => transform.Translate(Vector3.back * step);
    public void TurnLeftStep(float degrees = 15f) => transform.Rotate(Vector3.up, -degrees);
    public void TurnRightStep(float degrees = 15f) => transform.Rotate(Vector3.up, degrees);
}
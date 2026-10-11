using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CarButton : MonoBehaviour
{
    public Button forwardButton;
    public Button backwardButton;
    public Button leftButton;
    public Button rightButton;

    // Track hold state per action
    private bool holdingForward;
    private bool holdingBackward;
    private bool holdingLeft;
    private bool holdingRight;

    private void OnEnable()
    {
        // Press events
        AddHoldEvents(forwardButton, () => holdingForward = true, () => holdingForward = false);
        AddHoldEvents(backwardButton, () => holdingBackward = true, () => holdingBackward = false);
        AddHoldEvents(leftButton, () => holdingLeft = true, () => holdingLeft = false);
        AddHoldEvents(rightButton, () => holdingRight = true, () => holdingRight = false);
    }

    private void OnDisable()
    {
        // Cleanup to avoid dangling events
        RemoveHoldEvents(forwardButton);
        RemoveHoldEvents(backwardButton);
        RemoveHoldEvents(leftButton);
        RemoveHoldEvents(rightButton);
    }

    private void Update()
    {
        var car = CarController.Instance;
        if (car == null) return;

        // Continuously tell the car what to do based on current hold states
        if (holdingForward) car.StartForward(); else car.StopForward();
        if (holdingBackward) car.StartBackward(); else car.StopBackward();
        if (holdingLeft) car.StartTurnLeft(); else car.StopTurnLeft();
        if (holdingRight) car.StartTurnRight(); else car.StopTurnRight();
    }

    // --- Hold helpers using EventTrigger for down/up ---

    private void AddHoldEvents(Button button, System.Action onDown, System.Action onUp)
    {
        if (button == null) return;

        var trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger == null) trigger = button.gameObject.AddComponent<EventTrigger>();

        var downEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerDown };
        downEntry.callback.AddListener(_ => onDown());
        trigger.triggers.Add(downEntry);

        var upEntry = new EventTrigger.Entry { eventID = EventTriggerType.PointerUp };
        upEntry.callback.AddListener(_ => onUp());
        trigger.triggers.Add(upEntry);
    }

    private void RemoveHoldEvents(Button button)
    {
        if (button == null) return;
        var trigger = button.gameObject.GetComponent<EventTrigger>();
        if (trigger != null) trigger.triggers.Clear();
    }
}
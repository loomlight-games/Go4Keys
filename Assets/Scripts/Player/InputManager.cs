using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)] // Will run before any other script
public class InputManager : MonoBehaviour
{
    [HideInInspector] public bool swipeUp, swipeRight, swipeLeft;
    public static InputManager Instance; // Singleton
    PlayerInput playerInput; // InputActions script

    public delegate void TouchEvent(Vector2 screenPos, float time);
    public event TouchEvent OnStartTouch, OnEndTouch;

    void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        playerInput = new PlayerInput();
    }

    void OnEnable()
    {
        playerInput.Enable();

        // Enable device gyroscope (new input system)
        if (UnityEngine.InputSystem.Gyroscope.current != null)
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);

    }

    void OnDisable()
    {
        playerInput.Disable();

        // Disable device gyroscope (new input system)
        if (UnityEngine.InputSystem.Gyroscope.current != null)
            InputSystem.DisableDevice(UnityEngine.InputSystem.Gyroscope.current);
    }

    void Start()
    {
        playerInput.Touch.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerInput.Touch.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    private async void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        await Task.Delay(50);
        OnStartTouch?.Invoke(PrimaryPosition(), (float)ctx.startTime);
    }

    void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnEndTouch?.Invoke(PrimaryPosition(), (float)ctx.time);
    }

    public Vector2 PrimaryPosition()
    {
        // Return PrimaryPosition action value
        return playerInput.Touch.PrimaryPosition.ReadValue<Vector2>();
    }

    public Vector3 PrimaryWorldPosition()
    {
        Vector2 screenPos = PrimaryPosition();
        return Camera.main.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 3f));
    }

    public Vector3 DeviceRotation()
    {
        // Return Gyroscope action value
        return playerInput.Touch.Gyroscope.ReadValue<Vector3>();
    }
}

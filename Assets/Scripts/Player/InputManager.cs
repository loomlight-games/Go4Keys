using UnityEngine;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-1)] // Will run before any other script
public class InputManager : MonoBehaviour
{
    [HideInInspector] public bool swipeUp, swipeRight, swipeLeft;
    public static InputManager Instance; // Singleton
    PlayerInput playerInput; // InputActions script

    #region Events
    public delegate void StartTouch(Vector2 pos, float time);
    public event StartTouch OnStartTouch;
    public delegate void EndTouch(Vector2 pos, float time);
    public event EndTouch OnEndTouch;
    #endregion

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

        if (UnityEngine.InputSystem.Gyroscope.current != null)
            InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);

        if (AttitudeSensor.current != null)
            InputSystem.EnableDevice(AttitudeSensor.current);

    }

    void OnDisable()
    {
        playerInput.Disable();
    }

    void Start()
    {
        playerInput.Mobile.PrimaryContact.started += ctx => StartTouchPrimary(ctx);
        playerInput.Mobile.PrimaryContact.canceled += ctx => EndTouchPrimary(ctx);
    }

    void StartTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnStartTouch?.Invoke(PrimaryPosition(), (float)ctx.startTime);
    }

    void EndTouchPrimary(InputAction.CallbackContext ctx)
    {
        OnEndTouch?.Invoke(PrimaryPosition(), (float)ctx.time);
    }

    public Vector2 PrimaryPosition()
    {
        return playerInput.Mobile.PrimaryPosition.ReadValue<Vector2>();
    }

    public Vector3 DeviceRotation()
    {
        //return playerInput.Mobile.Gyroscope.ReadValue<Vector3>();
        return UnityEngine.InputSystem.Gyroscope.current.angularVelocity.ReadValue();
    }

    ////////////////////////////////////////////////////////////////////////////////////////////
    static Vector3 ScreenToWorld(Camera camera, Vector3 pos)
    {
        pos.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(pos);
    }
}

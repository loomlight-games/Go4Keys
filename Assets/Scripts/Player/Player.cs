using UnityEngine;

/// <summary>
/// Provides player states, movement and mechanics.
/// </summary>
public class Player : AStateController 
{
    public static Player Instance;
    Rigidbody rigidBody;
    Transform playerParent;

    #region STATES
    // Running
    // Jumping
    // At intersection?
    // Stopped
    #endregion

    #region BEHAVIOURS
    public RailControl railControl;
    public EndlessRunner endlessRunner;
    public TurnControl turnControl;
    public StaminaSystem staminaSystem;
    public Collecter collecter;
    #endregion

    #region CHECKERS
    [Header("Checkers")]
    //Checker of layer in that position
    public Transform groundChecker;
    public Transform obstacleChecker;
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;
    #endregion

    #region MOVEMENT
    [Header("Movement")]
    public float sideSpeed = 6.0f;
    public float forwardSpeed = 7.0f;
    public float jumpForce = 7.0f;
    [Tooltip("Its children are the diferrent rails")]
    public Transform railsParent;
    #endregion

    #region MECHANICS
    [Header("Mechanics")]
    public GameObject collectible;
    public float staminaLossPerSecond = 2f;
    public float staminaLossPerCrash = 5f;
    #endregion

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }


    public override void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerParent = transform.parent;

        //SetState(Running)

        railControl = new(transform, sideSpeed, railsParent);
        endlessRunner = new(rigidBody, playerParent, forwardSpeed, jumpForce, groundChecker, obstacleChecker, groundLayer, obstacleLayer);
        turnControl = new(playerParent);
        staminaSystem = new(obstacleChecker, obstacleLayer, staminaLossPerSecond, staminaLossPerCrash);
        collecter = new(collectible);
    }

    public override void Update()
    {
        railControl.Update();
        endlessRunner.Update();
        turnControl.Update();
        staminaSystem.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        turnControl.OnTriggerEnter(other);
        staminaSystem.OnTriggerEnter(other);
        collecter.OnTriggerEnter(other);
    }
}

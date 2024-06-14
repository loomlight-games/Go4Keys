using UnityEngine;

/// <summary>
/// Provides player states, movement and mechanics.
/// </summary>
public class Player : AStateController
{
    public static Player Instance;
    [HideInInspector] public Rigidbody rigidBody; 
    [HideInInspector] public Transform playerParent;
    //[HideInInspector] public bool victory = false;
    //[HideInInspector] public bool caught = false;
    //[HideInInspector] public bool tired = false;

    #region STATES
    public PlayerRunState runState = new();
    public PlayerJumpState jumpState = new();
    public PlayerAtIntersectionState atIntersection = new();
    public PlayerEndState endState = new();
    #endregion

    #region BEHAVIOURS
    public Railed railed;
    public Jumper jumper;
    public EndlessRunner endlessRunner;
    public Turner turner;
    public Resilient resilient;
    public KeyCollecter keyCollecter;
    public Chased chased;
    #endregion

    #region CHECKERS
    [Header("Layers and its checkers")]
    public LayerMask groundLayer;
    public LayerMask obstacleLayer;
    [SerializeField] Transform groundChecker;
    public Transform obstacleChecker;
    #endregion

    #region MOVEMENT
    [Header("Movement")]
    [SerializeField] float railChangeSpeed;
    [SerializeField] float forwardSpeed;
    [SerializeField] float jumpForce;
    [Tooltip("Its children are the diferrent rails")]
    [SerializeField] Transform rails;
    #endregion

    #region MECHANICS
    [Header("Mechanics")]
    [SerializeField] int keysToCollect;
    [SerializeField] float staminaLossPerStep;
    [SerializeField] float staminaLossPerJump;
    [SerializeField] float chaserResetDistance;
    #endregion

    public override void Awake()
    {
        // Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public override void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        playerParent = transform.parent;

        railed = new(transform, railChangeSpeed, rails);
        jumper = new(rigidBody, groundChecker, groundLayer, jumpForce);
        endlessRunner = new(playerParent, forwardSpeed, obstacleChecker, obstacleLayer);
        turner = new(playerParent);
        resilient = new(staminaLossPerStep, staminaLossPerJump);
        keyCollecter = new(keysToCollect);
        chased = new(chaserResetDistance);

        SetState(runState);
    }
}
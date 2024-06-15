using UnityEngine;

/// <summary>
/// Implements context and transitions for player states.
/// </summary>
public class PlayerManager : AStateManager
{
    public static PlayerManager Instance;

    #region STATES
    public PlayerRunState runState = new();
    public PlayerJumpState jumpState = new();
    public PlayerAtIntersectionState atIntersection = new();
    public PlayerEndState endState = new();
    #endregion

    #region HABILITIES
    public EndlessRunner endlessRunner;
    public Railed railed;
    public Jumper jumper;
    public Turner turner;
    public Resilient resilient;
    public KeyCollecter keyCollecter;
    public Chased chased;
    #endregion

    #region PROPERTIES
    [Header("Movement")]
    [SerializeField] float forwardSpeed;
    [SerializeField] float railChangeSpeed;
    [SerializeField] float jumpForce;
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
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        Transform playerParent = transform.parent;

        endlessRunner = new(playerParent, forwardSpeed);
        railed = new(transform, railChangeSpeed);
        jumper = new(rigidBody, jumpForce);
        turner = new(playerParent);
        resilient = new(staminaLossPerStep, staminaLossPerJump);
        keyCollecter = new(keysToCollect);
        chased = new(chaserResetDistance);

        SetState(runState);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Transform obstacleChecker = GameObject.Find("Player obstacle checker").transform;
        Transform groundChecker = GameObject.Find("Player ground checker").transform;
        Gizmos.DrawSphere(obstacleChecker.position, 0.4f);
        Gizmos.DrawSphere(groundChecker.position, 0.1f);
    }
}
﻿using UnityEngine;

/// <summary>
/// Provides player states, movement and mechanics.
/// </summary>
public class Player : AStateController
{
    public static Player Instance;
    [HideInInspector] public Rigidbody rigidBody; 
    [HideInInspector] public Transform playerParent;

    #region STATES
    public InStreetState inStreetState = new();
    public JumpState jumpState = new();
    public AtIntersectionState atIntersection = new();
    // Stopped
    #endregion

    #region BEHAVIOURS
    public Railed railed;
    public Jumper jumper;
    public EndlessRunner endlessRunner;
    public Turner turner;
    public Resilient resilient;
    public Collecter collecter;
    #endregion

    #region CHECKERS
    [Header("Layers and its checkers")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] Transform groundChecker;
    [SerializeField] Transform obstacleChecker;
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
    [SerializeField] GameObject collectible;
    [SerializeField] float staminaLossPerStep;
    [SerializeField] float staminaLossPerJump;
    #endregion

    public void Awake()
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
        collecter = new(collectible);

        SetState(inStreetState);
    }

    public override void Update()
    {
        currentState.Update();
    }

    public override void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(other);
    }
}
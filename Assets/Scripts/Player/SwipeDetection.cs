using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwipeDetection : MonoBehaviour
{
    public float minDistance = .1f;
    public float maxTime = 1f;
    [Range(0f, 1f)] public float directionThreshold = .9f;
    public GameObject trail;

    InputManager inputManager;

    Vector2 startPos, endPos;
    float startTime, endTime;
    Coroutine trailCoroutine;

    void Awake()
    {
        inputManager = InputManager.Instance;
    }

    void OnEnable()
    {
        inputManager.OnStartTouch += SwipeStart;
        inputManager.OnEndTouch += SwipeEnd;
    }

    void OnDisable()
    {
        inputManager.OnStartTouch -= SwipeStart;
        inputManager.OnEndTouch -= SwipeEnd;
    }

    private void SwipeStart(Vector2 pos, float time)
    {
        startPos = pos;
        startTime = time;

        trail.SetActive(true);
        trailCoroutine = StartCoroutine(Trail());
    }

    private void SwipeEnd(Vector2 pos, float time)
    {
        trail.SetActive(false);
        StopCoroutine(trailCoroutine);

        endPos = pos;
        endTime = time;

        DetectSwipe();
    }

    void DetectSwipe()
    {
        if (Vector3.Distance(startPos, endPos) >= minDistance &&
            (endTime - startTime) <= maxTime)
        {
            Debug.DrawLine(startPos, endPos, Color.red, 3f);
            Vector2 direction = endPos - startPos;
            direction.Normalize();
            SwipeDirection(direction);
        }
    }

    void SwipeDirection(Vector2 direction)
    {
        // Only check right and left swipes at intersections
        if (Player.Instance.currentState == Player.Instance.atIntersection)
        {
            if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
            {
                Debug.Log("Swipe right"); // Take street on the right
                InputManager.Instance.swipeRight = true;
            }
            else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
            {
                Debug.Log("Swipe left"); // Take street on the left
                InputManager.Instance.swipeLeft = true;
            }
        }

        // But check up swipes always
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            Debug.Log("Swipe up"); // Jump
            InputManager.Instance.swipeUp = true;
        }
    }

    /// <summary>
    /// For the trail renderer
    /// </summary>
    IEnumerator Trail()
    {
        while (true)
        {
            trail.transform.position = inputManager.PrimaryWorldPosition();
            yield return null; // Wait for next frame
        }
    }
}
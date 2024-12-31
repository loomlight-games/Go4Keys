using System.Collections;
using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public float minDistance = .2f;
    public float maxTime = 1f;
    [Range(0f, 1f)] public float directionThreshold = .9f;
    public GameObject trail;

    InputManager inputManager;
    Camera mainCamera;

    Vector2 startPos, endPos;
    float startTime, endTime;
    Coroutine trailCoroutine;

    void Awake()
    {
        inputManager = InputManager.Instance;
        mainCamera = Camera.main; // Get the main camera
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
        trail.transform.position = mainCamera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, mainCamera.nearClipPlane));
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
        if (
            Vector3.Distance(startPos, endPos) >= minDistance &&
            (endTime - startTime) <= maxTime
        )
        {
            Debug.DrawLine(startPos, endPos, Color.red, 3f);
            Vector3 direction3D = endPos - startPos;
            Vector2 direction2D = new Vector2(direction3D.x, direction3D.y).normalized;
            SwipeDirection(direction2D);
        }
    }

    void SwipeDirection(Vector2 direction)
    {
        if (Vector2.Dot(Vector2.up, direction) > directionThreshold)
        {
            Debug.Log("Swipe up");
        }
        else if (Vector2.Dot(Vector2.right, direction) > directionThreshold)
        {
            Debug.Log("Swipe right");
        }
        else if (Vector2.Dot(Vector2.down, direction) > directionThreshold)
        {
            Debug.Log("Swipe down");
        }
        else if (Vector2.Dot(Vector2.left, direction) > directionThreshold)
        {
            Debug.Log("Swipe left");
        }
    }

    /// <summary>
    /// For the trail renderer
    /// </summary>
    IEnumerator Trail()
    {
        while (true)
        {
            Vector2 screenPos = inputManager.PrimaryPosition();
            Vector3 worldPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, mainCamera.nearClipPlane));
            trail.transform.position = worldPos;
            yield return null; // wait for next frame
        }
    }
}
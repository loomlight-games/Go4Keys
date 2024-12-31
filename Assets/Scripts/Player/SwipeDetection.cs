using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    public float minDistance = .2f;
    public float maxTime = 1f;

    InputManager inputManager;

    Vector2 startPos, endPos;
    float startTime, endTime;

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
    }

    private void SwipeEnd(Vector2 pos, float time)
    {
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
            Debug.Log("Swipe detected!");
            Debug.DrawLine(startPos, endPos, Color.red, 5f);
        }
    }
}

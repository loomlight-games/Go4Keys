using System;
using UnityEngine;

/// <summary>
/// Handles tutorial pop ups
/// </summary>
[Serializable]
public class TutorialUI
{
    readonly Serializable<bool> tutorialVisibility = new(); // Serialized object to know if tutorial is activated
    bool learnTutorial;

    GameObject popUps;
    GameObject left;
    GameObject right;
    GameObject jump;
    GameObject stamina;
    GameObject keys;
    GameObject telephone;
    GameObject intersection;
    GameObject advice;
    bool atIntersection = false;
    bool hasSurpassedTurnPoint = false;
    bool tutorialFinished = false;
    bool Apressed, Dpressed, SpacePressed, timeEnded, startTimer;
    float time = 0.3f;

    public void Initialize()
    {
        //Reads from memory if tutorial is activated
        learnTutorial = tutorialVisibility.Deserialize("tutorialvisibility.json");

        if (learnTutorial)
        {
            Player.Instance.endlessRunner.AtIntersectionEvent += AtIntersection;
            Player.Instance.turner.TurnedEvent += TurnPointSurpassed;

            popUps = GameObject.Find("Tutorial pop ups");

            left = popUps.transform.Find("Left").gameObject;
            right = popUps.transform.Find("Right").gameObject;
            jump = popUps.transform.Find("Jump").gameObject;
            stamina = popUps.transform.Find("Stamina").gameObject;
            keys = popUps.transform.Find("Keys").gameObject;
            telephone = popUps.transform.Find("Telephone").gameObject;
            intersection = popUps.transform.Find("Intersection").gameObject;
            advice = popUps.transform.Find("Advice").gameObject;

            left.SetActive(true); // Left
            Time.timeScale = 0.2f;
        }
    }

    public void Update()
    {
        if (learnTutorial)
        {
            if (startTimer)
            {
                if (time < 0f)
                {
                    timeEnded = true;
                    startTimer = false;

                    if (stamina.activeSelf)
                        time = 2f;
                    else
                        time = 0.3f;
                }
                else if (time >= 0f)
                {
                    time -= Time.deltaTime;
                }
            }

            if (atIntersection) // Intersection guides
            {
                intersection.SetActive(true); // Intersection
                atIntersection = false;
                timeEnded = false;
            }
            else if (hasSurpassedTurnPoint)
            {
                intersection.SetActive(false);

                startTimer = true;

                advice.SetActive(true);

                if (timeEnded)
                {
                    advice.SetActive(false);
                    timeEnded = false;
                    tutorialFinished = false;
                    hasSurpassedTurnPoint = false;
                }
            }
            else if (!tutorialFinished)
            {
                if (Input.GetKeyDown(KeyCode.A))
                    Apressed = true;
                else if (Input.GetKeyDown(KeyCode.D))
                    Dpressed = true;
                else if (Input.GetKeyDown(KeyCode.Space))
                    SpacePressed = true;

                if (Apressed && left.activeSelf)
                {
                    Time.timeScale = 1f;

                    startTimer = true;

                    if (timeEnded)
                    {
                        left.SetActive(false);
                        right.SetActive(true); // Right
                        Time.timeScale = 0.2f;
                        timeEnded = false;
                    }

                }
                else if (Dpressed && right.activeSelf)
                {
                    Time.timeScale = 1f;

                    startTimer = true;

                    if (timeEnded)
                    {
                        right.SetActive(false); 
                        jump.SetActive(true); // Jump
                        Time.timeScale = 0.2f;
                        timeEnded = false;
                    }
                }
                else if (SpacePressed && jump.activeSelf)
                {
                    Time.timeScale = 1f;

                    startTimer = true;

                    if (timeEnded)
                    {
                        jump.SetActive(false); 
                        stamina.SetActive(true); // Stamina
                        Time.timeScale = 0.3f;
                        timeEnded = false;
                    }
                }
                else if (Input.anyKeyDown)
                {
                    if (stamina.activeSelf)
                    {
                        stamina.SetActive(false);
                        keys.SetActive(true); // Keys
                    }
                    else if (keys.activeSelf)
                    {
                        keys.SetActive(false);
                        telephone.SetActive(true); // Telephone
                    }
                    else if (telephone.activeSelf)
                    {
                        telephone.SetActive(false);
                        Time.timeScale = 1f;
                    }
                }
            }
        }
    }

    void AtIntersection(object sender, EventArgs any)
    {
        atIntersection = true;
    }

    void TurnPointSurpassed(object sender, bool turned)
    {
        hasSurpassedTurnPoint = true;
    }
}

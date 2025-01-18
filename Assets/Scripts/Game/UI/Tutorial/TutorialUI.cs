using System;
using UnityEngine;

/// <summary>
/// Handles tutorial pop ups
/// </summary>
[Serializable]
public class TutorialUI
{
    //readonly Serializable<bool> tutorialVisibility = new(); // Serialized object to know if tutorial is activated
    public readonly string TUTORIAL_FILE = "TutorialVisibility";
    bool learnTutorial;

    GameObject popUps, gyroscope, jump, stamina, keys, telephone, intersection;
    bool atIntersection = false, hasSurpassedTurnPoint = false, tutorialFinished = false;
    bool timeEnded, startTimer;
    float time = 0.3f;

    public void Initialize()
    {
        // Retrieve the integer value from PlayerPrefs and convert it to a boolean
        int tutorialActivatedInt = PlayerPrefs.GetInt(TUTORIAL_FILE, 1);
        learnTutorial = tutorialActivatedInt == 1;
        Debug.Log("Learn tutorial: " + learnTutorial);

        if (!learnTutorial) return;

        Player.Instance.endlessRunner.AtIntersectionEvent += AtIntersection;
        Player.Instance.turner.TurnedEvent += TurnPointSurpassed;

        popUps = GameObject.Find("Tutorial pop ups");

        gyroscope = popUps.transform.Find("Gyroscope").gameObject;
        jump = popUps.transform.Find("Jump").gameObject;
        stamina = popUps.transform.Find("Stamina").gameObject;
        keys = popUps.transform.Find("Keys").gameObject;
        telephone = popUps.transform.Find("Telephone").gameObject;
        intersection = popUps.transform.Find("Intersection").gameObject;

        //gyroscope.SetActive(true); // Left
        //Time.timeScale = 0.2f;
    }

    public void Update()
    {
        if (!learnTutorial) return;

        if (startTimer)
        {
            if (time < 0f)
            {
                timeEnded = true;
                startTimer = false;

                if (stamina.activeSelf)
                    time = 1f;
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
            hasSurpassedTurnPoint = false;

            intersection.SetActive(false);
            Time.timeScale = 1f; // Normal simulation
        }
        else if (!tutorialFinished)
        {
            // if (Apressed && left.activeSelf)
            // {
            //     Time.timeScale = 1f;

            //     startTimer = true;

            //     if (timeEnded)
            //     {
            //         left.SetActive(false);
            //         right.SetActive(true); // Right
            //         Time.timeScale = 0.2f;
            //         timeEnded = false;
            //     }

            // }
            // else if (Dpressed && right.activeSelf)
            // {
            //     Time.timeScale = 1f;

            //     startTimer = true;

            //     if (timeEnded)
            //     {
            //         right.SetActive(false);
            //         jump.SetActive(true); // Jump
            //         Time.timeScale = 0.2f;
            //         timeEnded = false;
            //     }
            // }
            // else if (SpacePressed && jump.activeSelf)
            // {
            //     Time.timeScale = 1f;

            //     startTimer = true;

            //     if (timeEnded)
            //     {
            //         jump.SetActive(false);
            //         stamina.SetActive(true); // Stamina
            //         Time.timeScale = 0.2f;
            //         timeEnded = false;
            //     }
            // }
            // else if (Input.anyKeyDown)
            // {
            //     if (stamina.activeSelf)
            //     {
            //         stamina.SetActive(false);
            //         keys.SetActive(true); // Keys
            //     }
            //     else if (keys.activeSelf)
            //     {
            //         keys.SetActive(false);
            //         telephone.SetActive(true); // Telephone
            //     }
            //     else if (telephone.activeSelf)
            //     {
            //         telephone.SetActive(false);
            //         Time.timeScale = 1f;
            //     }
            // }
        }
    }


    void AtIntersection(object sender, EventArgs any)
    {
        atIntersection = true;
        Time.timeScale = 0.5f; // Slows down simulation
    }

    void TurnPointSurpassed(object sender, bool turned)
    {
        hasSurpassedTurnPoint = true;
    }


}

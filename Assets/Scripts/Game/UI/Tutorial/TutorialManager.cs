using System;
using UnityEngine;

/// <summary>
/// Handles tutorial pop ups
/// </summary>
[Serializable]
public class TutorialManager
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
    float popUpShowTime = 2f;

    public void Initialize()
    {
        //Reads from memory if tutorial is activated
        learnTutorial = tutorialVisibility.Deserialize("tutorialvisibility.json");

        if (learnTutorial)
        {
            PlayerManager.Instance.endlessRunner.AtIntersectionEvent += AtIntersection;
            PlayerManager.Instance.turner.TurnEvent += TurnPointSurpassed;

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
        }
    }

    public void Update()
    {
        if (learnTutorial)
        {
            if (atIntersection) // Intersection guides
            {
                intersection.SetActive(true);
                atIntersection = false;
            }
            else if (hasSurpassedTurnPoint)
            {
                intersection.SetActive(false);

                if (popUpShowTime < 0f)
                {
                    advice.SetActive(false);
                    tutorialFinished = false;
                    hasSurpassedTurnPoint = false;
                }
                else if (popUpShowTime >= 0f)
                {
                    advice.SetActive(true); // Advice
                    popUpShowTime -= Time.deltaTime;
                }
            }
            else if (!tutorialFinished)
            {
                if (Input.GetKeyDown(KeyCode.A) && left.activeSelf)
                {
                    left.SetActive(false);
                    right.SetActive(true); // Right
                }
                else if (Input.GetKeyDown(KeyCode.D) && right.activeSelf)
                {
                    right.SetActive(false);
                    jump.SetActive(true); // Jump
                }
                else if (jump.activeSelf)
                {
                    if (popUpShowTime < 0f)
                    {
                        jump.SetActive(false);
                        stamina.SetActive(true); // Stamina
                        popUpShowTime = 2f;
                        Time.timeScale = 0f;
                    }
                    else if (popUpShowTime >= 0f)
                    {
                        popUpShowTime -= Time.deltaTime;
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

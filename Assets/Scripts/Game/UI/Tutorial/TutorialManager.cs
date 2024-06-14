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
    float showTime = 2.5f;


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

            left.SetActive(true);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && left.activeSelf)
        {
            left.SetActive(false);
            right.SetActive(true);
        }  
        else if (Input.GetKeyDown(KeyCode.D) && right.activeSelf)
        {
            right.SetActive(false);
            jump.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && jump.activeSelf)
        {
            jump.SetActive(false);
            stamina.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (Input.anyKeyDown)
        {
            if (stamina.activeSelf)
            {
                stamina.SetActive(false);
                keys.SetActive(true);
            }
            else if (keys.activeSelf)
            {
                keys.SetActive(false);
                telephone.SetActive(true);
            }
            else if (telephone.activeSelf)
            {
                telephone.SetActive(false);
                Time.timeScale = 1f;
            }
        }
        else if (atIntersection)
        {
            intersection.SetActive(true);
            atIntersection = false;
        }
        else if (hasSurpassedTurnPoint)
        {
            intersection.SetActive(false);

            if (showTime < 0f)
            {
                advice.SetActive(false);
                hasSurpassedTurnPoint = false;
            }
            else if (showTime >= 0f)
            {
                advice.SetActive(true);
                showTime -= Time.deltaTime;
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

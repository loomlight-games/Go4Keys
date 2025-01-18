using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles gameplay with the simulation going.
/// </summary>
public class GamePlayState : AState
{
    const string TUTORIAL_FILE = "TutorialVisibility";
    List<GameObject> popUpsList = new();
    GameObject pauseButton, intersection, advice;
    bool learnTutorial = true, tutorialStarted = false, tutorialFinished = false;

    public override void Enter()
    {
        Time.timeScale = 1f; // Resumes simulation

        GameObject UI = GameObject.Find("UI");
        pauseButton = UI.transform.Find("Buttons").transform.Find("Pause").gameObject;
        pauseButton.SetActive(true);

        // Retrieve the integer value from PlayerPrefs and convert it to a boolean
        int tutorialActivatedInt = PlayerPrefs.GetInt(TUTORIAL_FILE, 1);
        learnTutorial = tutorialActivatedInt == 1;
        Debug.Log("Learn tutorial: " + learnTutorial);

        if (!learnTutorial) return;

        Player.Instance.endlessRunner.AtIntersectionEvent += AtIntersection;
        Player.Instance.turner.TurnedEvent += TurnPointSurpassed;

        intersection = UI.transform.Find("Intersection").gameObject;
        advice = UI.transform.Find("Advice").gameObject;

        Transform popUps = UI.transform.Find("Tutorial pop ups");

        // Add all children of popUps to popUpsList
        foreach (Transform child in popUps)
            popUpsList.Add(child.gameObject);
    }

    public override void Update()
    {
        if (!learnTutorial || tutorialStarted || tutorialFinished) return;

        tutorialStarted = true;
        GameManager.Instance.TutorialSequence(popUpsList);
    }

    public override void Exit()
    {
        pauseButton.SetActive(false);
    }

    void AtIntersection(object sender, EventArgs any)
    {
        intersection.SetActive(true);
        Time.timeScale = GameManager.SLOWED_SPEED; // Slow down simulation
    }

    void TurnPointSurpassed(object sender, bool turned)
    {
        intersection.SetActive(false);
        Time.timeScale = 1f; // Normal simulation

        if (!tutorialFinished)
        {
            GameManager.Instance.ActivateMomentarily(advice);
            tutorialFinished = true;
        }
    }
}
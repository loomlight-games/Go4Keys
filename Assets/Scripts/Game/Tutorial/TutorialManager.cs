using System;
using UnityEngine;

//HANDLES TUTORIAL POP-UPS

[Serializable]
public class TutorialManager : MonoBehaviour
{
    //DIRTYFLAG
    TutorialVisibility tutorialVisibility;//Serialized object to know if tutorial is active
    bool tutorialIsShown;//Learn tutorial

    //Sprites group
    [SerializeField] GameObject popUpsGroup;//Needs to be ordered
    GameObject[] popUpsArray;//Array of popUps
    int current = 0;//Current popUp (starts from the first)

    //Trigger intersection popUp
    [SerializeField] Transform intersectionChecker;//Detects intersection
    [SerializeField] float checkerRadious = 1f;
    [SerializeField] LayerMask triggerMask;//Intersection
    bool atIntersection = false;
    
    //Message show time
    [SerializeField] float showTime = 4f;//Time certain popUps will be shown

    // Start is called before the first frame update
    void Start()
    {
        //Instantantiate serialized object
        tutorialVisibility = new TutorialVisibility();

        //Reads from memory if tutorial is activated
        tutorialIsShown = tutorialVisibility.Deserialize();

        //Want to learn the tutorial
        if (tutorialIsShown)
        {
            popUpsGroup.SetActive(true);

            //Creates array the size of number of children of popUps
            popUpsArray = new GameObject[popUpsGroup.transform.childCount];

            //Fills the array with each gameObject
            for (int i = 0; i < popUpsArray.Length; i++)
            {
                popUpsArray[i] = popUpsGroup.transform.GetChild(i).gameObject;

                //Deactivates all popUps
                popUpsArray[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Want to learn the tutorial
        if (tutorialIsShown)
        {
            CheckIntersection();

            switch (current)
            {
                //Move left 'A'
                case 0:
                    popUpsArray[current].SetActive(true);

                    //When 'A' pressed
                    if (Input.GetKeyDown(KeyCode.A))
                    {
                        popUpsArray[current].SetActive(false);

                        //Next popUp
                        current = 1;
                    }
                    break;

                //Move right 'D'
                case 1:
                    popUpsArray[current].SetActive(true);

                    //When 'D' pressed
                    if (Input.GetKeyDown(KeyCode.D))
                    {
                        popUpsArray[current].SetActive(false);

                        //Next popUp
                        current = 2;
                    }
                    break;

                //Jump 'Space'
                case 2:
                    popUpsArray[current].SetActive(true);

                    //When 'Space' pressed
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        popUpsArray[current].SetActive(false);

                        //Next popUp
                        current = 3;
                    }
                    break;

                //Stamina
                case 3:
                    popUpsArray[current].SetActive(true);

                    //Pauses simulation so that player can read the popUp
                    PauseGame();

                    //When any key down
                    if (Input.anyKeyDown)
                    {
                        popUpsArray[current].SetActive(false);

                        //Next popUp
                        current = 4;
                    }
                    break;

                //Keys
                case 4:
                    popUpsArray[current].SetActive(true);

                    //Simulation is still paused

                    //When any key down
                    if (Input.anyKeyDown)
                    {
                        popUpsArray[current].SetActive(false);

                        //Next popUp
                        current = 5;
                    }
                    break;

                //Telephone
                case 5:
                    popUpsArray[current].SetActive(true);

                    //Simulation is still paused

                    //When any key down
                    if (Input.anyKeyDown)
                    {
                        popUpsArray[current].SetActive(false);

                        //Next popUp
                        current = 6;

                        //Resumes simulation
                        ResumeGame();
                    }
                    break;

                //Intersection
                case 6:
                    //In intersection
                    if (atIntersection)
                    {
                        popUpsArray[current].SetActive(true);

                        //When 'A' or 'D' pressed
                        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                        {
                            popUpsArray[current].SetActive(false);

                            //Next popUp
                            current++;
                        }
                    }
                    break;

                //Don't get caught
                default:
                    popUpsArray[current].SetActive(true);

                    //Timer ends
                    if (showTime <= 0)
                    {
                        popUpsArray[current].SetActive(false);
                    }
                    else
                    {
                        //Reduces timer
                        showTime -= Time.deltaTime;
                    }

                    break;
            }
        }
    }

    private void CheckIntersection()
    {
        //Creates a sphere with certain radious at checker position that will get triggered by certain mask
        if (Physics.CheckSphere(intersectionChecker.position,checkerRadious, triggerMask))
        {
            atIntersection= true;
        }
    }

    void PauseGame()
    {
        // Set the time scale to 0 to pause the game
        Time.timeScale = 0f;
    }

    void ResumeGame()
    {
        // Set the time scale back to 1 to resume the game
        Time.timeScale = 1f;
    }
}

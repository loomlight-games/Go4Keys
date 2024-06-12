using System;
using UnityEngine;

//KEY PROPERTIES AND BEHAVIOUR

public class Key : MonoBehaviour
{
    public RotatorySO rotationType;
    public float bounceSpeed = 3f;
    private bool goUp = true;
    [SerializeField] Transform upMark;
    [SerializeField] Transform downMark;

    // Update is called once per frame
    void Update()
    {
        //ROTATION
        //360 degrees * speed * time independent from framerate
        //speed = 1 is a whole rotation
        transform.Rotate(360 * rotationType.Xspeed * Time.deltaTime, 
            360 * rotationType.Yspeed * Time.deltaTime, 
            360 * rotationType.Zspeed * Time.deltaTime);

        //Bounces automatically
        if(goUp)
        {
            //Goes up
            transform.position = Vector3.MoveTowards(transform.position, upMark.position, bounceSpeed * Time.deltaTime);
        }
        else
        {
            //Goes down
            transform.position = Vector3.MoveTowards(transform.position, downMark.position, bounceSpeed * Time.deltaTime);
        }

        //Reaches an extreme
        if(Math.Abs(transform.position.y - downMark.position.y) <= 0.01f  || Math.Abs(transform.position.y - upMark.position.y) <= 0.01f)
        {
            //Goes to the other
            goUp = !goUp;
        }
    }
}

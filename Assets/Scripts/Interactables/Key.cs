using System;
using UnityEngine;

/// <summary>
/// Key properties and behaviour
/// </summary>
public class Key : MonoBehaviour
{
    [SerializeField] RotatorySO rotationType;
    [SerializeField] float bounceSpeed = 3f;
    [SerializeField] Transform upMark;
    [SerializeField] Transform downMark;
    bool goUp = true;

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
            transform.position = Vector3.MoveTowards(transform.position, upMark.position, bounceSpeed * Time.deltaTime);
        else
            transform.position = Vector3.MoveTowards(transform.position, downMark.position, bounceSpeed * Time.deltaTime);

        //Reaches an extreme
        if(Math.Abs(transform.position.y - downMark.position.y) <= 0.01f || Math.Abs(transform.position.y - upMark.position.y) <= 0.01f)
            goUp = !goUp; //Goes to the other
    }
}

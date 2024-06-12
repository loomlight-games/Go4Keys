using UnityEngine;

//Creates new tab in Create menu
[CreateAssetMenu (fileName = "Rotatory" ,  menuName = "GameItems/Rotatory", order = 0)]

//DEFINES PROPERTIES OF ROTATIONAL OBJECTS 

public class RotatorySO : ScriptableObject
{
    //Speeds of rotation
    public float Xspeed, Yspeed, Zspeed;
}
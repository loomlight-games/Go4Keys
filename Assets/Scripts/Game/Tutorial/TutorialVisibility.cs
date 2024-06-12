using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//HANDLES SERIALIZATION OF OBJECT VISIBILITY

[Serializable]
public class TutorialVisibility : ISerializable
{
    //Properties to save
    bool isShown;

    //Default constructor
    public TutorialVisibility(){}

    //Constructor given value
    public TutorialVisibility(bool isShown)
    {
        this.isShown = isShown;
    }

    //Saves in memory
    public void Serialize()
    {
        TutorialVisibility data = this;

        string fileName = "tutorialvisibility.json";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(fileName);//Creates file with that name
        bf.Serialize(fs, data);//Writes information in file
        fs.Close();//Close file
    }

    //Reads from memory
    public bool Deserialize()
    {
        string fileName = "tutorialvisibility.json";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.OpenRead(fileName);//Open and read file with that name
        TutorialVisibility data = (TutorialVisibility) bf.Deserialize(fs);//Extracts information from file
        fs.Close();//Close file

        return data.isShown;
    }
}

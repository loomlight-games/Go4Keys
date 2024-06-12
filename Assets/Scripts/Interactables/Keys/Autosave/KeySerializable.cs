using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

//HANDLES SERIALIZATION OF KEY COUNT

[Serializable]
public class KeySerializable : ISerializable
{
    //Properties to save
    int keyCount;

    //Default constructor
    public KeySerializable() { }

    //Constructor given value
    public KeySerializable(int count)
    {
        keyCount = count;
    }

    public bool Deserialize()
    {
        DeserializeInt(); return true;
    }

    public int DeserializeInt()
    {
        string fileName = "keyCount.json";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.OpenRead(fileName);//Open and read file with that name
        KeySerializable data = (KeySerializable)bf.Deserialize(fs);//Extracts information from file
        fs.Close();//Close file

        return data.keyCount;
    }

    public void Serialize()
    {
        KeySerializable data = this;

        string fileName = "keyCount.json";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = File.Create(fileName);//Creates file with that name
        bf.Serialize(fs, this);//Writes information in file
        fs.Close();//Close file
    }
}
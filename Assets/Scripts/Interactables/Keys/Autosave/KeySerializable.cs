using System;

/// <summary>
/// Handles serialization of key count
/// </summary>
public class KeySerializable : Serializable<int>
{
    /*
    int keyCount;

    public void Serialize(int keyCount)
    {
        this.keyCount = keyCount;

        string fileName = "keyCount.json";
        BinaryFormatter formatter = new ();
        FileStream jsonFile = File.Create(fileName); //Creates file with that name
        formatter.Serialize(jsonFile, this); //Writes information in file
        jsonFile.Close(); //Close file
    }

    public int Deserialize()
    {
        string fileName = "keyCount.json";
        BinaryFormatter formatter = new ();
        FileStream jsonFile = File.OpenRead(fileName); //Open and read file with that name
        KeySerializable data = (KeySerializable) formatter.Deserialize(jsonFile); //Extracts information from file
        jsonFile.Close(); //Close file

        return data.keyCount;
    }
    */
}
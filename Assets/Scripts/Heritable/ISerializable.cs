//SERIALIZABLE INTERFACE
public interface ISerializable
{
    //Saves in memory
    public void Serialize();

    //Reads from memory
    public bool Deserialize();
}
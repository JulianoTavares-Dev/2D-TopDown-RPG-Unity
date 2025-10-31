using UnityEngine;

//Define serialized variables for the position of the player
[System.Serializable]
public class SerializableVector3
{
    public float x;
    public float y;
    public float z;

    /// <summary>
    /// This method sets the position of the player using x, y and z
    /// to the x, y and z variables created above.
    /// </summary>
    /// <param name="vector3"></param>
    public SerializableVector3(Vector3 vector3)
    {
        x = vector3.x;
        y = vector3.y;
        z = vector3.z;
    }

    /// <summary>
    /// This method returns those variables as vector 3.
    /// </summary>
    /// <returns></returns>
    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}


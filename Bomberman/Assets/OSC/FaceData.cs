using UnityEngine;
using System.Collections;
// custom data type to store information for a face
public class FaceData
{
    int faceId;
    float x;
    float y;
    public FaceData(int id, float x, float y)
    {
        faceId = id;
        this.x = x;
        this.y = y;
    }
    public float getX()
    { 
        //Configurable para la sensibilidad de movimiento
        return x / 10;
    }
    public float getY()
    {
        //Configurable para la sensibilidad de movimiento
        return y / 10;
    }
    public int getID()
    {
        return faceId;
    }
}
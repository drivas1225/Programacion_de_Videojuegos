using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveFaceData : MonoBehaviour {

    // Create the OSC object (1 per scene) by creating
    // a empty game object, add the OSC.cs script
    // and select it in any other object that requires
    // OSC data, like this one
    public OSC osc;
    public GameObject pad;
    int iteration = 1;
    List<FaceData> faces;
    GameObject[] balls;
    float x_var;
    float y_var;
    float pad_top;
    float pad_bttm;
    float pad_right;
    float pad_left;
    Vector2 pad_tile_size;
    public GameObject spawner;
    
    // Use this for initialization
    void Start() {
        faces = new List<FaceData>();
        createBalls();
        osc.SetAllMessageHandler(OnReceive);
        Vector3 Sizes = pad.GetComponent<Renderer>().bounds.size;
        pad_top = pad.gameObject.transform.position.y + Sizes[1] / 2;
        pad_bttm = pad.gameObject.transform.position.y - Sizes[1] / 2;
        pad_left = pad.gameObject.transform.position.x - Sizes[0] / 2;
        pad_right = pad.gameObject.transform.position.x + Sizes[0] / 2;
        pad_tile_size = new Vector2(Sizes[0] / 3, Sizes[1] / 3);
        Debug.Log("Top:" + pad_top + " Bttm: " + pad_bttm + " Left: " + pad_left + " Right: " + pad_right);
    }

    void createBalls()
    {
        int count = 10;
        balls = new GameObject[count]; // up to 10 balls move according to faces tracked
        for (int i=0;i<1;i++)
        {
            balls[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            balls[i].GetComponent<Renderer>().material.color = new Color(255, 255, 255, 1);
            balls[i].transform.localScale = new Vector3(0.43f, 0.32f, 10);
        }
    }

    /* For faceData the structure is:
     * - number of faces
     * - face id, x-coord, y-coord(repeated)
     * x and y coordinates are according to processing's pixel coordinates
     */
    void OnReceive(OscMessage message)
    {
        int numFaces = message.GetInt(0);
        faces = new List<FaceData>();
        for (int i = 0; i < numFaces; i++)
        {
            int faceID = message.GetInt(i * 3 + 1);
            float xCoord = message.GetFloat(i * 3 + 2) * -1;
            float yCoord = message.GetFloat(i * 3 + 3);
            if (iteration == 1)
            {
                
                x_var = xCoord / 10 * -1 + pad.gameObject.transform.position.x;
                y_var = yCoord / 10 * -1 + pad.gameObject.transform.position.y;
                iteration++;
            }
            FaceData face = new FaceData(faceID, xCoord, yCoord);
            faces.Add(face);

            //print(faceID + " " + xCoord + " " + yCoord);
        }
    }

    // Update is called once per frame
    void Update() {
        //move the balls to where the faces are
        for(int i=0;i<faces.Count; i++)
        {
            // stop if there are more faces than balls
            if(i > balls.Length - 1)
            {
                break;
            }
            float x_tranformed = faces[i].getX() + x_var ;
            float y_transformed = faces[i].getY() + y_var ;
            if(y_transformed>=pad_top)
            {
                y_transformed = pad_top;
            }
            if (y_transformed<= pad_bttm)
            {
                y_transformed = pad_bttm;
            }
            if(x_tranformed >= pad_right)
            {
                x_tranformed = pad_right;
            }
            if(x_tranformed <= pad_left)
            {
                x_tranformed = pad_left;
            }
            balls[i].transform.position = new Vector3(x_tranformed,y_transformed, 0);
            GetComponentInParent<PlayerMovement>().movment.x = 0;
            GetComponentInParent<PlayerMovement>().movment.y = 0;
            bool top = false;
            bool bttm = false;
            bool right = false;
            bool left = false;
            if(y_transformed >= pad_bttm + pad_tile_size.y * 2 )
            {
                top = true;
            }
            if(y_transformed<=pad_bttm + pad_tile_size.y)
            {
                bttm = true;
            }
            if(x_tranformed>=pad_left + pad_tile_size.x * 2)
            {
                right = true;
            }
            if(x_tranformed<=pad_left + pad_tile_size.x)
            {
                left = true;
            }
            if((top && right) || (top && left) || (bttm && right) || (bttm &&left))
            {
                spawner.GetComponent<BombSpawner>().activeBomb();
            } else
            {
                if(top)
                {
                    GetComponentInParent<PlayerMovement>().movment.y = 1;
                } else if (bttm)
                {
                    GetComponentInParent<PlayerMovement>().movment.y = -1;
                } else if (right)
                {
                    GetComponentInParent<PlayerMovement>().movment.x = 1;
                } else if (left)
                {
                    GetComponentInParent<PlayerMovement>().movment.x = -1;
                }
            }
        }

    }

}

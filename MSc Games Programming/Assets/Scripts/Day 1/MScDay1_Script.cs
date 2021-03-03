using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MScDay1_Script : MonoBehaviour
{
    //variables are like containers that contain values that can change
    public int Integer; //ints can only be whole numbers
    int PrivateInteger;
    public float Float; //floats can be decimals
    public bool Bool; //bools are true or false
    public string String; //text
    public Vector3 scaleVector;
    public Rigidbody myRB;

    // Start is called before the first frame update
    void Start() //this is a function that happens at the start of the game
    { //function begins

        if (Bool == true)  
        {
            print("Hello World " + Integer + " " + Float + " " + String); //prints something to the console, in this case a string saying Hello World - the quotations tell the IDE that this is a string
            gameObject.transform.localScale = scaleVector; //sets the gameobject's scale to the Float variable
        }
        

    }//function ends

    // Update is called once per frame
    void Update() //this is a function that happens every frame
    {
        if (Bool == true)
        {
            print("Hello World " + Integer + " " + Float + " " + String); //prints something to the console, in this case a string saying Hello World - the quotations tell the IDE that this is a string
            gameObject.transform.localScale = new Vector3(Random.Range(0.5f, 7f), transform.localScale.y, transform.localScale.z); // add f at the end of a double to convert to float
            myRB.isKinematic = false;
        }

        else if (Bool == false)
        {
            myRB.isKinematic = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Roll2Die player script
public class Player : MonoBehaviour
{
    //tiles [1][2][3] represent  
    private int playerPosition;
    private bool playerCrouched;

    //array needed to dynamically store all attack boxes created by player
    //so the attack boxes can be instantiated/referenced/destroyed 
    private GameObject[] attackBoxes;

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = 2; //spawn on middle tile
        moveToTile2(); //force set object to middle tile
        playerCrouched = false;
    }

    // Update is called once per frame
    void Update()
    {
        //checking for player movement with side keys
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveLeftOneTile();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            moveRightOneTile();
        }
    }

    //update on every game engine tick
    void FixedUpdate()
    {
        
    }

    //generalized method for setting object's position
    private void moveObjPosition(float x, float y, float z)
    {
        this.transform.position = new Vector3(x, y, z);
    }

    //middle ground methods to move between tiles 1/2/3 with checks
    public void moveRightOneTile()
    {
        if (!playerCrouched)
        {
            if (playerPosition == 1)
            {
                playerPosition = 2;
                moveToTile2();
            }
            else if (playerPosition == 2)
            {
                playerPosition = 3;
                moveToTile3();
            }
        }
    }

    public void moveLeftOneTile()
    {
        if (!playerCrouched)
        {
            if (playerPosition == 2)
            {
                playerPosition = 1;
                moveToTile1();
            }
            else if (playerPosition == 3)
            {
                playerPosition = 2;
                moveToTile2();
            }
        }
    }

    //forced movement to tile position 1/2/3 ignores checks
    public void moveToTile1()
    {
        moveObjPosition( (float) -4.25, (float) 0.25, (float) 0.0);
    }

    public void moveToTile2()
    {
        moveObjPosition((float) -0.25, (float) 0.25, (float) 0.0);
    }

    public void moveToTile3()
    {
        moveObjPosition((float) 3.75, (float) 0.25, (float) 0.0);
    }
}

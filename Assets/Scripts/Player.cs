using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Roll2Die player script
public class Player : MonoBehaviour
{
    private new Rigidbody rigidbody;

    //player stats
    public int hitPoints;
    public int manaPoints;
    public int strengthStat;
    public int intelligenceStat;
    public int dexterityStat;
    public int vitalityStat;
    public int wisdomStat;
    public int luckStat;
    public int hpPotion;
    public int mpPotion;

    //tiles [1][2][3] represent  
    private int playerPosition;

    //used to keep track if player is crouched to limit movement
    private bool playerCrouched;

    //coroutines
    IEnumerator hpRegenRoutine;
    IEnumerator mpRegenRoutine;

    //array needed to dynamically store all attack boxes created by player
    //so the attack boxes can be instantiated/referenced/destroyed 
    private GameObject[] attackBoxes;

    public Fireball fireballPrefab;
    //private GameObject[] firedProjectiles;



    //Start is called before the first frame update
    void Start()
    {
        playerPosition = 2; //spawn on middle tile
        moveToTile2(); //force set object to middle tile
        playerCrouched = false;

        rigidbody = GetComponent<Rigidbody>();

        //setting default stats
        hitPoints = 100;
        manaPoints = 100;
        strengthStat = 10;
        intelligenceStat = 10;
        dexterityStat = 10;
        vitalityStat = 10;
        wisdomStat = 10;
        luckStat = 10;
        hpPotion = 3;
        mpPotion = 3;

        //setting coroutines
        hpRegenRoutine = hpRegen();
        mpRegenRoutine = mpRegen();

        //starting coroutines
        //startPlayerCoroutines();
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnProjectile();
        }
    }

    //update on every game engine tick
    void FixedUpdate()
    {
        
    }

    //Coroutines functions start
    IEnumerator hpRegen()//makes sure the player regens hp based on vitality stat
    {
        float seconds;

        while (true)
        {
            hitPoints++;
            seconds = ((100 - vitalityStat) / 2);
            yield return new WaitForSeconds(seconds);
        }
    }

    IEnumerator mpRegen()//makes sure the player regens mp based on wisdom stat
    {
        float seconds;

        while (true)
        {
            manaPoints++;
            seconds = ((100 - wisdomStat) / 2);
            yield return new WaitForSeconds(seconds);
        }
    }

    public void startPlayerCoroutines()
    {
        StartCoroutine(hpRegenRoutine);
        StartCoroutine(mpRegenRoutine);
    }

    public void stopPlayerCoroutines()
    {
        StopCoroutine(hpRegenRoutine);
        StopCoroutine(mpRegenRoutine);
    }
    //Coroutines functions end


    //Movement functions start
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
    //movement functions end

    //projectile functions start
    private void spawnProjectile()
    {
        float castDistance = 2;
        float castHeight = .5f;
        Fireball newFireball = Instantiate(fireballPrefab);
        newFireball.moveObjPosition(transform.position.x, transform.position.y + castHeight, transform.position.z + castDistance);
    }

}

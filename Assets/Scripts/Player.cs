using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Roll2Die player script
public class Player : MonoBehaviour
{
    public readonly float maxHp = 100;
    public readonly float maxMp = 100;
    public readonly float regenSpeed = 10;

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
    public bool invulnerable;

    //prefabs player will need
    public Fireball fireballPrefab;
    public AttackBox atkBoxPrefab;

    //UI elements and their references that need to be updated
    public HealthBar hpBar;
    public HealthBar mpBar;
    public Text hpBarText;
    public Text mpBarText;
    public Text hpPotButtonText;
    public Text mpPotButtonText;

    //tiles [1][2][3] represent  
    private int playerPosition;

    //used to keep track if player is crouched to limit movement
    private bool playerCrouched;

    //coroutines
    private IEnumerator hpRegenRoutine;
    private IEnumerator mpRegenRoutine;

    private AttackBox currAtkBox;
    private new Rigidbody rigidbody;
    
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
        vitalityStat = 90; //this should never go over 100 or regen code breaks
        wisdomStat = 90; //this should never go over 100 or regen code breaks
        luckStat = 10;

        //setting pots defaults
        hpPotion = 3;
        hpPotButtonText.text = "" + hpPotion;
        mpPotion = 3;
        mpPotButtonText.text = "" + mpPotion;

        invulnerable = false;

        //setting health hud's defaults
        hpBar.setSliderMaxValue((int) maxHp);
        hpBar.setSliderValue(hitPoints);
        hpBarText.text = hitPoints + "/" + maxHp;

        mpBar.setSliderMaxValue((int) maxMp);
        mpBar.setSliderValue(manaPoints);
        mpBarText.text = manaPoints + "/" + maxMp;

        //setting coroutines
        hpRegenRoutine = hpRegen();
        mpRegenRoutine = mpRegen();

        //starting coroutines
        startPlayerCoroutines();
    }

    public void takeDamage(int damage)
    {
        hitPoints -= damage;
        hpBar.setSliderValue(hitPoints);
        hpBarText.text = hitPoints + "/" + maxHp;
    }

    public void useHpPot()
    {
        if(maxHp != hitPoints && hpPotion > 0)
        {
            hitPoints = 100;
            hpPotion--;
            hpBar.setSliderValue(hitPoints);
            hpBarText.text = hitPoints + "/" + maxHp;
            hpPotButtonText.text = "" + hpPotion; //has to be after the decrement to update properly
        }
    }

    public void useMpPot()
    {
        if(maxMp != manaPoints && mpPotion > 0)
        {
            manaPoints = 100;
            mpPotion--;
            mpBar.setSliderValue(manaPoints);
            mpBarText.text = manaPoints + "/" + maxMp;
            mpPotButtonText.text = "" + mpPotion; //has to be after the decrement to update properly
        }
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

        //check to see if player wants to spawn a projectile
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnProjectile();
        }


        //check to see if player wants to use an HP potion
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            useHpPot();
        }

        //check to see if player wants to use an MP potion
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            useMpPot();
        }

        //0 for primary button
        //1 for secondary button
        //2 for middle button
        if (Input.GetMouseButtonDown(0)) //checking if mouse button is held down
        {
            //Debug.Log(MousePosition3D.GetMouseWorldPosition());

            if(currAtkBox == null) //creates an attack box on the initial click
            {
                currAtkBox = Instantiate(atkBoxPrefab);
                currAtkBox.setBoolMouseReleased(false);
                currAtkBox.setMousePressedPoint(MousePosition3D.GetMouseWorldPosition());
                //the reason why setMouseReleasedPoint is set here is just so things dont throw
                //null errors when checked
                currAtkBox.setMouseReleasedPoint(MousePosition3D.GetMouseWorldPosition());
            }
            else //updates the release point as the mouse is dragged around
            {
                currAtkBox.setMouseReleasedPoint(MousePosition3D.GetMouseWorldPosition());
            }

            
        }
        if ( !(Input.GetMouseButton(0)) ) //checking if mouse button not held down
        {
            if(currAtkBox != null)
            {
                currAtkBox.setBoolMouseReleased(true);
            }
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
            if (maxHp > hitPoints)
            {
                hitPoints++;
                hpBar.setSliderValue(hitPoints);
                hpBarText.text = hitPoints + "/" + maxHp;
            }
            //the yield statement must be outside the if statement to avoid an infinite loop
            seconds = ((100 - vitalityStat) / regenSpeed);
            yield return new WaitForSeconds(seconds);
        }
    }

    IEnumerator mpRegen()//makes sure the player regens mp based on wisdom stat
    {
        float seconds;

        while (true)
        {
            if (maxMp > manaPoints)
            {
                manaPoints++;
                mpBar.setSliderValue(manaPoints);
                mpBarText.text = manaPoints + "/" + maxMp;
            }
            //the yield statement must be outside the if statement to avoid an infinite loop
            seconds = ((100 - wisdomStat) / regenSpeed);
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

    //this is here so I can let the player have varying degrees of invulnerability
    //depending on the situation like getting attacked or activating an ability
    //that grants invulerability
    IEnumerator playerInvulTimer(float seconds)
    {
        invulnerable = true;
        yield return new WaitForSeconds(seconds);
        invulnerable = false;
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
        if(manaPoints >= 25)
        {
            manaPoints -= 25;
            mpBar.setSliderValue(manaPoints);
            mpBarText.text = manaPoints + "/" + maxMp;


            //setting spawn distance away from player
            float castDistance = 2;
            float castHeight = .5f;
            //creating new instance of fireball prefab
            Fireball newFireball = Instantiate(fireballPrefab);
            //setting damage on fireball depending on the player's int stat
            newFireball.setDamage((2 * intelligenceStat) + 25);
            newFireball.moveObjPosition(transform.position.x, transform.position.y + castHeight, transform.position.z + castDistance);
        }

    }

}

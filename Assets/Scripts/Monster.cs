using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public int hitPoints;
    public bool attacking;
    public MonsterProjectile monsterProjectilePrefab;

    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 400;
        attacking = false;

        //calling tester routine
        StartCoroutine("spawnProjectileTesterCoroutine");
    }

    //tester coroutine for testing out all projectile spawn locations
    IEnumerator spawnProjectileTesterCoroutine()
    {
        float seconds = 2f;

        //for loop to test creating projectiles
        for (int count = 7; count < 19; count++)
        {
            createProjectile(7, 15);
            if (count == 18)
            {
                count = 6;
            }
            yield return new WaitForSeconds(seconds);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
      
    }

    public void createProjectile(int spawnLocation, int damage)
    {
        float x = 0, y = 0, z = 0;
        bool createProjectile = true;
        MonsterProjectile newProjectile = Instantiate(monsterProjectilePrefab);
        newProjectile.setProjectileDamage(damage);

        switch (spawnLocation)
        {
            // spawns 7/8/9 are the frontal projectiles
            case 7:
                x = -4.25f; y = .5f; z = 36f;
                newProjectile.rotateObj(-90, 0, 0);
                newProjectile.Launch("-z");
                break;
            case 8:
                x = -.25f; y = .5f; z = 36f;
                newProjectile.rotateObj(-90, 0, 0);
                newProjectile.Launch("-z");
                break;
            case 9:
                x = 3.75f; y = .5f; z = 36f;
                newProjectile.rotateObj(-90, 0, 0);
                newProjectile.Launch("-z");
                break;

            // spawns 11/12 are the left/right spawns respectively
            case 11:
                x = -12.25f; y = .5f; z = 0f;
                newProjectile.rotateObj(0, 0, -90);
                newProjectile.Launch("x");
                break;
            case 12:
                x = 11.75f; y = .5f; z = 0f;
                newProjectile.rotateObj(0, 0, 90);
                newProjectile.Launch("-x");
                break;

            //spawns 13/14/15 are the falling projectiles spawns (spawns above the player)
            case 13:
                x = -4.25f; y = 16.25f; z = 0f;
                newProjectile.rotateObj(180, 0, 0);
                newProjectile.Launch("-y");
                break;
            case 14:
                x = -.25f; y = 16.25f; z = 0f;
                newProjectile.rotateObj(180, 0, 0);
                newProjectile.Launch("-y");
                break;
            case 15:
                x = 3.75f; y = 16.25f; z = 0f;
                newProjectile.rotateObj(180, 0, 0);
                newProjectile.Launch("-y");
                break;

            //spawns 16/17/18 are the rising projectiles spawns (spawns below the player)
            case 16:
                x = -4.25f; y = -15.75f; z = 0f;
                newProjectile.rotateObj(0, 0, 0);
                newProjectile.Launch("y");
                break;
            case 17:
                x = -.25f; y = -15.75f; z = 0f;
                newProjectile.rotateObj(0, 0, 0);
                newProjectile.Launch("y");
                break;
            case 18:
                x = 3.75f; y = -15.75f; z = 0f;
                newProjectile.rotateObj(0, 0, 0);
                newProjectile.Launch("y");
                break;

            default:
                createProjectile = false;
                Destroy(newProjectile);
                Debug.Log("Monsterscript createProjectile Switch Statement: Spawn Location Invalid. ");
                break;
        }

        if (createProjectile)
        {
            newProjectile.moveObjPosition(x, y, z);
            //need a way to set projectiles "velocity direction"
            //probably will put this in the switch statements
        }
    }


}

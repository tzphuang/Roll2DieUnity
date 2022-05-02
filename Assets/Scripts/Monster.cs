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


        IEnumerator attackPattern1 = spawnProjectileChain(1f);
        StartCoroutine(attackPattern1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
      
    }

    //tester coroutine to make sure spawning works
    IEnumerator spawnProjectileChain(float seconds)
    {
        createProjectile(7, 15);
        yield return new WaitForSeconds(seconds);
        createProjectile(8, 15);
        yield return new WaitForSeconds(seconds);
        createProjectile(9, 15);
        yield return new WaitForSeconds(seconds);

        createProjectile(11, 15);
        yield return new WaitForSeconds(seconds);
        createProjectile(12, 15);
        yield return new WaitForSeconds(seconds);

        createProjectile(13, 15);
        yield return new WaitForSeconds(seconds);
        createProjectile(14, 15);
        yield return new WaitForSeconds(seconds);
        createProjectile(15, 15);
        yield return new WaitForSeconds(seconds);

        createProjectile(16, 15);
        yield return new WaitForSeconds(seconds);
        createProjectile(17, 15);
        yield return new WaitForSeconds(seconds);
        createProjectile(18, 15);
        yield return new WaitForSeconds(seconds);
    }

    public void createProjectile(int spawnLocation, int damage)
    {
        string orientation;
        MonsterProjectile newProjectile = Instantiate(monsterProjectilePrefab);
        newProjectile.setProjectileDamage(damage);
        newProjectile.setDefaults();

        switch (spawnLocation)
        {
            // spawns 7/8/9 are the frontal projectiles
            case 7:
                newProjectile.moveObjPosition(-4.25f, .5f, 36f);
                newProjectile.rotateObj(-90, 0, 0);
                orientation = "-z";
                newProjectile.Launch(orientation);
                break;
            case 8:
                newProjectile.moveObjPosition(-.25f, .5f, 36f);
                newProjectile.rotateObj(-90, 0, 0);
                orientation = "-z";
                newProjectile.Launch(orientation);
                break;
            case 9:
                newProjectile.moveObjPosition(3.75f, .5f, 36f);
                newProjectile.rotateObj(-90, 0, 0);
                orientation = "-z";
                newProjectile.Launch(orientation);
                break;

            // spawns 11/12 are the left/right spawns respectively
            case 11:
                newProjectile.moveObjPosition(-12.25f, .5f, 0f);
                newProjectile.rotateObj(0, 0, -90);
                orientation = "+x";
                newProjectile.Launch(orientation);
                break;
            case 12:
                newProjectile.moveObjPosition(11.75f, .5f, 0f);
                newProjectile.rotateObj(0, 0, 90);
                orientation = "-x";
                newProjectile.Launch(orientation);
                break;

            //spawns 13/14/15 are the falling projectiles spawns (spawns above the player)
            case 13:
                newProjectile.moveObjPosition(-4.25f, 16.25f, 0f);
                newProjectile.rotateObj(180, 0, 0);
                orientation = "-y";
                newProjectile.Launch(orientation);
                break;
            case 14:
                newProjectile.moveObjPosition(-.25f, 16.25f, 0f);
                newProjectile.rotateObj(180, 0, 0);
                orientation = "-y";
                newProjectile.Launch(orientation);
                break;
            case 15:
                newProjectile.moveObjPosition(3.75f, 16.25f, 0f);
                newProjectile.rotateObj(180, 0, 0);
                orientation = "-y";
                newProjectile.Launch(orientation);
                break;

            //spawns 16/17/18 are the rising projectiles spawns (spawns below the player)
            case 16:
                newProjectile.moveObjPosition(-4.25f, -15.75f, 0f);
                orientation = "+y";
                newProjectile.Launch(orientation);
                break;
            case 17:
                newProjectile.moveObjPosition(-.25f, -15.75f, 0f);
                orientation = "+y";
                newProjectile.Launch(orientation);
                break;
            case 18:
                newProjectile.moveObjPosition(3.75f, -15.75f, 0f);
                orientation = "+y";
                newProjectile.Launch(orientation);
                break;

            default:
                Debug.Log("Monsterscript createProjectile Switch Statement: Spawn Location Invalid. ");
                Destroy(newProjectile);
                break;
        }
    }


}

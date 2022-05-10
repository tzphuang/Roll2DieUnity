using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monster : MonoBehaviour
{
    public readonly int maxHp = 400;
    public readonly float waitTime = 1f; //time in between checks for next atk pattern
    public int hitPoints;
    public int monsterDamage;
    public bool currentlyAttacking;
    public MonsterProjectile monsterProjectilePrefab;
    public HealthBar hpBar;
    public Text hpBarText;
    private IEnumerator attackCoroutine;

    //Audio
    public AudioClip EnemyDamageSound;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        hitPoints = 400;
        hpBarText.text = hitPoints + "/" + maxHp;
        currentlyAttacking = false;
        monsterDamage = 15;

        hpBar.setSliderMaxValue(maxHp);
        hpBar.setSliderValue(hitPoints);

        attackCoroutine = continuousAtkRoutine();
        StartCoroutine(attackCoroutine);

        audioSource = GetComponent<AudioSource>();
    }

    public void takeDamage(int damage)
    {
        hitPoints -= damage;
        hpBar.setSliderValue(hitPoints);
        hpBarText.text = hitPoints + "/" + maxHp;
        audioSource.PlayOneShot(EnemyDamageSound);
    }

    IEnumerator continuousAtkRoutine()
    {
        while (true)
        {
            if (!currentlyAttacking)
            {
                callRandomAtkPattern();
            }

            //this is just to check if the monster is not attacking every
            //"waitTime" number of seconds and call a random attack
            yield return new WaitForSeconds(waitTime); 
        }
    }

    private void callRandomAtkPattern()
    {
        //inclusive range [1,2,3,4]
        int randomNumber = Random.Range(1, 4);

        switch (randomNumber)
        {
            case 1:
                StartCoroutine("atkPattern1");
                break;

            case 2:
                StartCoroutine("atkPattern2");
                break;

            case 3:
                StartCoroutine("atkPattern3");
                break;

            case 4:
                StartCoroutine("atkPattern4");
                break;

            default:
                Debug.Log("Monster:CallRandomAtkPattern:Switch:Default_Statement_hit: This shouldnt happen");
                break;
        }
    }

    //forced to the right than to the middle
    IEnumerator atkPattern1()
    {
        currentlyAttacking = true;

        createProjectile(7, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(8, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(12, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(8, monsterDamage);
        yield return new WaitForSeconds(2f);
        createProjectile(15, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(16, monsterDamage);
        yield return new WaitForSeconds(2.5f);

        currentlyAttacking = false;
    }

    //forced to fireball or dodge game
    IEnumerator atkPattern2()
    {
        currentlyAttacking = true;

        createProjectile(7, monsterDamage);
        createProjectile(8, monsterDamage);
        yield return new WaitForSeconds(1.5f);
        createProjectile(8, monsterDamage);
        createProjectile(9, monsterDamage);
        yield return new WaitForSeconds(1.5f);
        createProjectile(7, monsterDamage);
        createProjectile(8, monsterDamage);
        yield return new WaitForSeconds(1.5f);
        createProjectile(7, monsterDamage);
        createProjectile(9, monsterDamage);
        yield return new WaitForSeconds(2.5f);

        currentlyAttacking = false;
    }

    //dodge middle, stay mid, right to left to right
    IEnumerator atkPattern3()
    {
        currentlyAttacking = true;

        createProjectile(8, monsterDamage);
        yield return new WaitForSeconds(2f);
        createProjectile(16, monsterDamage);
        createProjectile(18, monsterDamage);
        yield return new WaitForSeconds(2f);
        createProjectile(13, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(14, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(15, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(14, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(13, monsterDamage);
        yield return new WaitForSeconds(2.5f);

        currentlyAttacking = false;
    }

    //mirror of atkpatter1
    IEnumerator atkPattern4()
    {
        currentlyAttacking = true;

        createProjectile(9, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(8, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(11, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(8, monsterDamage);
        yield return new WaitForSeconds(2f);
        createProjectile(13, monsterDamage);
        yield return new WaitForSeconds(1f);
        createProjectile(18, monsterDamage);
        yield return new WaitForSeconds(2.5f);
        currentlyAttacking = false;
    }

    //tester coroutine to make sure spawning works
    IEnumerator spawnProjectileChain(float seconds)
    {
        createProjectile(7, monsterDamage);
        yield return new WaitForSeconds(seconds);
        createProjectile(8, monsterDamage);
        yield return new WaitForSeconds(seconds);
        createProjectile(9, monsterDamage);
        yield return new WaitForSeconds(seconds);

        createProjectile(11, monsterDamage);
        yield return new WaitForSeconds(seconds);
        createProjectile(12, monsterDamage);
        yield return new WaitForSeconds(seconds);

        createProjectile(13, monsterDamage);
        yield return new WaitForSeconds(seconds);
        createProjectile(14, monsterDamage);
        yield return new WaitForSeconds(seconds);
        createProjectile(15, monsterDamage);
        yield return new WaitForSeconds(seconds);

        createProjectile(16, monsterDamage);
        yield return new WaitForSeconds(seconds);
        createProjectile(17, monsterDamage);
        yield return new WaitForSeconds(seconds);
        createProjectile(18, monsterDamage);
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

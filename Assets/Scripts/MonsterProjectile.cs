using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    public readonly float DEFAULTSPEED = 12;
    private float speed;
    private new Rigidbody rigidbody;
    private Vector3 velocity;
    private int projectileDamage;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        speed = DEFAULTSPEED;
    }

    public void setProjectileDamage(int newDamage)
    {
        projectileDamage = newDamage;
    }

    public int getProjectileDamage()
    {
        return projectileDamage;
    }

    //example how this function works
    //velocity = "x"
    //that means the monster projectile will now fly from its spawn
    //with an increasing x, so
    //in second 1, position is 5
    //in second 2, position is 10
    //in second 3, position is 15
    public void Launch(string velocity)
    {
        //Debug.Log("inside launch switch statement.");
        switch (velocity)
        {   
            case "x":
                this.rigidbody.velocity = Vector3.right * speed;
                break;

            case "-x":
                this.rigidbody.velocity = Vector3.left * speed;
                break;

            case "y":
                this.rigidbody.velocity = Vector3.up * speed;
                break;

            case "-y":
                this.rigidbody.velocity = Vector3.down * speed;
                break;

            case "z":
                this.rigidbody.velocity = Vector3.forward * speed;
                break;

            case "-z":
                this.rigidbody.velocity = Vector3.back * speed;
                break;

            default:
                Debug.Log("MonsterProjectile:launch():switch statement: Invalid velocity");
                break;
        }

    }

    public void moveObjPosition(float x, float y, float z)
    {
        this.transform.position = new Vector3(x, y, z);
    }

    public void rotateObj(float x, float y, float z)
    {
        this.transform.Rotate(x, y, z, Space.Self);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rigidbody.velocity = rigidbody.velocity.normalized * speed;
        velocity = rigidbody.velocity;
    }
}

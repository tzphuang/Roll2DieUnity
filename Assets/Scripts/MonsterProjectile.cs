using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    public readonly float DEFAULTSPEED = 15f;
    private float speed;
    private new Rigidbody rigidbody;
    private Vector3 velocity;
    private int projectileDamage;

    // Start is called before the first frame update
    void Start()
    {

    }

    //so this is here because when using the start()
    //I call the launch() but it is trying to reference the rigidbody
    //before the start() is called
    //so instead i just set the defaults myself before any start() occurs
    //to pre-emptively avoid calling a rigid body that was not set yet
    //essentially i put the cart before the horse here
    public void setDefaults()
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
    public void Launch(string orientation)
    {
        //Debug.Log("inside launch switch statement.");
        switch (orientation)
        {   
            case "+x":
                this.rigidbody.velocity = Vector3.right * speed;
                break;

            case "-x":
                this.rigidbody.velocity = Vector3.left * speed;
                break;

            case "+y":
                this.rigidbody.velocity = Vector3.up * speed;
                break;

            case "-y":
                this.rigidbody.velocity = Vector3.down * speed;
                break;

            case "+z":
                this.rigidbody.velocity = Vector3.forward * speed;
                break;

            case "-z":
                this.rigidbody.velocity = Vector3.back * speed;
                break;

            default:
                Debug.Log("MonsterProjectile:launch():switch statement: Invalid orientation");
                break;
        }

    }

    public void moveObjPosition(float x, float y, float z)
    {
        this.transform.position = new Vector3(x, y, z);
    }

    public void rotateObj(float x, float y, float z)
    {
        //this.transform.Rotate(x, y, z, Space.Self);
        this.transform.Rotate(x, y, z);
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

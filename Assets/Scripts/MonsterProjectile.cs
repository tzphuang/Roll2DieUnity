using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterProjectile : MonoBehaviour
{
    private float speed;
    public readonly float DEFAULTSPEED = 12;

    private new Rigidbody rigidbody;
    private Vector3 velocity;

    private int projectileDamage;

    // Start is called before the first frame update
    void Start()
    {
        speed = DEFAULTSPEED;
    }

    void cast()
    {
        rigidbody.velocity = Vector3.forward * speed;
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
        //rigidbody.velocity = rigidbody.velocity.normalized * speed;
        //velocity = rigidbody.velocity;
    }

    public void setProjectileDamage(int newDamage)
    {
        projectileDamage = newDamage;
    }

    public int getProjectileDamage()
    {
        return projectileDamage;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private float speed = 12;
    private new Rigidbody rigidbody;
    private Vector3 velocity;
    private int damage;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        //delay the movement of the fireball for cast time
        Invoke("cast", .15f);
    }

    void cast()
    {
        rigidbody.velocity = Vector3.forward * speed;
    }

    public void moveObjPosition(float x, float y, float z)
    {
        this.transform.position = new Vector3(x, y, z);
    }

    public void setDamage(int newDamage)
    {
        damage = newDamage;
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Projectile")
        {
            Destroy(other.gameObject);
        }

        if(other.gameObject.tag == "Monster")
        {
            Monster currMonster = (Monster) other.GetComponent<Monster>();
            currMonster.takeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}

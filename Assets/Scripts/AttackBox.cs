using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    public readonly float startingZ = 42f;
    public readonly float endingZ = -2f;
    public readonly float adjustmentZ = -2f;
    private float startingX;
    private float endingX;
    private float startingY;
    private float endingY;

    private float length; //distance between the startingZ and endingZ
    private float width; //distance the mouse travels in the x-axis
    private float height; //distance the mouse travels in the y-axis

    private int damage;
    private bool mouseReleased;

    private Vector3 mousePressedPoint;
    private Vector3 mouseReleasedPoint;


    // Start is called before the first frame update
    void Start()
    {
        //mouseReleased = false;
    }

    public void setMousePressedPoint(Vector3 currPoint)
    {
        mousePressedPoint = currPoint;
    }

    public void setMouseReleasedPoint(Vector3 currPoint)
    {
        mouseReleasedPoint = currPoint;
    }

    public void setDamage(int currDamage)
    {
        damage = currDamage;
    }

    public void setBoolMouseReleased(bool currStatus)
    {
        mouseReleased = currStatus;
    }

    // Update is called once per frame
    void Update()
    {
        //only move the object and scale the object on every update frame
        width = startingX - endingX;
        height = startingY - endingY;
        length = startingZ - endingZ;

        moveObjPosition( (width/2), (height/2), (length/2) + adjustmentZ);
        setObjScale( Mathf.Abs(width), Mathf.Abs(height), Mathf.Abs(length));
    }


    private void FixedUpdate()
    {
        //recalculate the (x, y) positions of starting point/ending point constantly
        //to get an accurate account of the position in which the cube will be in 3d space
        startingX = mousePressedPoint.x;
        startingY = mousePressedPoint.y;

        endingX = mouseReleasedPoint.x;
        endingY = mouseReleasedPoint.y;
    }

    //generalized method for setting object's position
    private void moveObjPosition(float x, float y, float z)
    {
        this.transform.position = new Vector3(x, y, z);
    }

    private void setObjScale(float scaleX, float scaleY, float scaleZ)
    {
        this.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
    }

    //I want the attack box to constantly be triggering when created
    //checking if it has encountered a monster hitbox
    private void OnTriggerEnter(Collider other)
    {
        if (mouseReleased)
        {
            if(other.gameObject.tag == "Monster")
            {
                Monster currMonster = (Monster)other.GetComponent<Monster>();
                currMonster.takeDamage(damage);
            }

            //no matter what i want the attack box destroyed the following frame
            StartCoroutine("destroySelf");
        }
    }

    IEnumerator destroySelf()
    {
        //yield return null;
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}

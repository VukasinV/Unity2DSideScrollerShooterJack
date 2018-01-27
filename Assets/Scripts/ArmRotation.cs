using UnityEngine;
using System.Collections;

public class ArmRotation : MonoBehaviour

{
    //public Transform hitPoint;

    public bool direction;

    public CharacterWalk p_Script;
    int hp;
    public Camera cam;

    public int posOffset;

    void Start()

    {

        direction = true;
        p_Script = GetComponentInParent<CharacterWalk>();
    }

    void Update()

    {

        hp = p_Script.hp;




        //rotation
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5f;

        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        //Debug.Log(angle + "");

        if (direction == true) { posOffset = 25; }
        if (direction == false) { posOffset = 158; }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + posOffset)); //Rotating!




        if (angle > 0f && angle < 100f || angle < 0f && angle > -90f)
        {
            if (direction == false)

            {
                direction = true;

                Flip();
            }
        }

        if (angle > 100f && angle < 180f || angle < -90f && angle > -180f)

            if (direction == true)

            {
                direction = false;

                Flip();
            }

    }

    void Flip()

    {
        if (direction == false && p_Script._Direction == true || direction == true && p_Script._Direction == false)

        {

            p_Script.Flipp();
        }



        /*Vector3 theScale = transform.localScale;

        theScale.x *= -1;

        transform.localScale = theScale;*/


    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementHandler : MonoBehaviour
{
    public CharacterController controller;
    private float xvel, yvel, zvel;
    public float speed;
    public float friction;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Mathf.Sin(controller.gameObject.transform.rotation.eulerAngles.y*(Mathf.PI/180)) + " " + Mathf.Cos(controller.gameObject.transform.rotation.eulerAngles.y)*(Mathf.PI/180));

        if (Input.GetKey("w"))
        {
            xvel = speed * Mathf.Sin(controller.gameObject.transform.rotation.eulerAngles.y*(Mathf.PI/180));
            zvel = speed * Mathf.Cos(controller.gameObject.transform.rotation.eulerAngles.y*Mathf.PI/180);
        }
        if (Input.GetKey("s"))
        {
            xvel = speed * -Mathf.Sin(controller.gameObject.transform.rotation.eulerAngles.y*(Mathf.PI/180));
            zvel = speed * -Mathf.Cos(controller.gameObject.transform.rotation.eulerAngles.y*Mathf.PI/180);
        }
        if (Input.GetKey("d"))
        {
            zvel =speed * -Mathf.Sin(controller.gameObject.transform.rotation.eulerAngles.y*(Mathf.PI/180));
            xvel =speed * Mathf.Cos(controller.gameObject.transform.rotation.eulerAngles.y*Mathf.PI/180);
        }
        if (Input.GetKey("a"))
        {
            zvel =speed * Mathf.Sin(controller.gameObject.transform.rotation.eulerAngles.y*(Mathf.PI/180));
            xvel =speed * -Mathf.Cos(controller.gameObject.transform.rotation.eulerAngles.y*Mathf.PI/180);
        }
        if (Mathf.Abs(xvel) < .1)
        {
            xvel = 0;
        }
        if (Mathf.Abs(zvel) < .1)
        {
            zvel = 0;
        }
        if (controller.isGrounded)
        {
            xvel += xvel / -friction;
            zvel += zvel / -friction;
            //            Debug.Log("Yikes");

        }
        else
        {
            yvel = yvel - 9.8f * Time.deltaTime;
            //            Debug.Log("Yikes2");
        }
        controller.Move(new Vector3(xvel, yvel, zvel));
        //Debug.Log(controller.gameObject.transform.position);
              Debug.Log(xvel + " " + yvel + " " + zvel);
    }
}

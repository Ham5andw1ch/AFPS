using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementHandler : MonoBehaviour
{
    public CharacterController controller;
    private float xvel, yvel, zvel, slideTimer;
    public float speed, accSpeed, gravity, jvel, slidet, crouchSpeed, slideSpeed;
    private bool crouching = false, walking = false, jumped = false, jumpQueue = false, sliding = false, slideQueue = false,grounded = false;
    public float friction;
    // Start is called before the first frame update
    void Start()
    {
        crouching = false;
    }
    //layo
    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.RightShift)){
            Debug.Log("I made it");
            controller.Move(new Vector3(-controller.transform.position.x,-controller.transform.position.y+20,-controller.transform.position.z));
        }else

        if (grounded)
        {
            Debug.Log(sliding);
            
            jumped = false;
            yvel = 0;
            float oldx = xvel;
            float oldz = zvel;
            float curSpeed = Mathf.Sqrt(oldx * oldx + oldz * oldz);

                    Debug.Log("haha1");
            //Yes, jumping techinically happens a cycle after space pressed, but it's worth.
            if (Input.GetKey(KeyCode.Space))
            {
                if (!jumped && jumpQueue)
                {
                    jumpQueue = false;
                    jumped = true;
                    //Debug.Log("Adding lul");
                    yvel += jvel;
                }
            }
            else
            {
                jumpQueue = true;
            }
            float oldy = controller.transform.position.y;
            yvel = yvel - gravity * Time.deltaTime;
            controller.Move(new Vector3(0, yvel * Time.deltaTime, 0));
            if(controller.transform.position.y == oldy){
                grounded = true;
            }else{
                grounded = false;
            }


            int xin = 1, yin = 1;
            float velSpeed = accSpeed * Time.deltaTime;
            int[][] angles = new int[][] { new int[] { -45, 0, 45 }, new int[] { -90, -1, 90 }, new int[] { -135, 180, 135 } };
            if (!sliding)
            {
                if (Input.GetKey("w"))
                {
                    xin--;
                }
                if (Input.GetKey("s"))
                {
                    xin++;
                }
                if (Input.GetKey("d"))
                {
                    yin++;
                }
                if (Input.GetKey("a"))
                {
                    yin--;
                }
            }
            else
            {
                xin--;
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                crouching = true;
            }
            else
            {
                crouching = false;
            }
            if (slideQueue && !sliding && crouching)
            {
                curSpeed *= slideSpeed;
                slideQueue = false;
                slideTimer = slidet;
                sliding = true;
            }
            else if (crouching && sliding)
            {
                slideQueue = false;
                slideTimer -= Time.deltaTime;
                if ( curSpeed == speed){
                    curSpeed = crouchSpeed;
                    slideTimer = 0;
                    sliding = false;
                }
            }
            else if (!crouching)
            {
                slideQueue = false;
                sliding = false;
                slideTimer = 0;
            }
            else if (crouching)
            {
                slideQueue = false;
                velSpeed = crouchSpeed;
            }

            //            Debug.Log(curSpeed + " " + accSpeed + " " + speed);
            if(!crouching){
            if ((angles[xin][yin] != -1))
            {
                float newx = xvel + velSpeed * Mathf.Sin((controller.gameObject.transform.rotation.eulerAngles.y + angles[xin][yin]) * (Mathf.PI / 180));
                float newz = zvel + velSpeed * Mathf.Cos((controller.gameObject.transform.rotation.eulerAngles.y + angles[xin][yin]) * Mathf.PI / 180);
                if (!(Mathf.Sqrt(newx * newx + newz * newz) < speed))
                {
                    xvel = speed * Mathf.Sin((controller.gameObject.transform.rotation.eulerAngles.y + angles[xin][yin]) * (Mathf.PI / 180));
                    zvel = speed * Mathf.Cos((controller.gameObject.transform.rotation.eulerAngles.y + angles[xin][yin]) * Mathf.PI / 180);
                }
                else
                {
                    xvel = newx;
                    zvel = newz;
                }
            }
            } else {
                    xvel = curSpeed * Mathf.Sin((controller.gameObject.transform.rotation.eulerAngles.y + angles[xin][yin]) * (Mathf.PI / 180));
                    zvel = curSpeed * Mathf.Cos((controller.gameObject.transform.rotation.eulerAngles.y + angles[xin][yin]) * Mathf.PI / 180);

            }
            //Calculate Friction
            oldx = xvel;
            oldz = zvel;
            float oldspeed = Mathf.Sqrt(xvel * xvel + zvel * zvel);
            float xf = (-xvel / Mathf.Abs(xvel) * friction * Time.deltaTime * xvel) / oldspeed;
            float zf = (-zvel / Mathf.Abs(zvel) * friction * Time.deltaTime * zvel) / oldspeed;
            if (xvel != 0 && oldspeed != 0)
                xvel += (-xvel * friction * Time.deltaTime) / oldspeed;
            if (zvel != 0 && oldspeed != 0)
                zvel += (-zvel * friction * Time.deltaTime) / oldspeed;
            if (oldx / xvel <= 0)
                xvel = 0;
            if (oldz / zvel <= 0)
                zvel = 0;
        }

        else
        {
                    Debug.Log("haha2");
            if (!Input.GetKey(KeyCode.Space))
            {
                jumpQueue = true;
            }
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                slideQueue = true;
            }
            float oldy = controller.transform.position.y;
            yvel = yvel - gravity * Time.deltaTime;
            controller.Move(new Vector3(0, yvel * Time.deltaTime, 0));
            if(controller.transform.position.y == oldy && yvel <= -gravity * Time.deltaTime){
                grounded = true;
            }else{
                grounded = false;
            }
            //            Debug.Log("yvel " + yvel);
        }
        controller.Move(new Vector3(xvel * Time.deltaTime, 0, zvel * Time.deltaTime));
        //Debug.Log(controller.gameObject.transform.position);
        //        Debug.Log(xvel + " " + yvel + " " + zvel);
    }
}

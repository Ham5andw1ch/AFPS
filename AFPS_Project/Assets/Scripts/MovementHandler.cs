using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MovementHandler : MonoBehaviour
{
    public CharacterController controller;
    private float xvel, yvel, zvel;
    public float speed, accSpeed, gravity, jvel;
    private bool crouching = false, walking = false, jumped = false, jumpQueue = false;
    public float friction;
    // Start is called before the first frame update
    void Start()
    {
        crouching = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (controller.isGrounded)
        {
            jumped = false;
            yvel = 0;
            float oldx = xvel;
            float oldz = zvel;
            float curSpeed = Mathf.Sqrt(oldx * oldx + oldz * oldz);

            float velSpeed = accSpeed * Time.deltaTime;
            int[][] angles = new int[][]{
            new int [] {-45 ,0  ,45},
            new int [] {-90 ,-1 ,90},
            new int [] {-135,180,135}
        };
            int xin = 1, yin = 1;
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
            if (Input.GetKey(KeyCode.LeftShift))
            {
                if (crouching == false)
                {
                    crouching = true;
                    velSpeed = curSpeed * .2f;
                }
            }
            else
            {
                crouching = false;
            }

            Debug.Log(curSpeed + " " + accSpeed + " " + speed);
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


                Debug.Log("Vels: " + xvel + " " + zvel);

            }
            if (Input.GetKey(KeyCode.Space))
            {
                if(!jumped && jumpQueue){
                jumpQueue = false;
                jumped = true;
                Debug.Log("Adding lul");
                yvel += jvel;
                }
            } else{
                jumpQueue = true;
            }
            oldx = xvel;
            oldz = zvel;
            Debug.Log("speed Before Friction:" + curSpeed + "\toldspeed After Friction:" + Mathf.Sqrt(xvel * xvel + zvel * zvel));
            float oldspeed = Mathf.Sqrt(xvel * xvel + zvel * zvel);
            float xf = (-xvel / Mathf.Abs(xvel) * friction * Time.deltaTime * xvel) / oldspeed;
            float zf = (-zvel / Mathf.Abs(zvel) * friction * Time.deltaTime * zvel) / oldspeed;
            Debug.Log("fs: " + xf + " " + zf + " " + oldspeed);
            if (xvel != 0 && oldspeed != 0)
                xvel += (-xvel * friction * Time.deltaTime) / oldspeed;
            if (zvel != 0 && oldspeed != 0)
                zvel += (-zvel * friction * Time.deltaTime) / oldspeed;
            //Debug.Log("Vels: " + oldx + " " + oldz + " " + oldspeed);
            if (oldx / xvel <= 0)
                xvel = 0;
            if (oldz / zvel <= 0)
                zvel = 0;
            Debug.Log("Speed Before Friction:" + oldspeed + "\tSpeed After Friction:" + Mathf.Sqrt(xvel * xvel + zvel * zvel));

            //            Debug.Log("Yikes");

            //if (Mathf.Abs(xvel) < .001)
            //{
            //    xvel = 0;
            //}
            //if (Mathf.Abs(zvel) < .001)
            //{
            //    zvel = 0;
            //}
        }

        else
        {
            if (!Input.GetKey(KeyCode.Space))
            {
                jumpQueue = true;
            }   
            yvel = yvel - gravity * Time.deltaTime;
            Debug.Log("yvel " + yvel);
        }
        controller.Move(new Vector3(xvel * Time.deltaTime, yvel * Time.deltaTime, zvel * Time.deltaTime));
        //Debug.Log(controller.gameObject.transform.position);
        Debug.Log(xvel + " " + yvel + " " + zvel);
    }
}

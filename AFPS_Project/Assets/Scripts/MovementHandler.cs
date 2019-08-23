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
        int[][] angles = new int[][]{
            new int [] {-45 ,0  ,45},
            new int [] {-90 ,-1 ,90},
            new int [] {-135,180,135}
        };
        int xin = 1, yin =1;
        if (Input.GetKey("w")){
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
        
        Debug.Log(Mathf.Sin(controller.gameObject.transform.rotation.eulerAngles.y*(Mathf.PI/180)) + " " + Mathf.Cos(controller.gameObject.transform.rotation.eulerAngles.y)*(Mathf.PI/180));
        if(angles[xin][yin] != -1){
            xvel = speed * Mathf.Sin((controller.gameObject.transform.rotation.eulerAngles.y+angles[xin][yin])*(Mathf.PI/180));
            zvel = speed * Mathf.Cos((controller.gameObject.transform.rotation.eulerAngles.y+angles[xin][yin])*Mathf.PI/180);

        }
        if (controller.isGrounded)
        {
                float oldx = xvel;
                float oldz = zvel;
                if(xvel != 0)
                xvel += -xvel/Mathf.Abs(xvel) * friction;
                if(zvel != 0)
                zvel += -zvel/Mathf.Abs(zvel) * friction;
                Debug.Log("Vels: " + oldx + " " + xvel);
                if (oldx/xvel <= 0)
                    xvel = 0;
                if (oldz/zvel <= 0)
                    zvel = 0;

                //            Debug.Log("Yikes");
            
            if (Mathf.Abs(xvel) < .001)
            {
                xvel = 0;
            }
            if (Mathf.Abs(zvel) < .001)
            {
                zvel = 0;
            }
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

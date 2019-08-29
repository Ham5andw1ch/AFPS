using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    public Camera test;
    public float sensitivity;
    public GameObject boi;
    public bool invert;
    private int invertint;
    void Start()
    {
        if (invert)
        {
            invertint = 1;
        }
        else
        {
            invertint = -1;
        }
        Cursor.visible = false;
    }
    void Update()
    {
        if(Input.GetKey("p")){
            Debug.Log("Epic");
        Cursor.lockState = CursorLockMode.Locked;
        }
        if(Input.GetKey("o")){
        Cursor.lockState = CursorLockMode.None;
        }
        float xrot = Input.GetAxis("Mouse Y") * sensitivity;
        float yrot = Input.GetAxis("Mouse X") * sensitivity;
        boi.transform.Rotate(new Vector3(0,yrot, 0), Space.World);
        Debug.Log(invertint*xrot+test.transform.rotation.eulerAngles.x + " " + yrot);
        float finalNum = invertint*xrot+test.transform.rotation.eulerAngles.x;
        if(finalNum > 180 )
        finalNum-=360;
        if(finalNum > 30){
            test.transform.rotation=Quaternion.Euler(30,test.transform.rotation.eulerAngles.y,test.transform.rotation.eulerAngles.z);
        }
        else if(finalNum < -30){
            test.transform.rotation=Quaternion.Euler(-30,test.transform.rotation.eulerAngles.y,test.transform.rotation.eulerAngles.z);
        }else
        test.transform.Rotate(new Vector3(invertint*xrot,0, 0), Space.Self);
    

        //Debug.Log("Hello");
    }
}
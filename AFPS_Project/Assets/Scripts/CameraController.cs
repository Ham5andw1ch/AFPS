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
        Cursor.lockState = CursorLockMode.None;
    }
    void Update()
    {
        float xrot = Input.GetAxis("Mouse Y") * sensitivity;
        float yrot = Input.GetAxis("Mouse X") * sensitivity;
        boi.transform.Rotate(new Vector3(0,yrot, 0), Space.World);
        test.transform.Rotate(new Vector3(invertint*xrot,0, 0), Space.Self);
        //Debug.Log("Hello");
        //Debug.Log(Input.GetAxis("Mouse X") + " " + Input.GetAxis("Mouse Y"));
    }
}
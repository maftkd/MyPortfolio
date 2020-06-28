using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatCam : MonoBehaviour
{
    public float _baseSpeed;
    public float _mouseSensitivity;
    Quaternion _manualRot;
    public float _rotationLerp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 flatForward = transform.forward;
        Vector3 flatRight = transform.right;
        flatForward.y = 0;
        flatRight.y = 0;
        flatForward.Normalize();
        flatRight.Normalize();
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += flatForward * _baseSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= flatRight * _baseSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= flatForward * _baseSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += flatRight * _baseSpeed * Time.deltaTime;
        }
        GetRotateInput();
        transform.rotation = Quaternion.Slerp(transform.rotation, _manualRot, _rotationLerp * Time.deltaTime);
    }

    private void GetRotateInput()
    {
        Quaternion curRot = transform.rotation;
        transform.Rotate(-Input.GetAxis("Mouse Y") * _mouseSensitivity, Input.GetAxis("Mouse X") * _mouseSensitivity, 0, Space.Self);
        Vector3 eulers = transform.localEulerAngles;
        eulers.z = 0;
        transform.localEulerAngles = eulers;
        _manualRot = transform.rotation;
        transform.rotation = curRot;
        
    }
}

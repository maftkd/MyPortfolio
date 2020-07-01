using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatCam : MonoBehaviour
{
    public static bool _instanced = false;
    public float _baseSpeed;
    public float _mouseSensitivity;
    Quaternion _manualRot;
    public float _rotationLerp;

    public Vector2 _xClamp;
    public Vector2 _zClamp;

    public Vector2 _xClampAttic;
    public Vector2 _zClampAttic;

    public Vector2 _xClampBasement;
    public Vector2 _zClampBasement;

    public Vector2 _xClampBrightline;
    public Vector2 _zClampBrightline;

    public Vector2 _xClampFreelance;
    public Vector2 _zClampFreelance;
    // Start is called before the first frame update

    public bool _moveEnabled = true;

    private void Awake()
    {
        if (!_instanced)
        {
            DontDestroyOnLoad(transform.gameObject);
            _instanced = true;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

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
        Vector3 movement = Vector3.zero;
        if (_moveEnabled)
        {
            if (Input.GetKey(KeyCode.W))
            {
                //transform.position += flatForward * _baseSpeed * Time.deltaTime;
                movement += flatForward;
            }
            if (Input.GetKey(KeyCode.A))
            {
                //transform.position -= flatRight * _baseSpeed * Time.deltaTime;
                movement -= flatRight;
            }
            if (Input.GetKey(KeyCode.S))
            {
                //transform.position -= flatForward * _baseSpeed * Time.deltaTime;
                movement -= flatForward;
            }
            if (Input.GetKey(KeyCode.D))
            {
                //transform.position += flatRight * _baseSpeed * Time.deltaTime;
                movement += flatRight;
            }
        }
        movement.Normalize();
        transform.position += _baseSpeed * movement * Time.deltaTime;
        Vector3 unclamped = transform.position;
        unclamped.x = Mathf.Clamp(unclamped.x, _xClamp.x, _xClamp.y);
        unclamped.z = Mathf.Clamp(unclamped.z, _zClamp.x, _zClamp.y);
        transform.position = unclamped;
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

    public void SetClamp(int sceneIndex)
    {
        switch (sceneIndex)
        {
            case 1:
                _xClamp = _xClampAttic;
                _zClamp = _zClampAttic;
                break;
            case 2:
                _xClamp = _xClampBasement;
                _zClamp = _zClampBasement;
                break;
            case 3:
                _xClamp = _xClampBrightline;
                _zClamp = _zClampBrightline;
                break;
            case 4:
                _xClamp = _xClampFreelance;
                _zClamp = _zClampFreelance;
                break;
            default:
                break;
        }
    }

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.red;
        Gizmos.DrawCube(new Vector3(_xClamp.x, 0, 0), new Vector3(0,10f,10f));
        Gizmos.DrawCube(new Vector3(_xClamp.y, 0, 0), new Vector3(0,10f,10f));
        Gizmos.color = Color.green;
        Gizmos.DrawCube(new Vector3(0, 0, _zClamp.x), new Vector3(10f,10f,0));
        Gizmos.DrawCube(new Vector3(0, 0, _zClamp.y), new Vector3(10f,10f,0f));
        */
    }
}

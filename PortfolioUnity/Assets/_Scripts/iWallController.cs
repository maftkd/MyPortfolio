using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iWallController : MonoBehaviour
{
    public Transform _monitor;
    MeshRenderer _mesh;
    public float _minLocalX, _maxLocalX;
    public float _minTexOff, _maxTexOff;
    bool _left;
    public float _moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _mesh = _monitor.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float localX = _monitor.localPosition.x;
        if (_left)
        {
            localX += _moveSpeed * Time.deltaTime;
        }
        else
        {
            localX -= _moveSpeed * Time.deltaTime;
        }
        localX = Mathf.Clamp(localX, _maxLocalX, _minLocalX);
        _monitor.localPosition = Vector3.right * localX;
        float xLocal = Mathf.InverseLerp(_minLocalX, _maxLocalX, _monitor.localPosition.x);
        float xOffset = Mathf.Lerp(_minTexOff, _maxTexOff, xLocal);
        _mesh.materials[1].mainTextureOffset = Vector2.right * xOffset;
            //SetTextureOffset("_EmissionMap", Vector2.right * _monitor.localPosition.x) ;
    }

    public void ToggleDir()
    {
        _left = !_left;
    }
}

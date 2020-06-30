using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerBlinker : MonoBehaviour
{
    int _mainIndex = 3;
    public float _toggleRateMain;
    public float _toggleRateInfo;
    MeshRenderer _mesh;
    bool[] _lights = new bool[10];
    public Color _mainColor, _infoColor;
    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();
        for(int i=1; i<_lights.Length; i++)
        {
            _lights[i] = Random.value < .5f ? false : true;
            SetLight(i, _lights[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //main toggle
        if (Random.value < _toggleRateMain)
        {
            //toggle main
            _lights[_mainIndex] = !_lights[_mainIndex];
            SetLight(_mainIndex, _lights[_mainIndex]);
        }
        for(int i=1; i<_lights.Length; i++)
        {
            if (i != _mainIndex)
            {
                if(Random.value < _toggleRateInfo)
                {
                    _lights[i] = !_lights[i];
                    SetLight(i, _lights[i]);
                }
            }
        }
    }

    void SetLight(int index, bool isOn)
    {
        if (!isOn)
        {
            //_mesh.materials[index].SetColor("_Color", Color.black);
            _mesh.materials[index].SetColor("_EmissionColor", Color.black);
        }
        else
        {
            if (index == _mainIndex)
            {
                //_mesh.materials[index].SetColor("_Color", _mainColor);
                _mesh.materials[index].SetColor("_EmissionColor", _mainColor);
            }
            else
            {
                //_mesh.materials[index].SetColor("_Color", _infoColor);
                _mesh.materials[index].SetColor("_EmissionColor", _infoColor);
            }
        }
    }
}

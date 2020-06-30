using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    bool _engineOn;
    ParticleSystem _parts;
    public Transform _gearA, _gearB, _sideGear;
    public float _mainSpeed;
    // Start is called before the first frame update
    void Start()
    {
        _parts = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_engineOn)
        {
            _gearA.Rotate(Vector3.forward * _mainSpeed * Time.deltaTime);
            _sideGear.Rotate(-Vector3.forward * _mainSpeed * Time.deltaTime);
            _gearB.Rotate(-Vector3.forward * _mainSpeed*2f * Time.deltaTime);
        }
    }

    public void ToggleEngine()
    {
        _engineOn = !_engineOn;
        if (_engineOn)
        {
            _parts.Play();
        }
        else
        {
            _parts.Stop();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public float _minIntensity, _maxIntensity;
    public float _minChange, _maxChange;
    private float _curChange = 0;
    private float _timer = 1f;
    private float _targetIntensity = 0;
    private Light _light;
    public float _lerp;
    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timer > _curChange)
        {
            _targetIntensity = Random.Range(_minIntensity, _maxIntensity);
            _timer = 0;
            _curChange = Random.Range(_minChange, _maxChange);
        }
        _timer += Time.deltaTime;
        _light.intensity = Mathf.Lerp(_light.intensity, _targetIntensity, _lerp * Time.deltaTime);
    }
}

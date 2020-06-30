using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskMonitors : MonoBehaviour
{
    public MeshRenderer _screen1, _screen2;
    public float _intensity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandomizeScreenColors()
    {
        _screen1.materials[1].SetColor("_EmissionColor", Color.HSVToRGB(Random.value, 1, 1) * _intensity);
        _screen2.materials[1].SetColor("_EmissionColor", Color.HSVToRGB(Random.value, 1, 1) * _intensity);
    }
}

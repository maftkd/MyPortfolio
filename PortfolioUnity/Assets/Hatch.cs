using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hatch : MonoBehaviour
{
    public MeshRenderer _attickFloor;
    int _hatchIndex = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Application.Quit();
    }

    private void OnMouseEnter()
    {
        Debug.Log("hey");
        _attickFloor.materials[_hatchIndex].SetColor("_EmissionColor", Color.gray*.2f);
    }

    private void OnMouseExit()
    {
        _attickFloor.materials[_hatchIndex].SetColor("_EmissionColor", Color.black*.2f);
    }
}

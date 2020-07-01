using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    MeshRenderer _mesh;
    // Start is called before the first frame update
    void Start()
    {
        _mesh = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnMouseDown()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().LeaveScene(SceneManager.GetActiveScene().name);
    }

    private void OnMouseEnter()
    {
        _mesh.material.SetColor("_EmissionColor", Color.gray);
        _mesh.materials[1].SetColor("_EmissionColor", Color.gray);
    }

    private void OnMouseExit()
    {
        _mesh.material.SetColor("_EmissionColor", Color.black);
        _mesh.materials[1].SetColor("_EmissionColor", Color.black);
    }
}

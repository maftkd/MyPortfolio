using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    float _restX, _activeX;
    Rigidbody _bod;
    // Start is called before the first frame update
    void Start()
    {
        _restX = transform.position.x;
        _activeX = _restX - .1f;
        _bod = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        Vector3 pos = transform.position;
        pos.x = _activeX;
        transform.position = pos;
        _bod.isKinematic = true;
        _bod.useGravity = false;
    }

    private void OnMouseDown()
    {
        Debug.Log("I have been clicked!");
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().BookClicked();
    }
    private void OnMouseExit()
    {
        Vector3 pos = transform.position;
        pos.x = _restX;
        transform.position = pos;
        _bod.isKinematic = false;
        _bod.useGravity = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseRotator : MonoBehaviour
{
    public Transform _house;
    bool _rotating = false;
    public float _rotTime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateHouse()
    {
        if(!_rotating)
            StartCoroutine(RotateHouseR());
    }

    IEnumerator RotateHouseR()
    {
        _rotating = true;
        Quaternion cur = _house.rotation;
        _house.Rotate(Vector3.up * 90f);
        Quaternion fin = _house.rotation;
        _house.rotation = cur;
        float timer = 0;
        while(timer < _rotTime)
        {
            _house.rotation = Quaternion.Slerp(cur, fin, timer / _rotTime);
            timer += Time.deltaTime;
            yield return null;
        }
        _house.rotation = fin;
        _rotating = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyButton : MonoBehaviour
{
    MeshRenderer _mesh;
    float _intensity = 2f;
    public AnimationCurve _heightCurve;
    float _animDur = .25f;
    bool _clicked = false;
    public Vector3 _downPos;

    public UnityEvent _OnClick;
    // Start is called before the first frame update
    void Start()
    {
        _mesh = GetComponent<MeshRenderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseOver()
    {
        _mesh.material.SetColor("_EmissionColor", Color.red*_intensity);
    }

    private void OnMouseDown()
    {
        if (!_clicked)
            StartCoroutine(ClickButton());
        _OnClick.Invoke();
    }

    private void OnMouseExit()
    {
        _mesh.material.SetColor("_EmissionColor", Color.black);
    }

    private IEnumerator ClickButton()
    {
        _clicked = true;
        float timer = 0;
        Vector3 startPos = transform.localPosition;
        while(timer < _animDur)
        {
            transform.localPosition = Vector3.LerpUnclamped(startPos, _downPos, _heightCurve.Evaluate(timer / _animDur));
            timer += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = startPos;
        _clicked = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashAnimator : MonoBehaviour
{
    public Text _logoText;
    public RawImage _logoImg;
    public CanvasGroup _fader;
    bool _animating;
    public float _fadeDur;
    public float _holdDur;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateLogo()
    {
        if(!_animating)
            StartCoroutine(AnimateLogoR());
    }

    IEnumerator AnimateLogoR()
    {
        _animating = true;
        Color newColor = Color.HSVToRGB(Random.value, 1, 1);
        _logoText.color = newColor;
        _logoImg.color = newColor;
        float timer = 0;
        while(timer < _fadeDur)
        {
            _fader.alpha = 1 - timer / _fadeDur;
            timer += Time.deltaTime;
            yield return null;
        }
        _fader.alpha = 0;
        yield return new WaitForSeconds(_holdDur);
        timer = 0;
        while (timer < _fadeDur)
        {
            _fader.alpha = timer / _fadeDur;
            timer += Time.deltaTime;
            yield return null;
        }
        _fader.alpha = 1;
        _animating = false;
    }
}

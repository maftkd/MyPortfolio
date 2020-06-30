using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinCollect : MonoBehaviour
{
    int score = 0;
    public Text _score;
    Animator _anim;
    bool _canJump = true;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollectCoin()
    {
        if(_canJump)
            StartCoroutine(CollectRoutine());
    }

    IEnumerator CollectRoutine()
    {
        _canJump = false;
        _anim.SetTrigger("jump");
        yield return new WaitForSeconds(.75f);
        score++;
        _score.text = "score: " + score;
        yield return new WaitForSeconds(.1f);
        _canJump = true;

    }
}

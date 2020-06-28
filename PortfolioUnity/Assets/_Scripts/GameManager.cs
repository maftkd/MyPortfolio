using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform _book;
    public Bookshelf[] _shelves;
    int _booksPerShelf = 20;
    Transform _bookshelf;

    [System.Serializable]
    public struct Bookshelf
    {
        public Vector3 _startPos;
        public Vector3 _endPos;
    }
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        _bookshelf = GameObject.Find("Books").transform;

        SpawnBooks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBooks()
    {
        for (int j = 0; j < _shelves.Length; j++)
        {
            for (int i = 0; i < _booksPerShelf; i++)
            {
                Transform book = Instantiate(_book, Vector3.Lerp(_shelves[j]._startPos, _shelves[j]._endPos, (float)i / _booksPerShelf), Quaternion.identity, _bookshelf);
                book.localScale = new Vector3(Random.Range(.2f,.3f), Random.Range(.25f, .4f), Random.Range(.02f, .12f));
                book.Rotate(Random.Range(-30f, 30f), 0, 0);
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach(Bookshelf shelf in _shelves)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(shelf._startPos, .1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(shelf._endPos, .1f);
        }
        
    }
}

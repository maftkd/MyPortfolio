using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool _instanced = false;
    public Transform _book;
    public Bookshelf[] _shelves;
    int _booksPerShelf = 24;
    Transform _bookshelf;

    public enum State { INTRO, ATTIC, BASEMENT, BRIGHTLINE, FREELANCE };
    public State _state;

    public Color _exhibitColor;
    public Color _atticColor;
    public CameraClearFlags _exhibitClearFlags;
    public CameraClearFlags _atticClearFlags;

    public Material[] _bookMats;

    public int _bookSeed;
    bool _basementUnlocked = true;
    bool _brightlineUnlocked = true;
    bool _freelanceUnlocked = true;

    public float _sceneChangeDur;
    public float _sceneChangeDelay;

    [System.Serializable]
    public struct Bookshelf
    {
        public Vector3 _startPos;
        public Vector3 _endPos;
    }
    private void Awake()
    {
        if (!_instanced)
        {
            DontDestroyOnLoad(transform.gameObject);
            _instanced = true;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
#endif
    }

    void OnSceneLoaded(Scene s, LoadSceneMode m)
    {
        Debug.Log("SceneLoaded");
        if (s.buildIndex == 1)
        {
            _bookshelf = GameObject.Find("Books").transform;
            SpawnBooks();
            SetBGColor(_atticColor, _atticClearFlags);
        }
        else
        {
            SetBGColor(_exhibitColor, _exhibitClearFlags);
        }
        Camera.main.transform.GetComponent<FlatCam>().SetClamp(s.buildIndex);
    }

    void SpawnBooks()
    {
        Random.InitState(_bookSeed);
        int firstBook = Random.Range(0, _shelves.Length * _booksPerShelf);
        int secondBook = Random.Range(0, _shelves.Length * _booksPerShelf);
        int thirdBook = Random.Range(0, _shelves.Length * _booksPerShelf);
        for (int j = 0; j < _shelves.Length; j++)
        {
            for (int i = 0; i < _booksPerShelf; i++)
            {
                Transform book = Instantiate(_book, Vector3.Lerp(_shelves[j]._startPos, _shelves[j]._endPos, (float)i / _booksPerShelf), Quaternion.identity, _bookshelf);
                book.localScale = new Vector3(Random.Range(.2f,.3f), Random.Range(.25f, .4f), Random.Range(.02f, .12f));
                book.Rotate(Random.Range(-30f, 30f), 0, 0);
                MeshRenderer mesh = book.GetComponent<MeshRenderer>();
                mesh.material = _bookMats[Random.Range(0, _bookMats.Length)];
                mesh.material.mainTextureScale = new Vector2(1f, Random.Range(1, 4));
                //temp code for highlighting book
                if(j*_booksPerShelf+i == firstBook && _basementUnlocked)
                {
                    mesh.material.SetColor("_EmissionColor", new Color(1f,.75f,.75f));
                    book.tag = "Basement";
                }
                if (j * _booksPerShelf + i == secondBook && _brightlineUnlocked)
                {
                    mesh.material.SetColor("_EmissionColor", new Color(.75f,1f,.75f));
                    book.tag = "Brightline";
                }
                if (j * _booksPerShelf + i == thirdBook && _freelanceUnlocked)
                {
                    mesh.material.SetColor("_EmissionColor", new Color(.75f,.75f,1f));
                    book.tag = "Freelance";
                }
            }
        }
    }

    public void BookClicked(Transform book)
    {
        //SceneManager.LoadScene(tag);
        if (book.tag != "Untagged")
        {
            StartCoroutine(ChangeScene(book));
        }
    }

    IEnumerator ChangeScene(Transform b)
    {
        Transform radiant = GameObject.Find("Radiate").transform;
        CanvasGroup fader = Camera.main.transform.GetChild(0).GetComponentInChildren<CanvasGroup>();
        radiant.position = b.position-Vector3.right*.1f;
        radiant.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(_sceneChangeDelay);
        float timer = 0;
        while(timer < _sceneChangeDur)
        {
            float frac = timer / _sceneChangeDur;
            fader.alpha = frac * frac;
            timer += Time.deltaTime;
            yield return null;
        }
        fader.alpha = 1;
        SceneManager.LoadScene(b.tag);
        timer = 0;
        while (timer < _sceneChangeDur)
        {
            float frac = timer / _sceneChangeDur;
            fader.alpha = 1-frac * frac;
            timer += Time.deltaTime;
            yield return null;
        }
        fader.alpha = 0;
    }



    private void SetBGColor(Color c, CameraClearFlags ccf)
    {
        Camera.main.clearFlags = ccf;
        Camera.main.backgroundColor = c;
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

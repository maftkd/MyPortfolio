using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ServerPing : MonoBehaviour
{
    bool _ping;
    public Text _display;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PingServer()
    {
        if(!_ping)
            StartCoroutine(PingServerR());
    }

    IEnumerator PingServerR()
    {
        _ping = true;
        UnityWebRequest www = UnityWebRequest.Get("http://michaelfeldman.info:3000/ping");
        float t = Time.time;
        yield return www.SendWebRequest();
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            if (www.responseCode == 200)
            {
                float diff = (Time.time - t)*1000;
                _display.text = diff.ToString("#") + "\nms";
            }
        }
        _ping = false;
    }
}

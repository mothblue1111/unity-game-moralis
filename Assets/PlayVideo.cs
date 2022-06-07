using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    [SerializeField] private string name;
    private void Start()
    {
        this.gameObject.GetComponent<VideoPlayer>().url = System.IO.Path.Combine(Application.streamingAssetsPath, name + ".mp4");
        this.gameObject.GetComponent<VideoPlayer>().Play();
    }

    // Start is called before the first frame update
 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Video;

public class StreamingAssets : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] private string streamingAssetsMoviePath;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, streamingAssetsMoviePath);
        videoPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

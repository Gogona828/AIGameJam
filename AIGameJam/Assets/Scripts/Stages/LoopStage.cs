using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

public class LoopStage : MonoBehaviour
{
    [SerializeField, Tooltip("ループする位置")]
    private float loopPosition;
    [SerializeField, Tooltip("進行方向")]
    private bool isLeftGo;

    private Vector2 defaultPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isLeftGo) loopPosition = -loopPosition;
        defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x <= loopPosition && isLeftGo)
        {
            transform.position = defaultPosition;
        }
        else if (transform.localPosition.x >= loopPosition && !isLeftGo) transform.position = defaultPosition;
    }
}

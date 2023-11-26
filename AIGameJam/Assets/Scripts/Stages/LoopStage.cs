using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopStage : MonoBehaviour
{
    [SerializeField, Tooltip("ループする位置")]
    private float loopPosition;
    [SerializeField, Tooltip("進行方向")]
    private bool isLeftGo = false;

    [SerializeField] private bool isBackGround;

    private Vector2 defaultPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        if (isLeftGo && !isBackGround) loopPosition = -loopPosition;
        if (isBackGround) defaultPosition = transform.position + new Vector3(0, -1080, 0);
        else defaultPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isBackGround) LoopBG();
        else LoopLane();
    }

    private void LoopBG()
    {
        if (transform.localPosition.y <= loopPosition) transform.position = defaultPosition;
    }

    private void LoopLane()
    {
        if (transform.localPosition.x <= loopPosition && isLeftGo)
        {
            transform.position = defaultPosition;
        }
        else if (transform.localPosition.x >= loopPosition && !isLeftGo) transform.position = defaultPosition;
    }
}

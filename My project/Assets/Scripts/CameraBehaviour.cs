using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // 1
    public Vector3 camOffset = new Vector3(0f, 1.2f, -2.6f);

    // 2
    private Transform target;

    private void Start()
    {
        // 3
        target = GameObject.Find("Player").transform;
    }

    // 4
    private void LateUpdate()
    {
        // 5
        this.transform.position = target.TransformPoint(camOffset);

        // 6
        this.transform.LookAt(target);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

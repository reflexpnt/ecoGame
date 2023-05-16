using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowAutorotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float xSpeed = 80f;
    private float ySpeed = 0f;
    private float zSpeed = 0f;

    // Update is called once per frame
    void Update()
    {
        
            transform.Rotate(
                 xSpeed * Time.deltaTime,
                 ySpeed * Time.deltaTime,
                 zSpeed * Time.deltaTime
            );
        
    }
}

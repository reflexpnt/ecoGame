using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSnowballToss : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void LoadToss()
    {
        SceneManager.LoadScene("SnowballToss");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

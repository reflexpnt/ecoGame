using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaVolta : MonoBehaviour
{
    public GameObject primaVoltaObj;
    public bool flag=false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(primaVoltaObj);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PrimoPassaggio()
    {
        flag = true;
    }
}

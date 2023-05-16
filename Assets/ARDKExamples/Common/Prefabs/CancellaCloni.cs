using UnityEngine;
using System.Collections;

public class CancellaCloni : MonoBehaviour
{

    public string tagToCheck;
    
    void Start()
    {
        
    }
    public void Update()
    {
        tagToCheck = gameObject.tag;
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tagToCheck);
        for (int c = 1; c<objectsWithTag.Length; c++)
        {
           
                Destroy(objectsWithTag[c]);
            
        }
    }
}

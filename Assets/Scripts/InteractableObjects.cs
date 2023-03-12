using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjects : MonoBehaviour
{
    public GameObject gameObject;
    public List<string> interactionOptions;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InteractableObject(GameObject obj, List<string> options)
    {
        gameObject = obj;
        interactionOptions = options;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class object_movement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Resetting object into the background
        if (transform.position.z > -15)
        {
            transform.position = transform.position + new Vector3(0, 0, -40);
        };
        // Translating the object forwards per frame
        transform.Translate(4 * Vector3.down * Time.deltaTime);
    }
}

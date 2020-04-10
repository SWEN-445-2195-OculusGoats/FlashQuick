using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_movement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 position = transform.position;
        // Space bounding the horizontal movement to keep player on screen
        if (horizontal > 0 && position.x < -7 || horizontal < 0 && position.x > 7)
        {
            return; 
        }
        // Otherwise, update horizontal position
        position.x += horizontal * -3 * Time.deltaTime;
        transform.position = position;
    }
}

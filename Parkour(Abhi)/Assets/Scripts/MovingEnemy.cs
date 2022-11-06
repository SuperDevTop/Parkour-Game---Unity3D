using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnemy : MonoBehaviour
{
    bool movingDirection = true;

    void Update()
    {
        if(this.transform.position.x >= 2f)
        {
            movingDirection = true;
        }
        else if(this.transform.position.x <= -3.5f)
        {
            movingDirection = false;
        }
        
        if(movingDirection)
        {
            this.transform.position = new Vector3(this.transform.position.x - 1f * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x + 1f * Time.deltaTime, this.transform.position.y, this.transform.position.z);
        }
    }
}

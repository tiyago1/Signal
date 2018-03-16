using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ho : MonoBehaviour
{
    public Vector2 aPosition1;
    public bool c;

    void Update()
    {
        if (c)
        {
            transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), aPosition1, 3 * Time.deltaTime);
        }
    }
}

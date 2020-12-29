using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (Bird.k > 0)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorMove : MonoBehaviour
{
    public float moveSpeed;
    private float width;
    private Vector2 oPos;
    // Start is called before the first frame update
    void Start()
    {
        width = GetComponent<RectTransform>().rect.width;
        oPos = GetComponent<RectTransform>().anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        Vector2 newPos = oPos;
        newPos.x -= (Time.time * moveSpeed) % width;
        GetComponent<RectTransform>().anchoredPosition = newPos;
    }
}

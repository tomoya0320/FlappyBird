using UnityEngine;

public class Tree : MonoBehaviour
{
    public float moveSpeed;
    public Main main;
    private Vector2 oPos;
    private float startTime;
    private void Start()
    {
        startTime = Time.time;
        oPos = GetComponent<RectTransform>().anchoredPosition;
        oPos.y = Random.Range(-250, 350);
        GetComponent<RectTransform>().anchoredPosition = oPos;
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
        Vector2 newPos = oPos;
        newPos.x -= (Time.time - startTime) * moveSpeed;     
        if(newPos.x < -1046)
        {
            newPos.x = 754;
            newPos.y = Random.Range(-250, 350);
            oPos = newPos;
            startTime = Time.time;
        }
        GetComponent<RectTransform>().anchoredPosition = newPos;
    }
}

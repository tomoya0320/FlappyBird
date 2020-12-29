using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public Text gText;
    public Transform trees;
    public List<Tree> treeList;
    public List<Bird> birdList;
    private void Start()
    {
        if (gText)
            gText.text = "Generation:" + Bird.k + "  BestFit:" + Bird.gBestFit;
    }
    public void pauseOrResume()
    {
        if (Bird.k > 0)
            Time.timeScale = (Time.timeScale + 1) % 2;
    }
    public void startTrain()
    {
        Time.timeScale = 1;
        Bird.k++;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

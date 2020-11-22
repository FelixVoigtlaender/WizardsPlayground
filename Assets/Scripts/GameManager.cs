using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Cosmonaut spawning")]
    public GameObject cosmonautPrefab;
    public LayerMask cosmonautLayer;
    public int count = 10;
    GameObject[] cosmonauts;
    [Header("UI")]
    public GameObject startUI;
    public GameObject winUI;

    public bool isPlaying = false;

    public static int rounds = 1;


    public void Start()
    {
        Time.timeScale = 0.01f;

        winUI.SetActive(false);
        startUI.SetActive(true);
    }

    public void StartGame()
    {
        startUI.SetActive(false);
        SpawnCosmonauts();
        isPlaying = true;
        Time.timeScale = 1f;
    }
    public void WinGame()
    {
        rounds++;
        for (int i = 0; i < cosmonauts.Length; i++)
        {
            Destroy(cosmonauts[i]);
        }
        winUI.SetActive(true);
    }

    public void ResetLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (!isPlaying)
            return;
        if (Input.GetMouseButtonDown(0))
        {
            WinCheck(Input.mousePosition);
            print("TEST");
        }

        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            WinCheck(Input.GetTouch(0).position);
        }
    }

    public void WinCheck(Vector2 position)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(position);

        Debug.DrawLine(Vector2.zero, worldPosition);

        RaycastHit2D hit =  Physics2D.Raycast(worldPosition, Vector2.down, 0.1f, cosmonautLayer);
        if (hit)
        {
            bool isWin = hit.transform.GetComponent<WinCheck>().isWin;
            if (isWin)
                WinGame();
            else
                print("WRONG");

        }
    }

    public void SpawnCosmonauts()
    {
        count =(int) (rounds - 1 ) * 2 +1 ;

        //count = rounds * 10;

        cosmonauts = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            cosmonauts[i] = Instantiate(cosmonautPrefab);
        }
    }
}

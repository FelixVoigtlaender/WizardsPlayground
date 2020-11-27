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
    Transform[] cosmonauts;
    [Header("UI")]
    public GameObject startUI;
    public GameObject winUI;
    public GameObject selectionUI;
    public GameObject watchUI;


    public Selection selection;

    public bool isPlaying = false;

    public static int rounds = 1;


    public void Start()
    {
        Time.timeScale = 0.01f;

        winUI.SetActive(false);
        startUI.SetActive(true);


        //UI Settings
        startUI.SetActive(true);
        winUI.SetActive(false);
        selectionUI.SetActive(false);
        watchUI.SetActive(false);
    }

    public void StartGame()
    {
        startUI.SetActive(false);
        SpawnCosmonauts();
        isPlaying = true;
        Time.timeScale = 1f;

        Invoke("Selection", cosmonauts.Length * 2);


        //UI Settings
        startUI.SetActive(false);
        winUI.SetActive(false);
        selectionUI.SetActive(false);
        watchUI.SetActive(true);
    }
    public void Selection()
    {
        if (!selection)
            return;

        selection.Setup(cosmonauts);


        //UI Settings
        startUI.SetActive(false);
        winUI.SetActive(false);
        selectionUI.SetActive(true);
        watchUI.SetActive(false);
    }
    public void WinGame()
    {
        rounds++;
        for (int i = 0; i < cosmonauts.Length; i++)
        {
            Destroy(cosmonauts[i].gameObject);
        }

        //UI Settings
        startUI.SetActive(false);
        winUI.SetActive(true);
        selectionUI.SetActive(false);
        watchUI.SetActive(false);
    }

    public void ResetLevel()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (!isPlaying || !selection.isSelection)
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
            bool isWin = hit.collider.transform.GetComponent<Kissable>().isKisser;
            if (isWin)
                WinGame();
            else
                print("WRONG");

            print("CHEEECK");

        }
    }

    public void SpawnCosmonauts()
    {
        count =(int) (rounds) * 2;

        //count = rounds * 10;

        cosmonauts = new Transform[count];
        for (int i = 0; i < count; i++)
        {
            cosmonauts[i] = Instantiate(cosmonautPrefab).transform;
            if(i==0)
                cosmonauts[i].GetComponent<Kissable>().isKisser = true;
        }
    }
}

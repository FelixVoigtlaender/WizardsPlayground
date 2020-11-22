using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class Kissable : MonoBehaviour
{
    public bool isKisser = false;
    public float coolDown = 3f;
    public float kissPercentage = 0.3f;
    public GameObject kiss;
    public LayerMask cosmonautLayer;
    float lastKiss = 0;
    public void Start()
    {
        lastKiss = -coolDown - 1;
        kiss.SetActive(false);
    }

    public void Kiss()
    {
        kiss.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (!isKisser)
            return;

        if (Time.time - lastKiss < coolDown)
            return;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.1f, cosmonautLayer);
        if (hits.Length <= 0)
            return;

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform == transform)
                continue;

            Kissable kissedCosmonaut = hits[i].transform.GetComponent<Kissable>();
            if (kissedCosmonaut.kiss.activeSelf)
                continue;
            kissedCosmonaut.Kiss();
            lastKiss = Time.time;
            return;
        }
    }
}

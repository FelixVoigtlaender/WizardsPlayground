using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Selection : MonoBehaviour
{
    public float spacing = 1f;
    public Transform[] selectables = new Transform[0];

    float dragVelocity = 0;
    public float minDragVelocity = 0.2f;
    [Range(0,1f)]
    public float dragVelocityDamper = 0.1f;

    bool isDraged = false;

    float dragStartX;


    public bool isSelection = false;

    public static event Action OnSelection;

    private void Awake()
    {
        OnSelection = null;
    }
    private void Start()
    {
        DragManager.OnDragBegan += OnDragBegan;
        DragManager.OnDrag += OnDrag;
        DragManager.OnDragEnded += OnDragEnded;
    }

    public void Setup(Transform[] selectables)
    {

        this.selectables = selectables;

        Vector2 startPosition = transform.position;
        for (int i = 0; i < selectables.Length; i++)
        {
            Vector2 position = startPosition + Vector2.right * spacing * i;
            selectables[i].position = position;
            selectables[i].SetParent(transform);
        }

        transform.position = Vector2.right * (selectables.Length-1)/2;

        OnSelection?.Invoke();
        isSelection = true;
    }

    private void Update()
    {
        if (isDraged)
            return;

        Vector2 position = transform.position;

        if(dragVelocity > minDragVelocity)
        {
            dragVelocity *= (1 - dragVelocityDamper);
            position += Vector2.right * dragVelocity * Time.deltaTime;
        }
        else
        {
            float targetX = Mathf.Round(position.x / spacing) * spacing;
            position.x = Mathf.SmoothDamp(position.x, targetX, ref dragVelocity, 0.5f);
        }
        position.x = Mathf.Clamp(position.x, -selectables.Length * spacing, 0);

        transform.position = position;
    }
    public void OnDragBegan(DragManager.Drag drag)
    {
        dragStartX = drag.GetStart().x;
        isDraged = true;
    }
    public void OnDrag(DragManager.Drag drag)
    {
        float delta = drag.GetEnd().x - dragStartX;
        Vector2 position = transform.position;
        position.x += delta;
        dragStartX += delta;
        dragVelocity = + delta / Time.deltaTime;

        position.x = Mathf.Clamp(position.x, -(selectables.Length-1) * spacing, 0);

        transform.position = position;


        isDraged = true;
    }
    public void OnDragEnded(DragManager.Drag drag)
    {
        isDraged = false;
    }
}

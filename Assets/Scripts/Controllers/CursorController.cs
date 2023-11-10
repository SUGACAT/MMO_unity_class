using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Texture2D _attackIcon;
    private Texture2D _handIcon;
    
    private int _mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    enum CursorType
    {
        None,
        Attack,
        Hand
    }

    private CursorType _cursorType = CursorType.None;

    private void Awake()
    {
        _attackIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Attack");
        _handIcon = Managers.Resource.Load<Texture2D>("Textures/Cursor/Hand");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
            return;
    
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, _mask))
        {
            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                if (_cursorType == CursorType.Attack) return;
                Cursor.SetCursor(_attackIcon, new Vector2(_attackIcon.width / 5, 0), CursorMode.Auto);
            }
            else
            {
                if (_cursorType == CursorType.Hand) return;
                Cursor.SetCursor(_handIcon, new Vector2(_handIcon.width / 3, 0), CursorMode.Auto);
            }
        }
    }
}

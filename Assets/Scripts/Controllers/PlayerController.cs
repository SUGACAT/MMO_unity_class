using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    private PlayerStat _stat;

    public float _speed = 3.0f;

    private Vector3 _destPos;
    
    private GameObject _lockTarget;
    private int mask = (1 << (int)Define.Layer.Ground) | (1 << (int)Define.Layer.Monster);

    private PlayerState _state = PlayerState.Idle;

    public AudioClip bgmClip;
    
    void Start()
    {
        _stat = GetComponent<PlayerStat>();
        Managers.Input.MouseAction += OnMouseEvent;
        Managers.Sound.Play(bgmClip, Define.Sound.Bgm);
    }

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        Skill
    }

    private void Update()
    {
        switch (_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
            case PlayerState.Skill:
                UpdateSkill();
                break;
        }
    }

    private void UpdateSkill()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetBool("attack", true);
    }

    private void OnHitEvent()
    {
        Debug.Log("OnHitEvent");

        Animator anim = GetComponent<Animator>();
        anim.SetBool("attack", false);

        _state = PlayerState.Idle;
    }
    
    private void UpdateMouseCursor()
    {
    }

    private void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", 0);
    }

    private void UpdateMoving()
    {
        if (_lockTarget != null)
        {
            float distance = (_destPos - transform.position).magnitude;
            if (distance <= 1)
            {
                _state = PlayerState.Skill;
                return;
            }
        }

        NavMeshAgent nav = gameObject.GetOrAddComponent<NavMeshAgent>();


        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.1f)
        {
            _state = PlayerState.Idle;
            return;
        }

        float moveDist = Math.Clamp(_stat.MoveSpeed * Time.deltaTime, 0, dir.magnitude);

        nav.Move(dir.normalized * moveDist);

        Debug.DrawRay(transform.position + Vector3.up * 0.5f, dir.normalized, Color.green);

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir, 1.0f, LayerMask.GetMask("Block")))
        {
            _state = PlayerState.Idle;
            nav.velocity = Vector3.zero;
            return;
        }

        if (dir.magnitude > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir),
                10 * Time.deltaTime);
        }
        
        //애니메이션
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("speed", _stat.MoveSpeed);
    }

    private void UpdateDie()
    {
        Debug.Log("Player die!!!!!");
    }

    private void OnMouseEvent(Define.MouseEvent obj)
    {
        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100, Color.red, 1.0f);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;

            if (hit.collider.gameObject.layer == (int)Define.Layer.Monster)
            {
                Debug.Log("Monster Click");
            }
            else
            {
                Debug.Log("Ground Click");
            }
        }
    }
}
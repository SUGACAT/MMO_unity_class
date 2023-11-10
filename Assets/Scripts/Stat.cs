using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] protected int _level;
    [SerializeField] protected int _hp;
    [SerializeField] protected int _maxHp;
    
    [SerializeField] protected int _attack;
    [SerializeField] protected int _defense;

    [SerializeField] protected float _moveSpeed;

    public int Level
    {
        get => _level;
        set => _level = value;
    }
    
    public int Hp
    {
        get => _hp;
        set => _hp = value;
    }
    
    public int MaxHp
    {
        get => _maxHp;
        set => _maxHp = value;
    }
    
    public int Attack
    {
        get => _attack;
        set => _attack = value;
    }
    
    public int Defence
    {
        get => _defense;
        set => _defense = value;
    }
    
    public float MoveSpeed
    {
        get => _moveSpeed;
        set => _moveSpeed = value;
    }
    
}

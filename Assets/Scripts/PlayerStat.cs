using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStat : Stat
{
    [SerializeField] protected int _exp;
    [SerializeField] protected int _gold;

    public int Exp
    {
        get => _exp;
        set => _exp = value;
    }

    private void Start()
    {
        _level = 1;
        _hp = 100;
        _maxHp = 100;
        _attack = 10;
        _defense = 5;
        _moveSpeed = 5;
        _exp = 0;
        _gold = 0;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{

    #region Stat
    [Serializable]
    public class Stat : MonoBehaviour
    {
        public int level;
        public int hp;
        public int attack;
    }

    [Serializable]
    public class StatData : ILoader<int, Stat>
    {
        public List<Stat> Stats = new List<Stat>();

        public Dictionary<int, Stat> MakeDict()
        {
            Dictionary<int, Stat> dict = new Dictionary<int, Stat>();
            foreach (Stat stat in Stats)
            {
                dict.Add(stat.level, stat);
            }

            return dict;
        }
    }
    #endregion
}

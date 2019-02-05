using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.AI.AIDataAccess
{
    [System.Serializable]
    public struct AnimalPosition
    {
        public float x { get; set; }
        public float y { get; set; }

        public AnimalPosition(float x, float y)
        {
            this.x = (float)System.Math.Round(x, 2);
            this.y = (float)System.Math.Round(y, 2); ;
        }

        public static implicit operator AnimalPosition(Vector3 v3)
        {
            return new AnimalPosition(v3.x, v3.y);
        }

        public static implicit operator Vector3(AnimalPosition pos)
        {
            return new Vector3(pos.x, pos.y, 0f);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.AI.AIDataAccess
{
    [System.Serializable]
    public class AIMemoryElement
    {
        private Dictionary<string, AnimalPosition> AnimalPositions { get; set; }

        public AIMemoryElement()
        {
            AnimalPositions = new Dictionary<string, AnimalPosition>();
        }

        public void Add(string animalName, Vector3 animalPosition)
        {
            AnimalPositions[animalName] = animalPosition;
        }

        public void Add(string animalName, float x, float y)
        {
            AnimalPositions[animalName] = new AnimalPosition(x, y);
        }

        public Vector3 this[string animalName]
        {
            get { return AnimalPositions[animalName]; }
            private set { AnimalPositions[animalName] = value; }
        }
    }
}

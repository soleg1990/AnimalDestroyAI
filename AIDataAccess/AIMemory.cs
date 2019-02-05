using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.AI.AIDataAccess
{
    public class AIMemory
    {
        private Dictionary<float, AIMemoryElement> memory;

        public AIMemory()
        {
            memory = new Dictionary<float, AIMemoryElement>();
        }

        public void AddAnimalPosition(float distance, string animalName, Vector3 animalPosition)
        {
            GetMemoryElement(distance).Add(animalName, animalPosition);
        }

        public void AddAnimalPosition(float distance, string animalName, float x, float y)
        {
            GetMemoryElement(distance).Add(animalName, x, y);
        }

        private AIMemoryElement GetMemoryElement(float distance)
        {
            distance = (float)System.Math.Round(distance, 1);
            AIMemoryElement memoryElement;
            if (!memory.TryGetValue(distance, out memoryElement))
            {
                memoryElement = new AIMemoryElement();
                memory.Add(distance, memoryElement);
            }
            return memoryElement;
        }

        public Vector3 GetAnimalPosition(float distance, string animalName)
        {
            return memory[distance][animalName];
        }

        public void SaveMemoryToFile(string fileName)
        {
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var fs = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, memory);
            }
        }

        public void LoadMemoryFromFile(string fileName)
        {
            var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (var fs = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate))
            {
                memory = (Dictionary<float, AIMemoryElement>)formatter.Deserialize(fs);
            }
        }
    }
}

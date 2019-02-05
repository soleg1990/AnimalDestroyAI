using Assets.AI.AIDataAccess;
using Assets.Scripts.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.AI.AIPlayer
{
    public class AIPlayerAttack : PlayerAttackBase
    {
        [SerializeField] GameObject player;

        private Dictionary<string, Action> animalsCreators;
        private Dictionary<string, Action> AnimalsCreators
        {
            get
            {
                if (animalsCreators == null)
                {
                    animalsCreators = new Dictionary<string, Action> {
                        { "Cow", () => CreateCow() },
                        { "Pig", () => CreatePig() },
                        { "Sheep", () => CreateSheep() }
                    };
                }
                return animalsCreators;
            }
        }

        private float distance;
        private float Distance
        {
            get
            {
                if (distance < 1)
                {
                    distance = (float)Math.Round(transform.position.x - player.transform.position.x, 1);
                }
                return distance;
            }
                    
        }

        private AIMemory memory;
        private Vector3 animalPosition;
        private bool canDropAnimal;

        private void Awake()
        {
            memory = new AIMemory();
            memory.LoadMemoryFromFile("emr");
        }

        protected override void OnTakeTurn()
        {
            canDropAnimal = false;
            //TODO когда будут посчитаны все животные, сделать рандом с проверкой на null, а лучше из memory дергать коллекцию из всех животных по этой дистанции
            // так можно не хардкодить имя животного
            animalPosition = memory.GetAnimalPosition(Distance, "Cow");
            AnimalsCreators["Cow"].Invoke(); //вызов Action
        }

        protected override void OnAnimalCreated()
        {
            base.OnAnimalCreated();
            animal.transform.position = new Vector3(transform.position.x, 10f, 0f);
        }

        //AI получает ход
        //из набора животных берем рандомное животное и криэйтит его
        //далее лерпит его до x и y из мемори
        //далее сброс
        protected override bool CanDropAnimal()
        {
            return canDropAnimal;
        }

        protected override Vector3 GetAnimalPosition()
        {
            if (animal.transform.position == animalPosition)
            {
                canDropAnimal = true;
                return animalPosition;
            }

            //TODO ты скопипастил этот код из CameraOnStart - можно и в Utils общий код
            var journeyLength = Vector3.Distance(animal.transform.position, animalPosition);
            float distCovered = Time.deltaTime * 2.5f;
            float fracJourney = distCovered / journeyLength;

            return Vector3.Lerp(animal.transform.position, animalPosition, fracJourney);
        }
    }
}

using Assets.AI.AIDataAccess;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculationController : MonoBehaviour {

    [SerializeField] GameObject player2;
    [SerializeField] Projectile projectilePrefab;

    private float x, y;
    private float enemyPosX = -7.6f;
    private CalculationPlayerAttack compAttack;
    private Projectile projectile;
    private AIMemory memory;
    private bool isMemorySave;

    // Use this for initialization
    void Start () {
        SetPlayer2StartPosition();
        x = player2.transform.position.x - 0.1f;
        y = 2f;
        projectile = Instantiate(projectilePrefab);
        projectile.gameObject.SetActive(false);
        compAttack = player2.GetComponent<CalculationPlayerAttack>();
        compAttack.TakeTurn(projectile);
        compAttack.AnimalPosition = new Vector3(x, y, 0);
        compAttack.CreateCow();
        compAttack.CanShoot = true;

        memory = new AIMemory();
        Time.timeScale = 100f;
    }
	
	// Update is called once per frame
	void Update () {
        if (isMemorySave) return;

        if (projectile.HasFinished)
        {
            if (projectile.HasHitTheEnemy)
            {
                memory.AddAnimalPosition(player2.transform.position.x - enemyPosX, "Cow", x, y);
                OffsetToRightOrSaveMemory();
            }
            projectile.HasFinished = false;
            compAttack.CanShoot = false;
            //x -= .1f;
            if (player2.transform.position.x - x <= 1.15)
            {
                x -= 0.05f;
            }
            else if (y <= 10f)
            {
                x = player2.transform.position.x - 0.1f;
                y += 0.05f;
            }
            else 
            {
                OffsetToRightOrSaveMemory();
            }

            foreach(var rg in player2.GetComponentsInChildren<Rigidbody>())
            {
                rg.velocity = new Vector3(0f, 0f, 0f);
            }
            compAttack.TakeTurn(projectile);
            compAttack.AnimalPosition = new Vector3(x, y, 0f);
            compAttack.CreateCow();
            compAttack.CanShoot = true;
        }
		//скидываем снаряд N с высоты ( от 2 до 10 возраст) и ширины (1.4 смещение в минус)
        //проверяем, что попали в игрока, если да, то записываем координаты сброса для этого животного
        //иначе
        //если можно прибавляем ширину, затем высоту, затем след животное
	}

    private void OffsetToRightOrSaveMemory()
    {
        if (player2.transform.position.x <= enemyPosX + 35f)
        {
            var tmp = player2.transform.position;
            tmp.x += 0.1f;
            player2.transform.position = tmp;
            x = player2.transform.position.x - 0.1f;
            y = 2f;
        }
        else
        {
            memory.SaveMemoryToFile("emr");
            isMemorySave = true;
        }
    }

    private void SetPlayer2StartPosition()
    {
        var temp = player2.transform.position;
        temp.x = 5f;
        player2.transform.position = temp;
    }
}

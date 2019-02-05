using Assets.Scripts.Player;
using UnityEngine;

public class CalculationPlayerAttack : PlayerAttackBase
{
    public bool CanShoot { get; set; }
    public Vector3 AnimalPosition { get; set; }

    protected override bool CanDropAnimal()
    {
        return CanShoot;
    }

    protected override Vector3 GetAnimalPosition()
    {
        return AnimalPosition;
    }
}

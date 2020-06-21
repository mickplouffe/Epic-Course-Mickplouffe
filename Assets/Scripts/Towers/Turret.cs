using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] int _warFundCost = 100;

    public int GetWarFundCost()
    {
        return _warFundCost;
    }

}
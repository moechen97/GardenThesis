using System.Collections;
using System.Collections.Generic;
using Planting;
using UnityEngine;

public class ChangePlantLifetime : MonoBehaviour
{
    [SerializeField] private UILifeButton[] _lifeButtons;
    public void Changeto05Speed()
    {
        PlantManager.UpdatePlantsLifeSpeed(0.5f);
        for (int x = 0; x < _lifeButtons.Length; x++)
        {
            if (x == 0)
            {
                _lifeButtons[x].ActiveButton();
            }
            else
            {
                _lifeButtons[x].InactiveButton();
            }
        }
    }
    
    public void Changeto1Speed()
    {
        PlantManager.UpdatePlantsLifeSpeed(1f);
        for (int x = 0; x < _lifeButtons.Length; x++)
        {
            if (x == 1)
            {
                _lifeButtons[x].ActiveButton();
            }
            else
            {
                _lifeButtons[x].InactiveButton();
            }
        }
    }
    
    public void Changeto2Speed()
    {
        PlantManager.UpdatePlantsLifeSpeed(2f);
        for (int x = 0; x < _lifeButtons.Length; x++)
        {
            if (x == 2)
            {
                _lifeButtons[x].ActiveButton();
            }
            else
            {
                _lifeButtons[x].InactiveButton();
            }
        }
    }
    
    public void Changeto5Speed()
    {
        PlantManager.UpdatePlantsLifeSpeed(5f);
        for (int x = 0; x < _lifeButtons.Length; x++)
        {
            if (x == 3)
            {
                _lifeButtons[x].ActiveButton();
            }
            else
            {
                _lifeButtons[x].InactiveButton();
            }
        }
    }
}

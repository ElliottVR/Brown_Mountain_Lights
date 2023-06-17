using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSurvival : MonoBehaviour
{
    public bool updateUI;
    
    public bool decreaseHunger;
    public bool decreaseWater;
    /*
    [Header("Player Variables")]
    [Range(0, 100)]
    public int health;
    [Range(0, 1)]
    public float food;
    [Range(0, 1)]
    public float water;
    */
    [Header("Rates")]
    public float hungerRate;
    public float thirstRate;

    [Header("Sliders")]
    public Slider healthSlide;
    public Slider hungerSlide;
    public Slider waterSlide;

    void Update()
    {
        if (updateUI == true)
        {
            UpdateUI();
        }

        if (decreaseHunger == true && GameVariables.Food > 0)
        {
            DecreaseFood();
        }
        if (decreaseWater == true && GameVariables.Water > 0)
        {
            DecreaseWater();
        }
        
        if (GameVariables.Food < 0)
        {
            GameVariables.Food = 0;
        }
        if (GameVariables.Water < 0)
        {
            GameVariables.Water = 0;
        }
        

        if (GameVariables.Food > 100)
        {
            GameVariables.Food = 100;
        }
        if (GameVariables.Water > 100)
        {
            GameVariables.Water = 100;
        }
        if (GameVariables.Health > 100)
        {
            GameVariables.Health = 100;
        }


    }

    public void DecreaseFood()
    {
        GameVariables.Food -= hungerRate / GameVariables.timeScale * Time.deltaTime;
    }

    public void DecreaseWater()
    {
        GameVariables.Water -= thirstRate / GameVariables.timeScale * Time.deltaTime;
    }

    public void UpdateUI()
    {
        healthSlide.value = GameVariables.Health;
        hungerSlide.value = GameVariables.Food;
        waterSlide.value = GameVariables.Water;
    }
}

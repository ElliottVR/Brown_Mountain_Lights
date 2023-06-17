using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VREating : MonoBehaviour
{
    public float maxEatTime;
    public float currentEatTime;

    private void OnTriggerStay(Collider other)
    {
        
        if (other.gameObject.tag == "Eatable")
        {
            
            if (other.gameObject.GetComponent<HandOffset>().item != null)
            {

                if (other.gameObject.GetComponent<HandOffset>().item.editorType == Item.Edible.Consumable)
                {

                    currentEatTime += 1 * Time.deltaTime;
                    if (currentEatTime >= maxEatTime)
                    {
                        EatFood(other.gameObject, other.gameObject.GetComponent<HandOffset>().item);
                    }
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        currentEatTime = 0;
    }

    public void EatFood(GameObject obj, Item food)
    {
        GameVariables.Food += food.nutritionalValue;
        Destroy(obj);
        
    }
}

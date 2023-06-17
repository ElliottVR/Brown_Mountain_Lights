using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class RoomGen : MonoBehaviour
{
    public Transform[] itemLocations; // Locations of where items can spawn.
    
    public List<Item> possibleItems; // Items that are allowed to spawn in that area.
    public List<Item> weightedItems; // Calculated weighted chace on what item spawns.

    public List<GameObject> itemsInArea; // Items spawned.

    private void Start()
    {
        GenerateRoom(); // Generate room when entering building.
    }

    public void GenerateRoom()
    {
        foreach (var locs in itemLocations) // Repeats for every item spawn location.
        {
            int i = GetRandomItem(); // Picks random item.
            GameObject tempObj = Instantiate(weightedItems[i].itemPrefab, locs.transform.position, locs.transform.rotation); // Spawn item at that position.
            tempObj.transform.parent = locs.transform; // Set item as child.
            itemsInArea.Add(tempObj);
        }
    }

    public int GetRandomItem()
    {   
        foreach(Item item in possibleItems) // Repeats for every possible item.
        {
            AddToList(item); // Adds items to the weighted list based on weight.
        }
        
        int output = Random.Range(0, weightedItems.Count); // Gets random number from weighted list count.
        return output;
    }
    
    // Higher weight items will have more elements in the weighted list, having a higher chance to be chosen.
    public void AddToList(Item item)
    {
        for (int i = 0; i < item.randChance / 2; i++)
        {
            weightedItems.Add(item);
        }
    }
}

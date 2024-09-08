using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// World provides a single, global access point the states via WorldStates

/// <summary> 
/// A singleton that manages the world states.
/// This class is sealed to prevent inheritence and other classes modifying it
/// </summary>
public sealed class World
{
    // Singleton instance of the world class. It's readonly so it cannot be modified
    private static readonly World instance = new World();
    private static WorldStates world;
    private static Queue<GameObject> kitchen;
    private static Queue<GameObject> bathroom;
    private static Queue<GameObject> bunkroom;

    // Constructor to initialise world states. It's static so world states are initialised before any instance of the world is used
    static World()
    {
        world = new WorldStates();
    }

    // Enforce singleton pattern
    private World()
    {

    }

    public void AddKitchen(GameObject k)
    {
        kitchen.Enqueue(k);
    }

    public void AddBathroom(GameObject b)
    {
        bathroom.Enqueue(b);
    }

    public void AddBunkroom(GameObject b)
    {
        bunkroom.Enqueue(b);
    }

    // Property to get the instance of the world
    public static World Instance
    {
        get { return instance; }
    }

    // Get the global worldStates instance
    public WorldStates GetWorld()
    {
        return world;
    }
}

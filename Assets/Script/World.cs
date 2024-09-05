using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// A singleton that manages the world states.
/// This class is sealed to prevent inheritence and other classes modifying it
/// </summary>
public sealed class World
{
    private static readonly World instance = new World();
    private static WorldStates world;

    // Constructor to initialise world states. It's static so world states are initialised before any instance of the world is used
    static World()
    {
        world = new WorldStates();
    }

    // Enforce singleton pattern
    private World()
    {

    }

    // Property to get the instance of the world
    public static World Instance
    {
        get { return Instance; }
    }

    // Get the global worldStates instance
    public WorldStates GetWorld()
    {
        return world;
    }
}

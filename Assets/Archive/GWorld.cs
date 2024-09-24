// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// /// <summary> 
// /// Singlton class that provides global access to GWordStates, which manages the world state data
// /// </summary>
// public class GWorld
// {
//     private static GWorld instance;
//     private static GWorldStates worldStates;

//     // Lazy loading of the GWorld singleton
//     public static GWorld Instance
//     {
//         get
//         {
//             if (instance == null)
//             {
//                 instance = new GWorld();
//                 InitWorldStates();
//             }
//             return instance;
//         }
//     }

    // // Initilises GWorldStates by adding it to a new game object
    // private static void InitWorldStates()
    // {
    //     GameObject worldObject = new GameObject("GWorldStatesObject");
    //     worldStates = worldObject.AddComponent<GWorldStates>();
    // }

//     // Returns the GWorldStates instance as a global accessor for other classes
//     public GWorldStates GetWorldStates()
//     {
//         return worldStates;
//     }

// }

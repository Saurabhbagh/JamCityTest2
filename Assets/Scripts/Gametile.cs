using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameTile", menuName = "GameTileYo")]
public class Gametile : ScriptableObject
{
    public string actualObject;
    public new string name;
    public Texture Image;
    public int facevalue;

    // made dynamically
    public bool Reachablity;
}

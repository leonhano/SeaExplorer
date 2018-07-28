using UnityEngine;
using System.Collections;

//define

public struct TagDef
{
    public const string Terrain = "Terrain";
    public const string Water = "Water";
    public const string Port = "Port"; //port tag;
    public const string NPC = "NPC";  //NPC tag;
    public const string Player = "Player";
    public const string MainCamera = "MainCamera";
}

public struct LayerDef
{
    public const string Terrain = "Terrain";
    public const string Water = "Water";
}

public struct Command
{
    public const string InteractiveWithPlayer = "InteractiveWithPlayer";
    public const string FinishInteractiveWithPlayer = "FinishInteractiveWithPlayer";
}
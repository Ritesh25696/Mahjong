using UnityEngine;

[CreateAssetMenu(fileName = "New Level", menuName = "Create Level")]
public class LevelData : ScriptableObject
{
    //Scriptable Object to load various levels.
    public int row;
    public int col;
    public string levelLayout;
}
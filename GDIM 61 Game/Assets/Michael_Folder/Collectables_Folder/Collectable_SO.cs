using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Insert Name", menuName = "New Collectable")]
public class Collectable_SO : ScriptableObject
{
    public string collectableName;
    public string description;
    public string hint;
    public Sprite image;
    public Sprite hintImage;

    public int worldNumber;
    public int levelNumber;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battery Item")]
public class BatteryItem : ScriptableObject
{
    public string batteryName;
    public int powerLevel;
    public Sprite icon;
}

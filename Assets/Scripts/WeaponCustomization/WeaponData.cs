using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon Customization/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    [Header("Weapon Info")]
    public string Name;
    public string Id;
    public WeaponRarityType Rarity;
    public int Level;
    public int Power;

    [Space(15)]
    public List<WeaponBaseStat> WeaponStats;

    [Space(15)]
    public List<AttachmentID> WeaponAttachments;

}

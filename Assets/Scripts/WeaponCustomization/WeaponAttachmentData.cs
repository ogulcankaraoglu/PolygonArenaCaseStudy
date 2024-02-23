using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "Weapon Customization/Weapon Attachment Data", order = 1)]
public class WeaponAttachmentData : ScriptableObject
{
    public string AttachmentName;
    public string AttachmentID;
    public WeaponAttachmentType AttachmentType;
    public Sprite AttachmentSprite;
    public Color AttachmentBackgroundColor;
    public Color AttachmentFrameColor;
}

public enum WeaponAttachmentType
{
    SIGHT,
    MAG,
    BARREL,
    STOCK,
    TACTICAL
}

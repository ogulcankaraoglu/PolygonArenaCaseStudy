
public enum WeaponRarityType
{
    COMMON,
    RARE,
    EPIC,
    LEGENDARY
}

[System.Serializable]
public struct WeaponBaseStat
{
    public WeaponStatType Type;
    public float Value;
}

public enum WeaponStatType
{
    Damage,
    FireRate,
    Accuracy,
    ClipSize,
    ReloadSpeed,
    Range
}

[System.Serializable]
public struct AttachmentID
{
    public WeaponAttachmentType Type;
    public string ID;
}
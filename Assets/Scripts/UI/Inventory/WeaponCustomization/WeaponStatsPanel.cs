using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;

public class WeaponStatsPanel : MonoBehaviour
{
    public const float StarDivideFactor = 80f;

    [Header("TEXT COMPONENTS")]
    public TMP_Text WeaponNameText;
    public TMP_Text LevelText;
    public TMP_Text PowerText;

    [Header("STATS COMPONENTS")]
    public StatElement StatElementPrefab;
    public Transform StatsContentParent;
    public List<StatIcon> StatIcons;

    [Header("RARITY COMPONENTS")]
    public TMP_Text RarityText;
    public Image RarityImage;
    public List<Color> RarityColors;

    [Header("STAR VIEW COMPONENT")]
    public StarView _StarView;

    public void SetWeaponStatTexts(WeaponData _weaponData)
    {
        WeaponNameText.text = _weaponData.Name;
        LevelText.text = _weaponData.Level.ToString();
        PowerText.text = _weaponData.Power.ToString();
        _StarView.SetStarView(_weaponData.Power / StarDivideFactor);
        SetWeaponRarity(_weaponData.Rarity);
        SetStatsScrollView(_weaponData);
    }

    public void SetWeaponRarity(WeaponRarityType _rarity)
    {
        RarityText.text = _rarity.ToString();
        RarityImage.color = RarityColors[(int)_rarity];
    }

    public void SetStatsScrollView(WeaponData _weaponData)
    {
        for (int i = 0; i < _weaponData.WeaponStats.Count; i++)
        {
            StatElement _statElement = Instantiate(StatElementPrefab);
            _statElement.transform.SetParent(StatsContentParent);
            StatIcon _statIcon = GetStatIcon(_weaponData.WeaponStats[i].Type);
            _statElement.StatIcon.sprite = _statIcon.sprite;
            _statElement.StatName.text = _statIcon.Name;
            _statElement.StatValueText.text = _weaponData.WeaponStats[i].Value.ToString().Replace(",", ".");
            _statElement.gameObject.SetActive(true);
        }
    }

    // Didn't wanted to use linq query for this one, instead gone with simple for-loop.
    private StatIcon GetStatIcon(WeaponStatType _weaponStatType)
    {
        StatIcon _stat = new StatIcon();
        for (int i = 0; i < StatIcons.Count; i++)
        {
            if (StatIcons[i].Type == _weaponStatType)
            {
                _stat = StatIcons[i];
            }
        }
        return _stat;
    }

}

[System.Serializable]
public struct StatIcon
{
    public string Name;
    public WeaponStatType Type;
    public Sprite sprite;
}



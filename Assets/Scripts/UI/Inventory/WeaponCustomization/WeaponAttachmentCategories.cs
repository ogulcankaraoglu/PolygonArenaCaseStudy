using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttachmentCategories : MonoBehaviour
{
    public WeaponSelectableCategory SelectableCategoryPrefab;
    private List<WeaponSelectableCategory> CurrentCategories = new List<WeaponSelectableCategory>();

    public void InitAttachmentCategories(List<WeaponAttachmentGroup> _group)
    {
        CurrentCategories.Clear();
        for (int i = 0; i < _group.Count; i++)
        {
            WeaponSelectableCategory _category = Instantiate(SelectableCategoryPrefab);
            CurrentCategories.Add(_category);
            WeaponAttachmentData _data = _group[i].WeaponAttachments[0].AttachmentData;
            _category.AttachmentImage.sprite = GetAttachmentSprite(_group[i].AttachmentType);
            _category.AttachmentText.text = _group[i].AttachmentType.ToString();
            _category.AttachmentType = _group[i].AttachmentType;
            _category.transform.SetParent(transform);
            _category.gameObject.SetActive(true);
        }
        if (CurrentCategories.Count > 0)
        {
            CurrentCategories[0].SelectionFrame.SetActive(true);
            CurrentCategories[0].InitCategory();
        }
    }

    public void UpdateCategorySprite(WeaponAttachmentType _type)
    {
        CurrentCategories[(int)_type].AttachmentImage.sprite = GetAttachmentSprite(_type);
    }

    public Sprite GetAttachmentSprite(WeaponAttachmentType _type)
    {
        AttachmentDataKey _dataKey =  WeaponCustomization.Instance.CurrentWeaponManager.AttachmentDataKeys[(int)_type];
        WeaponAttachmentGroup _group = WeaponCustomization.Instance.CurrentWeaponManager.WeaponAttachments[(int)_type];
        for (int i = 0; i < _group.WeaponAttachments.Count; i++)
        {
            if (_group.WeaponAttachments[i].AttachmentData.AttachmentID == _dataKey.ID)
            {
                return _group.WeaponAttachments[i].AttachmentData.AttachmentSprite;
            }
        }
        return _group.WeaponAttachments[0].AttachmentData.AttachmentSprite;
    }

    public void UpdateSelectedCategory(WeaponAttachmentType _type)
    {
        for (int i = 0; i < CurrentCategories.Count; i++)
        {
            if (CurrentCategories[i].AttachmentType != _type)
            {
                CurrentCategories[i].SelectionFrame.SetActive(false);
            }
            else
            {
                CurrentCategories[i].SelectionFrame.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelectableAttachments : MonoBehaviour
{
    public const int SelectableAttachmentGenerateCount = 10;
    public SelectableAttachment SelectableAttachmentPrefab;
    private List<SelectableAttachment> CurrentAttachments = new List<SelectableAttachment>();

    private void Awake()
    {
        for (int i = 0; i < SelectableAttachmentGenerateCount; i++)
        {
            SelectableAttachment _attachment = Instantiate(SelectableAttachmentPrefab);
            _attachment.transform.SetParent(transform);
            CurrentAttachments.Add(_attachment);
        }
    }

    public void InitAttachmentButtons(WeaponAttachmentType _type, WeaponAttachmentManager _manager)
    {

        for (int i = 0; i < CurrentAttachments.Count; i++)
        {
            CurrentAttachments[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < _manager.WeaponAttachments[(int)_type].WeaponAttachments.Count; i++)
        {
            SelectableAttachment _attachment = CurrentAttachments[i];
            WeaponAttachmentData _data = _manager.WeaponAttachments[(int)_type].WeaponAttachments[i].AttachmentData;
            _attachment.SelectionFrame.SetActive(false);
            _attachment.AttachmentImage.sprite = _data.AttachmentSprite;
            _attachment.AttachmentText.text = _data.AttachmentName;
            _attachment.FrameImage.color = _data.AttachmentFrameColor;
            _attachment.BackgroundImage.color = _data.AttachmentBackgroundColor;
            _attachment.CurrentAttachmentData = _data;
            _attachment.gameObject.SetActive(true);
        }

        AttachmentDataKey _dataKey = WeaponCustomization.Instance.CurrentWeaponManager.AttachmentDataKeys[(int)_type];
        LastSavedSelectedAttachment(_dataKey.Type, _dataKey.ID);
    }

    public void LastSavedSelectedAttachment(WeaponAttachmentType _type, string _id)
    {
        AttachmentDataKey _dataKey = WeaponCustomization.Instance.CurrentWeaponManager.AttachmentDataKeys[(int)_type];
        for (int i = 0; i < CurrentAttachments.Count; i++)
        {
            if (CurrentAttachments[i].CurrentAttachmentData == null)
            {
                break;
            }

            CurrentAttachments[i].SelectionFrame.SetActive(false);
            if (CurrentAttachments[i].CurrentAttachmentData.AttachmentID == _dataKey.ID)
            {
                CurrentAttachments[i].SelectionFrame.SetActive(true);
                WeaponCustomization.Instance.SetEquipButtonState(WeaponCustomization.Instance.CurrentWeaponManager.IsAttachmentEquipped(_dataKey.Type, _dataKey.ID));
            }
        }

    }

    public void UpdateSelectedAttachment(WeaponAttachmentType _type, string _id)
    {
        AttachmentDataKey _dataKey = WeaponCustomization.Instance.CurrentWeaponManager.AttachmentDataKeys[(int)_type];
        WeaponCustomization.Instance.SetEquipButtonState(WeaponCustomization.Instance.CurrentWeaponManager.IsAttachmentEquipped(_dataKey.Type, _id));
        
        for (int i = 0; i < CurrentAttachments.Count; i++)
        {
            if (CurrentAttachments[i].CurrentAttachmentData == null)
            {
                break;
            }

            CurrentAttachments[i].SelectionFrame.SetActive(false);
            if (CurrentAttachments[i].CurrentAttachmentData.AttachmentID == _id)
            {
                CurrentAttachments[i].SelectionFrame.SetActive(true);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttachmentManager : MonoBehaviour
{
    public WeaponData _WeaponData;
    public List<AttachmentDataKey> AttachmentDataKeys;
    public List<WeaponAttachmentGroup> WeaponAttachments;

    private void Start()
    {
        LoadAttachmentData();
        WeaponCustomization.Instance.CurrentWeaponManager = this;
        WeaponCustomization.Instance.SetCurrentWeaponStats();
        WeaponCustomization.Instance.AttachmentCategories.InitAttachmentCategories(WeaponAttachments);
    }

    private void LoadAttachmentData()
    {
        for (int i = 0; i < AttachmentDataKeys.Count; i++)
        {
            if (PlayerPrefs.HasKey(AttachmentDataKeys[i].Key))
            {
                string currentID = PlayerPrefs.GetString(AttachmentDataKeys[i].Key);
                AttachmentDataKeys[i].ID = currentID;
            }
            else
            {
                PlayerPrefs.SetString(AttachmentDataKeys[i].Key, _WeaponData.WeaponAttachments[i].ID);
                AttachmentDataKeys[i].ID = _WeaponData.WeaponAttachments[i].ID;
            }
        }
        SetInitialWeaponVisual();
    }

    public void SaveAttachmentData(WeaponAttachmentType _type, string _id)
    {
        PlayerPrefs.SetString(AttachmentDataKeys[(int)_type].Key, _id);
        LoadAttachmentData();
    }

    private void SetInitialWeaponVisual()
    {
        for (int i = 0; i < AttachmentDataKeys.Count; i++)
        {
            List<WeaponAttachment> currentAttachments = WeaponAttachments[(int)AttachmentDataKeys[i].Type].WeaponAttachments;
            for (int a = 0; a < currentAttachments.Count; a++)
            {
                if (currentAttachments[a].AttachmentData.AttachmentID != AttachmentDataKeys[i].ID)
                {
                    currentAttachments[a].AttachmentReference.SetActive(false);
                }
                else
                {
                    currentAttachments[a].AttachmentReference.SetActive(true);
                }
            }
        }
    }

    public void SetAttachmentVisual(WeaponAttachmentType _type, string _id)
    {
        for (int i = 0; i < WeaponAttachments[(int)_type].WeaponAttachments.Count; i++)
        {
            WeaponAttachment _currentAttachment =  WeaponAttachments[(int)_type].WeaponAttachments[i];
            if (_currentAttachment.AttachmentData.AttachmentID != _id)
            {
                _currentAttachment.AttachmentReference.SetActive(false);
            }
            else
            {
                _currentAttachment.AttachmentReference.SetActive(true);
                WeaponCustomization.Instance.CameraManager.SetAttachmentView(_currentAttachment.AttachmentViewPoint);
            }
           
        }
    }

    public bool IsAttachmentEquipped(WeaponAttachmentType _type, string _id)
    {
        if (AttachmentDataKeys[(int)_type].Type == _type && AttachmentDataKeys[(int)_type].ID == _id) return false;
        else return true;
    }

}

[System.Serializable]
public class WeaponAttachmentGroup
{
    public WeaponAttachmentType AttachmentType;
    public List<WeaponAttachment> WeaponAttachments;
}

[System.Serializable]
public class WeaponAttachment
{
    public WeaponAttachmentData AttachmentData;
    public GameObject AttachmentReference;
    public Transform AttachmentViewPoint;
}

[System.Serializable]
public class AttachmentDataKey
{
    public WeaponAttachmentType Type;
    public string Key;
    public string ID;
}



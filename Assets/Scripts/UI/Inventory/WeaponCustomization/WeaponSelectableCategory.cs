using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponSelectableCategory : MonoBehaviour
{
    [HideInInspector]
    public WeaponAttachmentData CurrentAttachmentData;
    [HideInInspector]
    public WeaponAttachmentType AttachmentType;
    public Image AttachmentImage;
    public TMP_Text AttachmentText;
    public GameObject SelectionFrame;
    public Button _Button;

    private void Start()
    {
        _Button.onClick.AddListener(InitCategory);
    }

    public void InitCategory()
    {
        WeaponCustomization.Instance.AttachmentCategories.UpdateSelectedCategory(AttachmentType);
        WeaponCustomization.Instance.SelectableAttachments.InitAttachmentButtons(AttachmentType,WeaponCustomization.Instance.CurrentWeaponManager);
    }
}

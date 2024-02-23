using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarView : MonoBehaviour
{
    public List<Image> StarImages;
    public Sprite FullStarSprite;
    public Sprite HalfStarSprite;

    public void SetStarView(float _starCount)
    {
        ClearStars();
        int _fullStar = Mathf.Clamp((int)_starCount,1,5);
        float _diff = _starCount - _fullStar;
        if (_diff > 0) SetStars(_fullStar, true);
        else SetStars(_fullStar);
    }

    private void ClearStars()
    {
        for (int i = 0; i < StarImages.Count; i++)
        {
            StarImages[i].enabled = false;
        }
    }

    private void SetStars(int _count, bool _isHalf = false)
    {
        for (int i = 0; i < _count; i++)
        {
            StarImages[i].sprite = FullStarSprite;
            StarImages[i].enabled = true;
            if (_isHalf && i == _count - 1 && _count < 5)
            {
                StarImages[i + 1].sprite = HalfStarSprite;
                StarImages[i + 1].enabled = true;
            }
        }
    }


}

using UnityEngine;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private List<Animator> _buttonList;

    [SerializeField]
    private string _appearAnimationName = "UIobjectAppear";


    [SerializeField]
    private string _disappearAnimationName = "UIobjectDisappear";

    public void ShowButtons()
    {
        foreach (Animator button in _buttonList)
        {
            button.Play(_appearAnimationName);
        }
    }

    public void HideButtons()
    {
        foreach (Animator button in _buttonList)
        {
            button.Play(_disappearAnimationName);
        }
    }

}

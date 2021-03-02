using UnityEngine;

public class LDPluginDemoMenuButtonBehavior : MonoBehaviour
{
    public GameObject thisUiElement;
    public GameObject otherUiElement;
    public bool isOtherUiElementDisabledOnAwake = true;

    void Awake()
    {
        if(isOtherUiElementDisabledOnAwake)
        {
            otherUiElement.SetActive(false);
        }
        if(!thisUiElement)
        {
            thisUiElement = this.gameObject;
        }
    }

    public void OnClick()
    {
        if(otherUiElement)
        {
            otherUiElement.SetActive(true);
            thisUiElement.SetActive(false);
        }
    }
}

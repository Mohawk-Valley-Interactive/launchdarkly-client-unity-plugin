﻿using LaunchDarkly.Client;
using LaunchDarkly.Unity;
using UnityEngine;

public class LDPluginDemoOptionsButtonBehavior : MonoBehaviour
{
    public string showOptionsFeatureFlagName = "show-options-menu";
    public GameObject optionsButton;
    public bool showOptionsFeatureFlagDefault = false;

    private void Awake()
    {
        if(optionsButton == null)
        {
            Debug.LogWarning("LDPluginDemoOptionsButtonBehavior.Awake() optionsButton not assigned.");
            return;
        }

        isVisible = optionsButton.activeSelf;
    }

    void Start()
    {
        LaunchDarkly.Client.LdValue flagValueDefault = LaunchDarkly.Client.LdValue.Of(showOptionsFeatureFlagDefault);
        LaunchDarklyClientBehavior.Instance.RegisterFeatureFlagChangedCallback(showOptionsFeatureFlagName, flagValueDefault, flagValueCallback, true);  
    }

    private void Update()
    {
        if(isVisible != optionsButton.activeSelf)
        {
            optionsButton.SetActive(isVisible);
        }
    }

    private bool isVisible;

    private void flagValueCallback(LdValue obj)
    {
        isVisible = obj.AsBool;
    }
}

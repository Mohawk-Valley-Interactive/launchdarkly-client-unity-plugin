using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class LDPluginDemoPlayerClassDropdownBehavior : MonoBehaviour
{

	public void Awake()
	{
		dropDown = GetComponent<Dropdown>();

		playerAttributesBehavior = null;
		if (LDPluginDemoGameAttributesBehavior.Instance != null)
		{
			playerAttributesBehavior = LDPluginDemoGameAttributesBehavior.Instance.GetComponent<LDPluginDemoPlayerAttributesBehavior>();
		}
		else
		{
			LDPluginDemoPlayerAttributesBehavior[] playerAttributesList = FindObjectsOfType<LDPluginDemoPlayerAttributesBehavior>();
			if (playerAttributesList.Length == 0)
			{
				Debug.LogError("PlayerClassToggleButtonBehavior.Awake() No player attribute list objects found in scene. Attribute will be default in dropDown.");
				return;
			}
			else if (playerAttributesList.Length > 1)
			{
				Debug.LogWarning("PlayerClassToggleButtonBehavior.Awake() More than one player attribute list object found in scene. Defaulting to first in list.");
			}

			playerAttributesBehavior = playerAttributesList[0];

		}

		dropDown.ClearOptions();
		List<Dropdown.OptionData> optionsList = new List<Dropdown.OptionData>(4);
		optionsList.Add(new Dropdown.OptionData(LDPluginDemoPlayerAttributesBehavior.ClassType.Barbarian.ToString()));
		optionsList.Add(new Dropdown.OptionData(LDPluginDemoPlayerAttributesBehavior.ClassType.Monk.ToString()));
		optionsList.Add(new Dropdown.OptionData(LDPluginDemoPlayerAttributesBehavior.ClassType.Wizard.ToString()));
		optionsList.Add(new Dropdown.OptionData(LDPluginDemoPlayerAttributesBehavior.ClassType.Unset.ToString()));
		dropDown.AddOptions(optionsList);
		switch (playerAttributesBehavior.classType)
		{
			case LDPluginDemoPlayerAttributesBehavior.ClassType.Barbarian:
				dropDown.value = 0;
				break;
			case LDPluginDemoPlayerAttributesBehavior.ClassType.Monk:
				dropDown.value = 1;
				break;
			case LDPluginDemoPlayerAttributesBehavior.ClassType.Wizard:
				dropDown.value = 2;
				break;
			case LDPluginDemoPlayerAttributesBehavior.ClassType.Unset:
			default:
				dropDown.value = 3;
				break;
		}

		dropDown.onValueChanged.AddListener(onValueChanged);
	}

	private Dropdown dropDown;
	private LDPluginDemoPlayerAttributesBehavior playerAttributesBehavior = null;

	private void onValueChanged(int optionsIndex)
	{
		if (playerAttributesBehavior != null)
		{
			switch (optionsIndex)
			{
				case 0:
					playerAttributesBehavior.ChangeAttribute(LDPluginDemoPlayerAttributesBehavior.ClassType.Barbarian);
					break;
				case 1:
					playerAttributesBehavior.ChangeAttribute(LDPluginDemoPlayerAttributesBehavior.ClassType.Monk);
					break;
				case 2:
					playerAttributesBehavior.ChangeAttribute(LDPluginDemoPlayerAttributesBehavior.ClassType.Wizard);
					break;
				case 3:
				default:
					playerAttributesBehavior.ChangeAttribute(LDPluginDemoPlayerAttributesBehavior.ClassType.Unset);
					break;
			}
		}
	}
}



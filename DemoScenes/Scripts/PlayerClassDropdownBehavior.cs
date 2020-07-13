using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
public class PlayerClassDropdownBehavior : MonoBehaviour
{

	public void Awake()
	{
		dropDown = GetComponent<Dropdown>();

		playerAttributesBehavior = null;
		if (GameAttributesBehavior.Instance != null)
		{
			playerAttributesBehavior = GameAttributesBehavior.Instance.GetComponent<PlayerAttributesBehavior>();
		}
		else
		{
			PlayerAttributesBehavior[] playerAttributesList = FindObjectsOfType<PlayerAttributesBehavior>();
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
		optionsList.Add(new Dropdown.OptionData(PlayerAttributesBehavior.ClassType.Barbarian.ToString()));
		optionsList.Add(new Dropdown.OptionData(PlayerAttributesBehavior.ClassType.Monk.ToString()));
		optionsList.Add(new Dropdown.OptionData(PlayerAttributesBehavior.ClassType.Wizard.ToString()));
		optionsList.Add(new Dropdown.OptionData(PlayerAttributesBehavior.ClassType.Unset.ToString()));
		dropDown.AddOptions(optionsList);
		switch (playerAttributesBehavior.classType)
		{
			case PlayerAttributesBehavior.ClassType.Barbarian:
				dropDown.value = 0;
				break;
			case PlayerAttributesBehavior.ClassType.Monk:
				dropDown.value = 1;
				break;
			case PlayerAttributesBehavior.ClassType.Wizard:
				dropDown.value = 2;
				break;
			case PlayerAttributesBehavior.ClassType.Unset:
			default:
				dropDown.value = 3;
				break;
		}

		dropDown.onValueChanged.AddListener(onValueChanged);
	}

	private Dropdown dropDown;
	private PlayerAttributesBehavior playerAttributesBehavior = null;

	private void onValueChanged(int optionsIndex)
	{
		if (playerAttributesBehavior != null)
		{
			switch (optionsIndex)
			{
				case 0:
					playerAttributesBehavior.ChangeAttribute(PlayerAttributesBehavior.ClassType.Barbarian);
					break;
				case 1:
					playerAttributesBehavior.ChangeAttribute(PlayerAttributesBehavior.ClassType.Monk);
					break;
				case 2:
					playerAttributesBehavior.ChangeAttribute(PlayerAttributesBehavior.ClassType.Wizard);
					break;
				case 3:
				default:
					playerAttributesBehavior.ChangeAttribute(PlayerAttributesBehavior.ClassType.Unset);
					break;
			}
		}
	}
}



using System;
using LaunchDarkly.Client;
using LaunchDarkly.Unity;
using UnityEngine;

public class PlayerAttributesBehavior : IUserAttributeProviderBehavior
{
    public ClassType classType;

    [Serializable]
    public enum ClassType
    {
        Barbarian,
        Monk,
        Wizard,
        Unset
    }

    public override void InjectAttributes(ref IUserBuilder userBuilder)
    {
        if(classType != ClassType.Unset)
		{
            userBuilder.Custom("class-type", classType.ToString());
		} 
        else
		{
            userBuilder.Custom("class-type", "");
		}
    }

    public void ChangeAttribute(ClassType ct)
    {
        if(classType != ct)
        {
            classType = ct;
            if(ClientBehavior.IsInitialized)
            {
                Debug.Log("Changing user state.");
                ClientBehavior.Instance.UpdateUser(this);
            }
        } 
    }

}

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
        Ninja,
        Wizard,
        Unset
    }

    public override void InjectAttributes(ref IUserBuilder userBuilder)
    {
        userBuilder.Custom("class-type", classType.ToString());
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

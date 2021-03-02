using LaunchDarkly.Client;
using LaunchDarkly.Unity;
using UnityEngine.SceneManagement;

public class LDPluginDemoCurrentLevelAttributesBehavior : ILaunchDarklyUserAttributeProviderBehavior
{
    public string levelName = "unset";
    public override void InjectAttributes(ref IUserBuilder userBuilder)
    {
        userBuilder.Custom("level-id", SceneManager.GetActiveScene().buildIndex);
        userBuilder.Custom("level-name", levelName);
    }
}

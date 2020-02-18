using LaunchDarkly.Client;
using UnityEngine;

namespace LaunchDarkly.Unity
{
	public class BuiltInUserAttributeProviderBehavior : IUserAttributeProviderBehavior
	{
		[SerializeField]
		public StringAttribute email = null;
		public StringAttribute fullName = null;
		public StringAttribute firstName = null;
		public StringAttribute lastName = null;
		public StringAttribute country = null;
		public StringAttribute ipAddress = null;
		public StringAttribute avatar = null;

		public override void InjectAttributes(ref IUserBuilder userBuilder)
		{
			if (email.isSet)
			{
				if (email.isPrivate)
				{
					userBuilder.Email(email.value).AsPrivateAttribute();
				}
				else
				{
					userBuilder.Email(email.value);
				}
			}
			if (fullName.isSet)
			{
				if (fullName.isPrivate)
				{
					userBuilder.Name(fullName.value).AsPrivateAttribute();
				}
				else
				{
					userBuilder.Name(fullName.value);
				}
			}
			if (firstName.isSet)
			{
				if (firstName.isPrivate)
				{
					userBuilder.FirstName(firstName.value).AsPrivateAttribute();
				}
				else
				{
					userBuilder.FirstName(firstName.value);
				}
			}
			if (lastName.isSet)
			{
				if (lastName.isPrivate)
				{
					userBuilder.LastName(lastName.value).AsPrivateAttribute();
				}
				else
				{
					userBuilder.LastName(lastName.value);
				}
			}
			if (country.isSet)
			{
				if (country.isPrivate)
				{
					userBuilder.Country(country.value).AsPrivateAttribute();
				}
				else
				{
					userBuilder.Country(country.value);
				}
			}
			if (ipAddress.isSet)
			{
				if (ipAddress.isPrivate)
				{
					userBuilder.IPAddress(ipAddress.value).AsPrivateAttribute();
				}
				else
				{
					userBuilder.IPAddress(ipAddress.value);
				}
			}
			if (avatar.isSet)
			{
				if (avatar.isPrivate)
				{
					userBuilder.Avatar(avatar.value).AsPrivateAttribute();
				}
				else
				{
					userBuilder.Avatar(avatar.value);
				}
			}
		}
	}
}

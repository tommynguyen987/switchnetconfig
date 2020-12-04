using System;
using System.Xml.Serialization;
using System.Collections;
namespace SwitchNetConfig
{
	/// <summary>
	/// Store a profile information.
	/// </summary>
	[XmlInclude( typeof( NICProfile ) )]
	public class Profile
	{
		#region variables

		// Name of the profile
		public string Name = string.Empty;

		// Collection of network card profile
		[XmlArrayAttribute("NICProfiles")]
		public ArrayList NICProfiles = new ArrayList();

		public IEProfile IEProfile = new IEProfile(false, string.Empty, false, string.Empty );

		#endregion

		#region Constructors

		public Profile() {}
		public Profile( string name )
		{
			Name = name;
		}

		#endregion
	}
}

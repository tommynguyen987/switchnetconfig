using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace SwitchNetConfig
{
	/// <summary>
	/// Helper class for persisting configuration
	/// </summary>
	public class ConfigurationHelper
	{
		#region Variables

		private const string PROFILE_FILE_NAME = "Profiles.xml";

		#endregion

		#region Public Static 

		/// <summary>
		/// Loads the collection of profiles from configuration file
		/// </summary>
		/// <returns></returns>
		public static ArrayList LoadConfig()
		{
			if( File.Exists( PROFILE_FILE_NAME ) )
			{
				// Use XML Deserializer to load the serialized collection
				XmlSerializer serializer = new XmlSerializer( typeof( ConfigWrapper ) );
				// read the profile file
				StreamReader reader = new StreamReader( PROFILE_FILE_NAME );
				// deserialize the profile collection from the file
				ConfigWrapper wrapper = (ConfigWrapper) serializer.Deserialize( reader.BaseStream );
				// close the file handle
				reader.Close();
				// return the profile collection
				return wrapper.Profiles;
			}
			else
				return new ArrayList();

		}
		/// <summary>
		/// Saves the profiles in the configuration file.
		/// </summary>
		/// <param name="profiles"></param>
		public static void SaveConfig( ArrayList profiles )
		{
			// Use XML Serializer to serialize the content of the specified array list
			XmlSerializer serializer = new XmlSerializer( typeof( ConfigWrapper ) );
			// open the profile file
			StreamWriter writer = new StreamWriter( PROFILE_FILE_NAME, false );				
			try
			{
				ConfigWrapper wrapper = new ConfigWrapper();
				wrapper.Profiles = profiles;
				
				// Serialize the array list to the file
				serializer.Serialize( writer.BaseStream, wrapper );
			}
			catch( Exception x )
			{
				System.Diagnostics.Debug.WriteLine( x.Message );
			}
			// close the file
			writer.Close();
		}

		#endregion
	}

	#region ConfigWrapper

	/// <summary>
	/// Configuration wrapper class
	/// </summary>
	[XmlInclude( typeof( Profile ) )]
	public class ConfigWrapper
	{
		[XmlArrayAttribute("Profiles")]
		public ArrayList Profiles;
	}

	#endregion

}


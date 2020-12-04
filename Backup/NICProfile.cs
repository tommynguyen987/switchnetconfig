using System;

namespace SwitchNetConfig
{
	/// <summary>
	/// Stores network card configuration for a profile
	/// </summary>
	public class NICProfile
	{
		#region variables

		public string Name = string.Empty;

		public string IP = string.Empty;
		public string Subnet = string.Empty;
		public string Gateway = string.Empty;
		public string DNS = string.Empty;
		public bool UseDHCP = false;

		#endregion

		#region Constructors

		public NICProfile() {}

		public NICProfile( string name )
		{
			Name = name;
		}

		#endregion

		#region public methods

		public void IsValid()
		{
			// check IPs
			string [] IPs = IP.Split(',');
			if( 0 == IPs.Length )
				throw new Exception( "No IP specified" );

			// validate IP
			foreach( string ip in IPs )
				isValidIP( ip );

			// validate Subnet
			isValidIP( Subnet );

			// validate Gateway
			isValidIP( Gateway );

			// validate DNS
			string [] Dnses = DNS.Split(',');
			if( Dnses.Length > 0 )
			{
				foreach( string dns in Dnses )
					isValidIP( dns );
			}
		}

		private bool isValidIP( string IP )
		{
			string [] segments = IP.Split('.');

			if( segments.Length != 4 )
				throw new Exception( "Invalid IP format. Valid format is XXX.XXX.XXX.XXX" );

			foreach( string segment in segments )
			{
				try
				{
					byte value = byte.Parse( segment );
					return true;
				}
				catch
				{
					throw new Exception( "Only values from 0 to 255 allowed" );
				}
			}

			return false;
		}

		#endregion
	}
}

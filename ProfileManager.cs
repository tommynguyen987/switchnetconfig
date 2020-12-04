using System;

namespace SwitchNetConfig
{
	public delegate void StatusUpdate( string message );
		
	/// <summary>
	/// Applies profile
	/// </summary>
	public class ProfileManager
	{
		#region Variables

		private Profile _Profile;

		public event StatusUpdate OnStatusUpdate;

		#endregion

		#region Constructors
		
		public ProfileManager( Profile profile )
		{
			_Profile = profile;
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Call this to apply the specified profile for the specified NIC
		/// </summary>
		/// <param name="nicName"></param>
		public void Run( string nicName )
		{
			applyNIC( nicName );
			applyIEProfile( _Profile.IEProfile );
		}

		/// <summary>
		/// Apply on all NICs
		/// </summary>
		public void Run( )
		{
			foreach( NICProfile nic in _Profile.NICProfiles )
				Run( nic.Name );
		}

		#endregion

		#region Private methods

		/// <summary>
		/// Applies configuration for the specified NIC
		/// </summary>
		/// <param name="nicName"></param>
		private void applyNIC( string nicName )
		{
			NICProfile nicProfile = getNICProfile( nicName );

			if( null == nicProfile ) return;

			UpdateStatus( "Setting configuration for: " + nicName );

			try
			{
				if( nicProfile.UseDHCP )
					WMIHelper.SetDHCP( nicName );
				else
					WMIHelper.SetIP( nicProfile.Name, nicProfile.IP, nicProfile.Subnet, nicProfile.Gateway, nicProfile.DNS );
			}
			catch( Exception x )
			{
				UpdateStatus( "Error occured while setting network configuration." );
				UpdateStatus( x.Message );
				return;
			}

			UpdateStatus( "Done." );
		}

		/// <summary>
		/// Returns the profile for the specified NIC
		/// </summary>
		/// <param name="nicName"></param>
		/// <returns></returns>
		private NICProfile getNICProfile( string nicName )
		{
			foreach( NICProfile profile in _Profile.NICProfiles )
				if( profile.Name.Equals( nicName ) )
					return profile;

			return null;
		}

		/// <summary>
		/// Set Internet Explorer Proxt setting
		/// </summary>
		/// <param name="ieProfile"></param>
		private void applyIEProfile( IEProfile ieProfile )
		{
			UpdateStatus( "Setting Internet Explorer Proxy Setting..." );

			IEProxy.ProxyEnabled = ieProfile.UseProxy;
			IEProxy.ProxyServer = ieProfile.ProxyName;
			IEProxy.BypassProxyForLocal = ieProfile.BypassLocal;

			UpdateStatus( "Done." );
		}

		/// <summary>
		/// Raises event to update status
		/// </summary>
		/// <param name="message"></param>
		private void UpdateStatus( string message )
		{
			if( null != OnStatusUpdate )
				OnStatusUpdate( message + Environment.NewLine );
		}
		
		#endregion
	}
}

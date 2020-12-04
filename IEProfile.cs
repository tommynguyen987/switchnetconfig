using System;

namespace SwitchNetConfig
{
	/// <summary>
	/// Proxy setting for Internet Explorer
	/// </summary>
	public class IEProfile
	{
		#region Variables

		public bool UseProxy = false;
		public string ProxyName = string.Empty;
		public bool BypassLocal = false;
		public string BypassAddresses = string.Empty;

		#endregion

		#region Constructors

		public IEProfile() {}

		public IEProfile( bool useProxy, string proxyName, bool bypassLocal, string bypassAddresses )
		{
			UseProxy = useProxy;
			ProxyName = proxyName;
			BypassLocal = bypassLocal;
			BypassAddresses = bypassAddresses;
		}

		#endregion
	}
}

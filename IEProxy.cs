using System;
using Microsoft.Win32;

namespace SwitchNetConfig
{
	/// <summary>
	/// Summary description for IEProxy.
	/// </summary>
	public class IEProxy
	{
		#region Variables

		private static readonly RegistryKey currentUser = Registry.CurrentUser;		
		private static RegistryKey _InternetSettings;

		#endregion

		#region Public static methods
		/// <summary>
		/// Open the key where Internet Explorer store's its proxy setting
		/// </summary>
		private static void OpenInternetSettings()
		{
			_InternetSettings = currentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings", true);
		}
		
		/// <summary>
		/// Proxy Server name. It can be IP, IP:PORT or a HTTP URL with PORT
		/// </summary>
		public static string ProxyServer
		{
			get
			{
				OpenInternetSettings();
				
				string value = (string)_InternetSettings.GetValue( "ProxyServer", string.Empty );
				_InternetSettings.Close();

				return value;
			}
			set
			{
				OpenInternetSettings();
				
				_InternetSettings.SetValue( "ProxyServer", value );	
				_InternetSettings.Close();
			}
		}

		/// <summary>
		/// True means proxy setting is applied, otherwise proxy is ignored
		/// </summary>
		public static bool ProxyEnabled
		{
			get
			{
				OpenInternetSettings();
				
				int value = (int)_InternetSettings.GetValue( "ProxyEnable", 0 );
				_InternetSettings.Close();

				return (value > 0);
			}
			set
			{
				OpenInternetSettings();
		
				int setValue = ( value ? 1 : 0 );
				_InternetSettings.SetValue( "ProxyEnable", setValue );
				_InternetSettings.Close();
			}
		}

		public static bool BypassProxyForLocal
		{
			get
			{
				OpenInternetSettings();

				string value = (string) _InternetSettings.GetValue( "ProxyOverride", string.Empty );
				_InternetSettings.Close();

				// If bypass proxy set, then it should contain <local>
				if( value.IndexOf("<local>") >= 0 )
					return true;
				else
					return false;

				
			}
			set
			{
				OpenInternetSettings();

				string existingValue = (string) _InternetSettings.GetValue( "ProxyOverride", string.Empty );
				if( existingValue.IndexOf("<local>") >= 0 )
				{
					if( !value )
						existingValue = existingValue.Replace( ";" + Environment.NewLine + "<local>", "" );

				}
				else
				{
					// does not contain the local keyword. Add it.
					if( value )
						existingValue += ";" + Environment.NewLine + "<local>";
				}

				_InternetSettings.SetValue( "ProxyOverride", existingValue );
				_InternetSettings.Close();
			}
		}

		#endregion
	}
}

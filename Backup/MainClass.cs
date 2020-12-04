using System;
using System.Windows.Forms;
namespace SwitchNetConfig
{
	/// <summary>
	/// Entry point
	/// </summary>
	public class MainClass
	{
		[STAThreadAttribute]
		public static void Main()
		{
			Application.Run( new MainForm() );
		}
	}
}

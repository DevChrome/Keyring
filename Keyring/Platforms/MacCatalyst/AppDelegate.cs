using Foundation;

namespace Keyring;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp () => MauiProgram.CreateMauiApp();
}

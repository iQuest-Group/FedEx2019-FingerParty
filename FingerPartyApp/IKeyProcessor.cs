#region Using Directives

using System.Windows.Input;

#endregion

namespace FingerPartyApp
{
	public interface IKeyProcessor
	{
		#region Public Methods

		void SetupHost(IKeyProcessorHost host);

		void InjectKey(Key key);

		#endregion
	}
}
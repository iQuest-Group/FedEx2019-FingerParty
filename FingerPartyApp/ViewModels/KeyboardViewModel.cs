#region Using Directives

using System.Windows.Input;

#endregion

namespace FingerPartyApp.ViewModels
{
	public sealed class KeyboardViewModel : IKeyProcessor
	{
		#region Public Methods

		public void SetupHost(IKeyProcessorHost host)
		{
			this.parent = host;
		}

		public void InjectKey(Key key)
		{
			this.wordProcessor.InjectKey(key);
			if ("start".Equals(this.wordProcessor.Next()))
			{
				this.parent.PushNextFrame(new KeyboardViewModel());
			}
		}

		#endregion

		#region Constants and Fields

		private readonly WordProcessor wordProcessor = new WordProcessor();

		private IKeyProcessorHost parent;

		#endregion
	}
}
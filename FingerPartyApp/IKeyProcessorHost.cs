namespace FingerPartyApp
{
	public interface IKeyProcessorHost
	{
		#region Public Methods

		void PushNextFrame(IKeyProcessor newProcessor);

		#endregion
	}
}
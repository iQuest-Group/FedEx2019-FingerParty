#region Using Directives

using System.ComponentModel;
using System.Windows.Input;

#endregion

namespace FingerPartyApp
{
	public sealed class KeyChangeWrapper : INotifyPropertyChanged
	{
		#region Constructors

		public KeyChangeWrapper(Key key)
		{
			Key = key;
		}

		#endregion

		#region Public Properties

		public Key Key { get; }

		public bool IsHighlighted
		{
			get
			{
				return this.isHighlighted;
			}
			set
			{
				this.isHighlighted = value;
				OnPropertyChanged(nameof(IsHighlighted));
			}
		}

		#endregion

		#region Public Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Public Methods

		public void SendHighlightSignal()
		{
			IsHighlighted = true;
			IsHighlighted = false;
		}

		#endregion

		#region Private Methods

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		#region Constants and Fields

		private bool isHighlighted;

		#endregion
	}
}
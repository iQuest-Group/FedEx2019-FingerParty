#region Using Directives

using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace FingerPartyApp
{
	public sealed class ColoredKeyWrapper : INotifyPropertyChanged
	{
		#region Constructors

		public ColoredKeyWrapper(Key key)
		{
			Key = key;
			Color = Brushes.Wheat;
		}

		#endregion

		#region Public Properties

		public Key Key { get; }

		public Brush Color
		{
			get
			{
				return this.color;
			}
			set
			{
				this.color = value;
				OnPropertyChanged(nameof(Color));
			}
		}

		#endregion

		#region Public Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Private Methods

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		#region Constants and Fields

		private Brush color;

		#endregion
	}
}
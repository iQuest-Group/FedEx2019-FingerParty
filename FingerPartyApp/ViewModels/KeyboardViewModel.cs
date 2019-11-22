#region Using Directives

using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace FingerPartyApp.ViewModels
{
	public sealed class KeyboardViewModel : IKeyProcessor, INotifyPropertyChanged
	{
		#region Constructors

		public KeyboardViewModel()
		{
			BackgroundColor = Brushes.Black;
		}

		#endregion

		#region Public Properties

		public SolidColorBrush BackgroundColor
		{
			get
			{
				return this.background;
			}
			set
			{
				this.background = value;
				OnPropertyChanged(nameof(BackgroundColor));
			}
		}

		public IEnumerable<Key> FirstRow
			=> new[] { Key.D1, Key.D2, Key.D3, Key.D4, Key.D5, Key.D6, Key.D7, Key.D8, Key.D9, Key.D0 };

		public IEnumerable<Key> SecondRow => new[] { Key.Q, Key.W, Key.E, Key.R, Key.T, Key.Y, Key.U, Key.I, Key.O, Key.P };

		public IEnumerable<Key> HomeRow => new[] { Key.A, Key.S, Key.D, Key.F, Key.G, Key.H, Key.J, Key.K, Key.L };

		public IEnumerable<Key> FourthRow => new[] { Key.Z, Key.X, Key.C, Key.V, Key.B, Key.N, Key.M };

		#endregion

		#region Public Events

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		#region Public Methods

		public void SetupHost(IKeyProcessorHost host)
		{
			this.parent = host;
		}

		public void InjectKey(Key key)
		{
			this.wordProcessor.InjectKey(key);
			string word = this.wordProcessor.Next();

			if (this.brushConverterCache.IsValid(word))
			{
				BackgroundColor = this.brushConverterCache.ConvertFromString(word) as SolidColorBrush;
			}
		}

		#endregion

		#region Private Methods

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		#region Constants and Fields

		private readonly TypeConverter brushConverterCache = TypeDescriptor.GetConverter(typeof(SolidColorBrush));

		private readonly WordProcessor wordProcessor = new WordProcessor();

		private SolidColorBrush background;

		private IKeyProcessorHost parent;

		#endregion
	}
}
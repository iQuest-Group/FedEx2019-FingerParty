#region Using Directives

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
			this.leftWord = new TrackedWord("hello");
			this.rightWord = new TrackedWord("world");
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

		public IEnumerable<KeyChangeWrapper> FirstRow => this.firstRow;

		public IEnumerable<KeyChangeWrapper> SecondRow => this.secondRow;

		public IEnumerable<KeyChangeWrapper> HomeRow => this.homeRow;

		public IEnumerable<KeyChangeWrapper> FourthRow => this.fourthRow;

		public IEnumerable<ColoredKeyWrapper> LeftWord => this.leftWord.Keys;

		public IEnumerable<ColoredKeyWrapper> RightWord => this.rightWord.Keys;

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
			HandleKeyHighlight(key);
			HandleBackgroundColorChange(key);
			HandleDirection(key);
		}

		#endregion

		#region Private Methods

		private void HandleKeyHighlight(Key key)
		{
			KeyChangeWrapper keyHit =
				FirstRow.Concat(SecondRow).Concat(HomeRow).Concat(FourthRow).SingleOrDefault(x => key.Equals(x.Key));
			keyHit?.SendHighlightSignal();
		}

		private void HandleBackgroundColorChange(Key key)
		{
			this.wordProcessor.InjectKey(key);
			string word = this.wordProcessor.Next();

			if (this.brushConverterCache.IsValid(word))
			{
				BackgroundColor = this.brushConverterCache.ConvertFromString(word) as SolidColorBrush;
			}
		}

		private void HandleDirection(Key key)
		{
			if (!this.leftWord.IsMatchStarted && !this.rightWord.IsMatchStarted)
			{
				this.leftWord.InjectKey(key);
				this.rightWord.InjectKey(key);
				return;
			}

			if (this.leftWord.IsMatchStarted)
			{
				this.leftWord.InjectKey(key);
			}

			if (this.rightWord.IsMatchStarted)
			{
				this.rightWord.InjectKey(key);
			}
		}

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		#endregion

		#region Constants and Fields

		private readonly TypeConverter brushConverterCache = TypeDescriptor.GetConverter(typeof(SolidColorBrush));

		private readonly KeyChangeWrapper[] firstRow =
		{
			new KeyChangeWrapper(Key.D1), new KeyChangeWrapper(Key.D2),
			new KeyChangeWrapper(Key.D3), new KeyChangeWrapper(Key.D4), new KeyChangeWrapper(Key.D5),
			new KeyChangeWrapper(Key.D6), new KeyChangeWrapper(Key.D7), new KeyChangeWrapper(Key.D8),
			new KeyChangeWrapper(Key.D9), new KeyChangeWrapper(Key.D0)
		};

		private readonly KeyChangeWrapper[] fourthRow =
		{
			new KeyChangeWrapper(Key.Z), new KeyChangeWrapper(Key.X),
			new KeyChangeWrapper(Key.C), new KeyChangeWrapper(Key.V), new KeyChangeWrapper(Key.B), new KeyChangeWrapper(Key.N),
			new KeyChangeWrapper(Key.M)
		};

		private readonly KeyChangeWrapper[] homeRow =
		{
			new KeyChangeWrapper(Key.A), new KeyChangeWrapper(Key.S),
			new KeyChangeWrapper(Key.D), new KeyChangeWrapper(Key.F), new KeyChangeWrapper(Key.G), new KeyChangeWrapper(Key.H),
			new KeyChangeWrapper(Key.J), new KeyChangeWrapper(Key.K), new KeyChangeWrapper(Key.L)
		};

		private readonly TrackedWord leftWord;

		private readonly TrackedWord rightWord;

		private readonly KeyChangeWrapper[] secondRow =
		{
			new KeyChangeWrapper(Key.Q), new KeyChangeWrapper(Key.W),
			new KeyChangeWrapper(Key.E), new KeyChangeWrapper(Key.R), new KeyChangeWrapper(Key.T), new KeyChangeWrapper(Key.Y),
			new KeyChangeWrapper(Key.U), new KeyChangeWrapper(Key.I), new KeyChangeWrapper(Key.O), new KeyChangeWrapper(Key.P)
		};

		private readonly WordProcessor wordProcessor = new WordProcessor();

		private SolidColorBrush background;

		private IKeyProcessorHost parent;

		#endregion
	}
}
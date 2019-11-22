#region Using Directives

using System.Text;
using System.Windows;
using System.Windows.Input;

using FingerPartyApp.ViewModels;

#endregion

namespace FingerPartyApp
{
	/// <summary>
	///     Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, IKeyProcessorHost
	{
		#region Constructors

		public MainWindow()
		{
			InitializeComponent();
			PushNextFrame(new TutorialMenuViewModel());
		}

		#endregion

		#region Public Methods

		public void PushNextFrame(IKeyProcessor newProcessor)
		{
			DataContext = newProcessor;
			this.currentProcessor = newProcessor;
			this.currentProcessor.SetupHost(this);
		}

		#endregion

		#region Protected Methods

		protected override void OnPreviewKeyDown(KeyEventArgs e)
		{
			base.OnPreviewKeyDown(e);

			this.TypingHistory.Text = GetTypingHistory(e.Key);
			this.currentProcessor.InjectKey(e.Key);
		}

		#endregion

		#region Private Methods

		private string GetTypingHistory(Key key)
		{
			string keyString = GetKeyString(key);
			EnsureMaxHistoryLength(keyString);
			this.historyBuffer.Append(keyString);

			return this.historyBuffer.ToString();
		}

		private static string GetKeyString(Key key)
		{
			string keyString = key.ToString();
			if (1 < keyString.Length)
			{
				return $" {keyString} ";
			}

			return keyString;
		}

		private void EnsureMaxHistoryLength(string keyString)
		{
			int toRemove = this.historyBuffer.Length + keyString.Length - MaxHistory;
			if (0 < toRemove)
			{
				this.historyBuffer.Remove(0, toRemove);
			}
		}

		#endregion

		#region Constants and Fields

		private const int MaxHistory = 300;

		private readonly StringBuilder historyBuffer = new StringBuilder(MaxHistory);

		private IKeyProcessor currentProcessor;

		#endregion
	}
}
#region Using Directives

using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

#endregion

namespace FingerPartyApp
{
	class WordProcessor
	{
		#region Public Methods

		public void InjectKey(Key key)
		{
			string keyString = key.ToString();
			if (IsWordChar(keyString))
			{
				this.current.Append(keyString);
				return;
			}

			if (0 == this.current.Length)
			{
				return;
			}

			if (IsBackspace(key))
			{
				this.current.Remove(this.current.Length - 1, 1);
			}

			if (IsWordEnd(key))
			{
				this.words.Enqueue(this.current.ToString());
				this.current.Clear();
			}
		}

		public string Next()
		{
			if (0 == this.words.Count)
			{
				return string.Empty;
			}

			return this.words.Dequeue();
		}

		#endregion

		#region Private Methods

		private static bool IsBackspace(Key key)
		{
			return Key.Back == key;
		}

		private static bool IsWordChar(string keyString)
		{
			return 1 == keyString.Length;
		}

		private static bool IsWordEnd(Key key)
		{
			switch (key)
			{
				case Key.Space:
				case Key.Tab:
				case Key.Enter:
					return true;
				default:
					return false;
			}
		}

		#endregion

		#region Constants and Fields

		private readonly StringBuilder current = new StringBuilder();

		private readonly Queue<string> words = new Queue<string>();

		#endregion
	}
}
#region Using Directives

using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;

#endregion

namespace FingerPartyApp
{
	public sealed class TrackedWord
	{
		#region Constructors

		public TrackedWord(string word)
		{
			this.keys = GetKeys(word);
		}

		#endregion

		#region Public Properties

		public IEnumerable<ColoredKeyWrapper> Keys => this.keys;

		public bool IsMatchStarted { get; private set; }

		public bool IsAtEnd { get; private set; }

		#endregion

		#region Public Methods

		public void InjectKey(Key key)
		{
			if (CalculateIsAtEnd())
			{
				IsAtEnd = true;

				return;
			}

			IsMatchStarted = CalculateIsMatchStarted(key);
			if (!IsMatchStarted)
			{
				return;
			}

			this.keys[this.caret].Color = this.keys[this.caret].Key.Equals(key) ? GoodColor : BadColor;

			this.caret++;
		}

		#endregion

		#region Private Methods

		private bool CalculateIsMatchStarted(Key key)
		{
			return this.keys[this.caret].Key.Equals(key) && 0 == this.caret || this.caret != 0;
		}

		private bool CalculateIsAtEnd()
		{
			return this.caret >= this.keys.Length;
		}

		private static ColoredKeyWrapper[] GetKeys(string word)
		{
			return word.Select(x => new ColoredKeyWrapper(GetKey(x))).ToArray();
		}

		private static Key GetKey(char key)
		{
			return (Key)TypeDescriptor.GetConverter(typeof(Key)).ConvertFromString(key.ToString());
		}

		#endregion

		#region Constants and Fields

		private static readonly Brush BadColor = Brushes.OrangeRed;

		private static readonly Brush GoodColor = Brushes.GreenYellow;

		private readonly ColoredKeyWrapper[] keys;

		private int caret;

		#endregion
	}
}
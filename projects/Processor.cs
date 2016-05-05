﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ReactionSeriesSolver
{
	public enum ReactionState
	{
		BOTH_KNOWN,
		LEFT_UNKNOWN,
		RIGHT_UNKNOWN,
		BOTH_UNKNOWN = LEFT_UNKNOWN | RIGHT_UNKNOWN
	}

	public class Pair<T, U>
	{
		public Pair()
		{
		}

		public Pair(T first, U second)
		{
			this.First = first;
			this.Second = second;
		}

		public T First { get; set; }
		public U Second { get; set; }
	};

	public class Reactant
	{
		public List<Pair<string, int>> Atoms { get; set; }
	}

	public static class Processor
	{
		public static List<Pair<List<string>, List<string>>> LoadReactionListFromFile(string filePath)
		{
			var _readstream = new System.IO.FileStream(filePath,
										  System.IO.FileMode.Open,
										  System.IO.FileAccess.Read,
										  System.IO.FileShare.ReadWrite);
			var _reader = new System.IO.StreamReader(_readstream, System.Text.Encoding.UTF8, true, 128);

			List<Pair<List<string>, List<string>>> _reactionList = new List<Pair<List<string>, List<string>>>();

			string _reactionStr = null;
			while ((_reactionStr = _reader.ReadLine()) != null)
			{
				_reactionList.Add(AnalyzeReaction(_reactionStr));
			}
			
			_reader.Dispose();
			_readstream.Dispose();

			return _reactionList;
		}

		public static Pair<List<string>, List<string>> AnalyzeReaction(string reactionStr)
		{
			reactionStr = reactionStr.Split('|')[0];
			reactionStr = reactionStr.Replace(" ", "");
			reactionStr = reactionStr.Replace("=", "->");
			string[] _sides = reactionStr.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

			string[] _reactants = _sides[0].Split('+');
			string[] _products = _sides[1].Split('+');

			return new Pair<List<string>, List<string>>(_reactants.ToList<string>(), _products.ToList<string>());
		}

		public static List<Pair<List<string>, List<string>>> AnalyzeReactionSeries(string reactionSeriesStr)
		{
			List<Pair<List<string>, List<string>>> _reactionSeries = new List<Pair<List<string>, List<string>>>();

			string[] _sides = reactionSeriesStr.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

			for(int i = 0; i < _sides.Length - 1; i++)
			{
				_reactionSeries.Add(AnalyzeReaction(_sides[i] + " -> " + _sides[i + 1]));
			}

			return _reactionSeries;
		}

		public static int FindReaction(List<Pair<List<string>, List<string>>> reactionList, 
																	Pair<List<string>, List<string>> reaction, int offset = 0)
		{
			for(int i = offset + 1; i < reactionList.Count; i++)
			{
				if (IsContain(reactionList[i], reaction))
					return i;
			}

			return -1;
		}

		delegate string HTMLGenerator(string className, string value);
		delegate string TextFormat(string tag, string value);
		delegate string ReactionSideGenerator(List<List<Pair<string, int>>> info, List<int> coefs, int offset);
		
		public static string GenerateReaction(Pair<List<List<Pair<string, int>>>, List<List<Pair<string, int>>>> reactionInfo, List<int> coefficients)
		{
			HTMLGenerator _generator = delegate(string className, string value){
				return "<span class=\"" + className + "\">" + value + "</span>";
			};

			TextFormat _formater = delegate (string tag, string value) {
				return "<" + tag + ">" + value + "</" + tag + ">";
			};

			ReactionSideGenerator _sideGenerator = delegate(List<List<Pair<string, int>>> info, List<int> coefs, int offset){
				string _value = "";

				for (int i = 0; i < info.Count; i++)
				{
					if (coefficients[i + offset] != 1)
						_value += _generator("coeff", coefficients[i + offset].ToString());

					foreach (Pair<string, int> _element in info[i])
					{
						_value += _generator("element", _element.First);
						if (_element.Second != 1)
							_value += _formater("sub", _element.Second.ToString());
					}

					if (i < info.Count - 1)
						_value += _generator("", " + ");
				}

				return _value;
			};

			string _reaction = "";
			_reaction += _sideGenerator(reactionInfo.First, coefficients, 0);
			_reaction += _generator("arrow", " → ");
			_reaction += _sideGenerator(reactionInfo.First, coefficients, reactionInfo.First.Count);

			return _reaction;
		}

		private static bool IsContain(Pair<List<string>, List<string>> container, Pair<List<string>, List<string>> candidate)
		{
			if (container.First.Count < candidate.First.Count)
				return false;
			if (container.Second.Count < candidate.Second.Count)
				return false;

			foreach (string _str in candidate.First)
			{
				if (_str != "?" && !container.First.Contains(_str))
					return false;
			}

			foreach (string _str in candidate.Second)
			{
				if (_str != "?" && !container.Second.Contains(_str))
					return false;
			}

			return true;
		}

		public static ReactionState GetReactionState(Pair<List<string>, List<string>> reaction)
		{
			ReactionState _state = new ReactionState();
			_state = ReactionState.BOTH_KNOWN;

			if(reaction.First.Contains("?"))
			{
				_state = ReactionState.LEFT_UNKNOWN;
			}

			if(reaction.Second.Contains("?"))
			{
				if (_state == ReactionState.LEFT_UNKNOWN)
					_state = ReactionState.BOTH_UNKNOWN;
				else
					_state = ReactionState.RIGHT_UNKNOWN;
			}

			return _state;
		}

		public static string GetNextElement(List<string> list, string current)
		{
			if (current == "?" && list.Count > 0)
				return list[0];

			for(int i = 0; i < list.Count - 1; i++)
			{
				if(list[i] == current)
				{
					return list[i + 1];
				}
			}

			return null;
		}

		public static int GCD(int first, int second)
		{
			first = Math.Abs(first);
			second = Math.Abs(second);

			while(second != 0)
			{
				int _temp = first % second;
				first = second;
				second = _temp;
			}

			return first;
		}

		public static int GCD(List<int> row)
		{
			int _result = 0;

			foreach(int _value in row)
			{
				_result = GCD(_value, _result);
			}

			return _result;
		}
	}
}

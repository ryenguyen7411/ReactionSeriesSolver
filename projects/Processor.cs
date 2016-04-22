using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactionSeriesSolver
{
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

	public static class Processor
	{
		public static List<Pair<List<string>, List<string>>> LoadReactionFromFile(string filePath)
		{
			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			// FOR DEBUG
			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			List< Pair<List<string>, List<string>> > _reactionList = new List<Pair<List<string>, List<string>>>();

			_reactionList.Add(AddReaction("blablabla"));

			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			// FOR RELEASE
			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			// TODO: Add real algorithm

			return _reactionList;
		}

		public static Pair<List<string>, List<string>> AddReaction(string reaction)
		{
			List<string> _first = new List<string>();
			List<string> _second = new List<string>();

			_first.Add("Cl2");
			_first.Add("NaOH");

			_second.Add("NaCl");
			_second.Add("NaClO");
			_second.Add("H2O");

			return new Pair<List<string>, List<string>>(_first, _second);
		}

		public static Pair<List<string>, List<string>> AnalyzeReaction(string reaction)
		{
			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			// FOR DEBUG
			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			List<string> _first = new List<string>();
			List<string> _second = new List<string>();

			_first.Add("Cl2");

			_second.Add("NaCl");
			_second.Add("NaClO");

			return new Pair<List<string>, List<string>>(_first, _second);

			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			// FOR RELEASE
			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			// TODO: Add real algorithm
		}

		public static Pair<List<string>, List<string>> FindReaction(List<Pair<List<string>, List<string>>> reactionList, 
																	Pair<List<string>, List<string>> reaction)
		{
			// TODO: Add default value: null
			return reactionList[0];
		}

		public static List<int> BalanceReaction(Pair<List<string>, List<string>> reaction)
		{
			List<int> _coefficients = new List<int>();

			_coefficients.Add(1);
			_coefficients.Add(2);
			_coefficients.Add(3);
			_coefficients.Add(4);
			_coefficients.Add(5);

			return null;
		}

		public static string GenerateReaction(Pair<List<string>, List<string>> reaction, List<int> coefficients)
		{
			//foreach (string _reactant in reaction.First)
			//{

			//}

			return "Cl2 + NaOH -> NaCl + NaClO + H2O";
		}
	}
}

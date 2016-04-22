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

		public static void AnalyzeReaction(List<string> left, List<string> right, string reaction)
		{
			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			// FOR DEBUG
			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			left.Add("Cl2");
			right.Add("NaCl");
			right.Add("NaClO");

			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			// FOR RELEASE
			// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
			// TODO: Add real algorithm
		}
	}
}

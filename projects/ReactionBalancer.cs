using ReactionSeriesSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReactionSeriesSolver
{
	public static class ReactionBalancer
	{
		public static List<int> BalanceReaction(Pair<List<string>, List<string>> reaction)
		{
			List<List<Pair<string, int>>> _left = new List<List<Pair<string, int>>>();
			foreach (string _reactant in reaction.First)
			{
				_left.Add(ParseTerm(_reactant));
			}

			List<List<Pair<string, int>>> _right = new List<List<Pair<string, int>>>();
			foreach (string _reactant in reaction.Second)
			{
				_right.Add(ParseTerm(_reactant));
			}

			Matrix _matrix = BuildMatrix(_left, _right);
			SolveMatrix(_matrix);

			return _matrix.ExtractCoefficients();
		}

		private static void SolveMatrix(Matrix matrix)
		{
			matrix.GaussJordanEliminate();

			bool _flag = true;
			int i = 0;
			for(i = 0; i < matrix.Rows; i++)
			{
				if(matrix.CountNonZeroFromRow(matrix.Values[i]) != 0)
				{
					_flag = false;
					break;
				}
			}

			if(_flag)
				throw new Exception("All-zero solution");

			matrix[matrix.Rows - 1, i] = 1;
			matrix[matrix.Rows - 1, matrix.Cols - 1] = 1;

			matrix.GaussJordanEliminate();
		}

		private static Matrix BuildMatrix(List<List<Pair<string, int>>> left, List<List<Pair<string, int>>> right)
		{
			List<string> _elementList = new List<string>();
			GetUniqueElements(left, _elementList);
			GetUniqueElements(right, _elementList);

			int _row = _elementList.Count + 1;
			int _col = left.Count + right.Count + 1;

			Matrix _matrix = new Matrix(_row, _col, 0);
			_matrix.ElementList = _elementList;

			for(int i = 0; i < left.Count; i++)
			{
				foreach(Pair<string, int> element in left[i])
				{
					int _index = _elementList.IndexOf(element.First);
					_matrix[_index, i] = element.Second;
				}
			}

			for(int i = 0; i < right.Count; i++)
			{
				foreach (Pair<string, int> element in right[i])
				{
					int _index = _elementList.IndexOf(element.First);
					_matrix[_index, i + left.Count] = -element.Second;
				}
			}

			return _matrix;
		}

		public static List<Pair<string, int>> ParseTerm(string term)
		{
			List<Pair<string, int>> _items = new List<Pair<string, int>>();

			int i = 0;
			while (i < term.Length)
			{
				if (term[i] >= 'A' && term[i] <= 'Z')
				{
					Match _regexMatched = Regex.Match(term.Substring(i), "([A-Z])([a-z]+)?([0-9]+)?");

					if (_regexMatched.Success)
					{
						ParseElement(_regexMatched.Value, _items);
						i += _regexMatched.Value.Length;
					}
				}
				else if(term[i] == '(')
				{
					string _group = FindFirstGroup(term.Substring(i));
					ParseGroup(_group, _items);
					i += _group.Length;
					
				}
				else if(term[i] == 'e')
				{
					_items.Add(new Pair<string, int>("e", 1));

					if(term[i + 1] != 0)
						throw new Exception("Invalid term at " + i + " - electron needs to stand alone");

					break;
				}
				else if(term[i] == '^')
				{
					string _quantityStr = Regex.Match(term.Substring(i), @"(\d+)").Value;
					int _quantity = (_quantityStr != "") ? int.Parse(_quantityStr) : 1;

					i += _quantityStr.Length + 1;
					if(term[i] == '+')
					{
						_quantity *= -1;
					}
					else if(term[i] != '-')
					{
						throw new Exception("Wrong sign expect at " + i);
					}

					_items.Add(new Pair<string, int>("e", _quantity));
					break;
				}
				else
					throw new Exception("Invalid element name at " + i);
			}

			return _items;
		}

		private static void ParseElement(string info, List<Pair<string, int>> list, int ratio = 1)
		{
			string _quantityStr = Regex.Match(info, @"(\d+)").Value;
			int _quantity = ratio;

			if (_quantityStr != "")
			{
				info = info.Substring(0, info.Length - _quantityStr.Length);
				_quantity = int.Parse(_quantityStr) * ratio;
			}

			list.Add(new Pair<string, int>(info, _quantity));
		}

		private static void ParseGroup(string info, List<Pair<string, int>> list, int ratio = 1)
		{
			string _ratioStr = Regex.Match(info, @"(\d+)(?!.*\d)").Value;
			int _ratio = ratio;

			if (_ratioStr != "")
			{
				info = info.Substring(0, info.Length - _ratioStr.Length);
				_ratio = int.Parse(_ratioStr) * ratio;
			}

			info = info.Substring(1, info.Length - 2);

			int i = 0;
			while (i < info.Length)
			{
				if (info[i] >= 'A' && info[i] <= 'Z')
				{
					Match _regexMatched = Regex.Match(info.Substring(i), "([A-Z])([a-z]+)?([0-9]+)?");

					if (_regexMatched.Success)
					{
						ParseElement(_regexMatched.Value, list, _ratio);
						i += _regexMatched.Value.Length;
					}
				}
				else if (info[i] == '(')
				{
					string _group = FindFirstGroup(info.Substring(i));
					ParseGroup(_group, list);
					i += _group.Length;
				}
				else if (info[i] == '^')
				{
					string _quantityStr = Regex.Match(info.Substring(i), @"(\d+)").Value;
					int _quantity = (_quantityStr != "") ? int.Parse(_quantityStr) : 1;

					i += _quantityStr.Length + 1;
					if (info[i] == '+')
					{
						_quantity *= -1;
					}
					else if (info[i] != '-')
					{
						throw new Exception("Wrong sign expect at " + i);
					}

					list.Add(new Pair<string, int>("e", _quantity));
					break;
				}
				else
					throw new Exception("Invalid element name at " + i);
			}
		}

		// TODO: Parse complicate group, such as (K3(OH)2)2(SO4)3
		private static string FindFirstGroup(string term)
		{
			Match _regexMatched = Regex.Match(term, @"\((.*)\)([0-9]+)?");

			if (_regexMatched.Success)
				return _regexMatched.Value;

			return null;
		}

		private static void GetUniqueElements(List<List<Pair<string, int>>> list, List<string> elementList)
		{
			foreach (List<Pair<string, int>> items in list)
			{
				foreach (Pair<string, int> item in items)
				{
					bool _flag = false;
					foreach (string element in elementList)
					{
						if (element == item.First)
						{
							_flag = true;
							break;
						}
					}

					if (!_flag)
					{
						elementList.Add(item.First);
					}
				}
			}
		}
	}
}

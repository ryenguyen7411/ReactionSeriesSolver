using ReactionSeriesSolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReactionSeriesSolver
{
	public class Element
	{
		public string m_element;
		public int m_count;

		public List<Element> m_children;

		public Element()
		{
			m_children = new List<Element>();
		}
	}

	public static class ReactionBalancer
	{
		public static List<int> BalanceReaction(Pair<List<string>, List<string>> reaction, Pair<List<List<Element>>, List<List<Element>>> reactionInfo)
		{
			//Pair<List<List<Element>>, List<List<Element>>> _info = new Pair<List<List<Element>>, List<List<Element>>>();
			//_info.First = new List<List<Element>>();
			foreach (string _reactant in reaction.First)
			{
				//_info.First.Add(new List<Element>());
				reactionInfo.First.Add(ParseTerm(_reactant));
			}

			//_info.Second = new List<List<Element>>();
			foreach (string _reactant in reaction.Second)
			{
				//_info.Second.Add(new List<Element>());
				reactionInfo.Second.Add(ParseTerm(_reactant));
			}

			Matrix _matrix = BuildMatrix(reactionInfo.First, reactionInfo.Second);
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

		private static Matrix BuildMatrix(List<List<Element>> left, List<List<Element>> right)
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
				foreach (Element element in left[i])
				{
					GetIndex(_matrix, element, i, element.m_count);
				}
			}

			for(int i = 0; i < right.Count; i++)
			{
				foreach (Element element in right[i])
				{
					GetIndex(_matrix, element, i + left.Count, element.m_count, -1);
				}
			}

			return _matrix;
		}

		private static void GetIndex(Matrix matrix, Element element, int column, int count, int ratio = 1)
		{
			if (element.m_element != null)
			{
				int _index = matrix.ElementList.IndexOf(element.m_element);
				matrix[_index, column] = count * ratio;
			}
			else
			{
				foreach(Element child in element.m_children)
				{
					GetIndex(matrix, child, column, element.m_count * child.m_count, ratio);
				}
			}
		}

		public static List<Element> ParseTerm(string term)
		{
			List<Element> info = new List<Element>();

			List<Pair<string, int>> _items = new List<Pair<string, int>>();

			int i = 0;
			while (i < term.Length)
			{
				if (term[i] >= 'A' && term[i] <= 'Z')
				{
					Match _regexMatched = Regex.Match(term.Substring(i), "([A-Z])([a-z]+)?([0-9]+)?");

					if (_regexMatched.Success)
					{
						info.Add(ParseElement(_regexMatched.Value, _items));
						i += _regexMatched.Value.Length;
					}
				}
				else if(term[i] == '(')
				{
					string _group = FindFirstGroup(term.Substring(i));
					info.Add(ParseGroup(_group, _items));
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

			return info;
		}

		private static Element ParseElement(string term, List<Pair<string, int>> list, int ratio = 1)
		{
			string _quantityStr = Regex.Match(term, @"(\d+)").Value;
			int _quantity = ratio;

			if (_quantityStr != "")
			{
				term = term.Substring(0, term.Length - _quantityStr.Length);
				_quantity = int.Parse(_quantityStr) * ratio;
			}

			list.Add(new Pair<string, int>(term, _quantity));

			Element _element = new Element();
			_element.m_element = term;
			_element.m_count = _quantity;

			return _element;
		}

		private static Element ParseGroup(string term, List<Pair<string, int>> list, int ratio = 1)
		{
			Element info = new Element();

			string _ratioStr = Regex.Match(term, @"(\d+)(?!.*\d)").Value;
			int _ratio = ratio;

			int _groupRatio = 1;

			if (_ratioStr != "")
			{
				term = term.Substring(0, term.Length - _ratioStr.Length);
				_ratio = int.Parse(_ratioStr) * ratio;
				_groupRatio = int.Parse(_ratioStr);
			}

			info.m_count = _groupRatio;
			term = term.Substring(1, term.Length - 2);

			int i = 0;
			while (i < term.Length)
			{
				if (term[i] >= 'A' && term[i] <= 'Z')
				{
					Match _regexMatched = Regex.Match(term.Substring(i), "([A-Z])([a-z]+)?([0-9]+)?");

					if (_regexMatched.Success)
					{
						info.m_children.Add(ParseElement(_regexMatched.Value, list, _ratio));
						info.m_children.Last().m_count /= _groupRatio;
						i += _regexMatched.Value.Length;
					}
				}
				else if (term[i] == '(')
				{
					string _group = FindFirstGroup(term.Substring(i));
					info.m_children.Add(ParseGroup(_group, list, _ratio));
					info.m_children.Last().m_count /= _groupRatio;
					i += _group.Length;
				}
				else
					throw new Exception("Invalid element name at " + i);
			}

			return info;
		}

		// TODO: Parse complicate group, such as (K3(OH)2)2(SO4)3
		private static string FindFirstGroup(string term)
		{
			Match _regexMatched = Regex.Match(term, @"\((.*)\)([0-9]+)?");

			if (_regexMatched.Success)
				return _regexMatched.Value;

			return null;
		}

		private static void AddElement(Element element, List<string> _elementList, int coefs = 1)
		{
			if (element.m_element == null)
			{
				foreach (Element item in element.m_children)
				{
					AddElement(item, _elementList, coefs * element.m_count);
				}
			}
			else
			{
				bool _flag = false;
				foreach (string item in _elementList)
				{
					if (item == element.m_element)
					{
						_flag = true;
						break;
					}
				}

				if (!_flag)
				{
					_elementList.Add(element.m_element);
				}
			}
		}

		private static void GetUniqueElements(List<List<Element>> list, List<string> elementList)
		{

			foreach (List<Element> items in list)
			{
				foreach (Element item in items)
				{	
					AddElement(item, elementList, item.m_count);
				}
			}
		}
	}
}

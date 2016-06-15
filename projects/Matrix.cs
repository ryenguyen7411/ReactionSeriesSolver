using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReactionSeriesSolver
{
	public class Matrix
	{
		public List<List<int>> Values { get; set; }
		public List<string> ElementList { get; set; }

		public int Rows { get; set; }
		public int Cols { get; set; }

		public Matrix()
		{
			Values = new List<List<int>>();
		}

		public Matrix(int row, int col, int value)
		{
			Values = new List<List<int>>();
			Rows = row;
			Cols = col;

			for (int i = 0; i < row; i++)
			{
				Values.Add(new List<int>());
				for (int j = 0; j < col; j++)
				{
					Values[i].Add(value);
				}
			}
		}

		public Matrix(int size, int value)
		{
			Values = new List<List<int>>();
			Rows = size;
			Cols = size;

			for (int i = 0; i < size; i++)
			{
				Values.Add(new List<int>());
				for (int j = 0; j < size; j++)
				{
					Values[i].Add(value);
				}
			}
		}

		public int this[int x, int y]
		{
			get
			{
				return Values[x][y];
			}

			set
			{
				Values[x][y] = value;
			}
		}

		public void GaussJordanEliminate()
		{
			for(int i = 0; i < Rows; i++)
				Values[i] = SimplifyRow(Values[i]);

			for(int i = 0; i < Cols; i++)
			{
				int _pivot = i;
				while (_pivot < Rows && Values[_pivot][i] == 0)
					_pivot++;

				if (_pivot == Rows)
					continue;

				int _pivotValue = Values[_pivot][i];
				SwapRows(i, _pivot);

				for(int j = i + 1; j < Rows; j++)
				{
					int _gcd = Processor.GCD(_pivotValue, Values[j][i]);
					List<int> _eliminatedRow = Add(Multifly(Values[j], _pivotValue / _gcd), Multifly(Values[i], -Values[j][i] / _gcd));
					Values[j] = SimplifyRow(_eliminatedRow);
				}
			}

			for (int i = Rows - 1; i >= 0; i--)
			{
				int _pivot = 0;
				while (_pivot < Cols && Values[i][_pivot] == 0)
					_pivot++;

				if (_pivot == Cols)
					continue;

				int _pivotValue = Values[i][_pivot];

				for (int j = i - 1; j >= 0; j--)
				{
					int _gcd = Processor.GCD(_pivotValue, Values[j][_pivot]);
					List<int> _eliminatedRow = Add(Multifly(Values[j], _pivotValue / _gcd), Multifly(Values[i], -Values[j][_pivot] / _gcd));
					Values[j] = SimplifyRow(_eliminatedRow);
				}
			}
		}

		private void SwapRows(int first, int second)
		{
			List<int> _temp = Values[first];
			Values[first] = Values[second];
			Values[second] = _temp;
		}

		public int CountNonZeroFromRow(List<int> row)
		{
			int _counter = 0;
			foreach(int _value in row)
			{
				if (_value == 0)
					_counter++;
			}

			return _counter;
		}

		public List<int> SimplifyRow(List<int> row)
		{
			int _sign = 0;
			for(int i = 0; i < row.Count; i++)
			{
				if (row[i] != 0)
				{
					if (row[i] > 0)
						_sign = 1;
					else
						_sign = -1;
					break;
				}
			}

			List<int> _clone = new List<int>(row);
			if (_sign == 0)
				return _clone;

			int _gcd = Processor.GCD(row);
			for (int i = 0; i < _clone.Count; i++)
			{
				_clone[i] /= (_gcd * _sign);
			}

			return _clone;
		}

		public List<int> Add(List<int> row1, List<int> row2)
		{
			List<int> _result = new List<int>();
			for(int i = 0; i < row1.Count; i++)
				_result.Add(row1[i] + row2[i]);

			return _result;
		}

		public List<int> Multifly(List<int> row, int ratio = 1)
		{
			List<int> _result = new List<int>();
			for (int i = 0; i < row.Count; i++)
				_result.Add(row[i] * ratio);

			return _result;
		}

		public List<int> ExtractCoefficients()
		{
			try
			{
				if (Cols - 1 > Rows || Values[Cols - 2][Cols - 2] == 0)
					throw new Exception("Multiple independent solutions");

				int _lcm = 1;
				for (int i = 0; i < Cols - 1; i++)
					_lcm = (_lcm / Processor.GCD(_lcm, Values[i][i])) * Values[i][i];

				bool _flag = true;
				List<int> _coefficients = new List<int>();

				for (int i = 0; i < Cols - 1; i++)
				{
					int _value = (_lcm / Processor.GCD(_lcm, Values[i][i])) * Values[i][Cols - 1];
					_coefficients.Add(_value);
					_flag &= (_value == 0);
				}

				if (_flag)
					throw new Exception("Assertion error: All-zero solution");

				return _coefficients;
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
				return null;
			}
		}
	}
}

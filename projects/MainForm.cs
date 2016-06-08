using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReactionSeriesSolver
{
    public partial class MainForm : Form
    {
		List<Pair<List<string>, List<string>>> m_reactionList;
		public List<Pair<List<string>, List<string>>> ReactionList
		{
			get { return m_reactionList; }
			set { m_reactionList = value; }
		}

		public MainForm()
        {
            InitializeComponent();
			ReactionList = Processor.LoadReactionListFromFile(@"..\..\data\reactions.pro");
		}

		private void btn_solve_reaction_Click(object sender, EventArgs e)
		{
			string _reactionStr = txt_reaction.Text;

			try
			{
				Pair<List<List<Element>>, List<List<Element>>> _reactionInfo = new Pair<List<List<Element>>, List<List<Element>>>();
				_reactionInfo.First = new List<List<Element>>();
				_reactionInfo.Second = new List<List<Element>>();

				Pair<List<string>, List<string>> _reaction = Processor.AnalyzeReaction(_reactionStr);

				int _reactionTargetId = Processor.FindReaction(ReactionList, _reaction);

				if (_reactionTargetId == -1)
					throw new Exception("Cannot find reaction! Please check your input...");

				if (ReactionList[_reactionTargetId] != null)
				{
					List<int> _coefficients = ReactionBalancer.BalanceReaction(ReactionList[_reactionTargetId], _reactionInfo);
					txt_result_reaction.Text = Processor.GenerateReaction(_reactionInfo, _coefficients);
					web_reaction.DocumentText = Processor.GenerateReaction(_reactionInfo, _coefficients);
				}
				else
				{
					txt_result_reaction.Text = "Could not solve reaction!!!";
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("{0} Exception caught.", ex);
			}
		}

		private void btn_solve_reactionSeries_Click(object sender, EventArgs e)
		{
			txt_result_reactionSeries.Text = "";

			try
			{
				string _reactionSeriesStr = txt_reactionSeries.Text;
				List<Pair<List<string>, List<string>>> _reactionSeries = Processor.AnalyzeReactionSeries(_reactionSeriesStr);
				int[] _reactionId = new int[_reactionSeries.Count];
				ReactionState[] _reactionState = new ReactionState[_reactionSeries.Count];

				for (int i = 0; i < _reactionSeries.Count; i++)
				{
					_reactionId[i] = -1;
					_reactionState[i] = Processor.GetReactionState(_reactionSeries[i]);
				}

				for (int i = 0; i < _reactionSeries.Count; i++)
				{
					_reactionId[i] = Processor.FindReaction(ReactionList, _reactionSeries[i], _reactionId[i]);

					if (_reactionId[i] == -1)
					{
						// DONE
						if(i <= 0)
						{
							txt_result_reactionSeries.Text = "NULL";
							return;
						}

						if ((_reactionState[i] & ReactionState.LEFT_UNKNOWN) == ReactionState.LEFT_UNKNOWN)
						{
							string _nextElement = Processor.GetNextElement(_reactionSeries[i].First, _reactionSeries[i].First[0]);

							if (_nextElement != null)
							{
								_reactionSeries[i].First[0] = Processor.GetNextElement(_reactionSeries[i].First, _reactionSeries[i].First[0]);
								_reactionSeries[i - 1].Second[0] = _reactionSeries[i].First[0];
							}
							else
							{
								_reactionSeries[i - 1].Second[0] = _reactionSeries[i].First[0] = "?";
							}
						}

						i--;
						continue;
					}

					if ((_reactionState[i] & ReactionState.RIGHT_UNKNOWN) == ReactionState.RIGHT_UNKNOWN)
					{
						_reactionSeries[i].Second[0] = Processor.GetNextElement(ReactionList[_reactionId[i]].Second, _reactionSeries[i].Second[0]);

						if(i < _reactionSeries.Count - 1)
							_reactionSeries[i + 1].First[0] = _reactionSeries[i].Second[0];
					}
				}

				for (int i = 0; i < _reactionSeries.Count; i++)
				{
					//List<int> _coefficients = ReactionBalancer.BalanceReaction(ReactionList[_reactionId[i]], ReactionInfo);
					//txt_result_reactionSeries.Text += "(" + (i + 1) + "): ";
					//txt_result_reactionSeries.Text += Processor.GenerateReaction(ReactionList[_reactionId[i]], _coefficients);
					//txt_result_reactionSeries.Text += Environment.NewLine;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("{0} Exception caught.", ex);
			}
		}
	}
}

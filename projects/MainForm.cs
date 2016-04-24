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
				Pair<List<string>, List<string>> _reaction = Processor.AnalyzeReaction(_reactionStr);

				int _reactionTargetId = Processor.FindReaction(ReactionList, _reaction);

				if (ReactionList[_reactionTargetId] != null)
				{
					List<int> _coefficients = Processor.BalanceReaction(ReactionList[_reactionTargetId]);
					txt_result_reaction.Text = Processor.GenerateReaction(ReactionList[_reactionTargetId], _coefficients);
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
				string _reactionSeriesStr = txt_reaction.Text;
				List<Pair<List<string>, List<string>>> _reactionSeries = Processor.AnalyzeReactionSeries(_reactionSeriesStr);

				for (int i = 0; i < _reactionSeries.Count; i++)
				{
					int _reactionTargetId = Processor.FindReaction(ReactionList, _reactionSeries[i]);

					if (ReactionList[_reactionTargetId] != null)
					{
						List<int> _coefficients = Processor.BalanceReaction(ReactionList[_reactionTargetId]);
						txt_result_reactionSeries.Text += "(" + (i + 1) + "): " + Processor.GenerateReaction(ReactionList[_reactionTargetId], _coefficients) + Environment.NewLine;
					}
					else
					{
						txt_result_reactionSeries.Text = "Could not solve reaction series!!!";
						break;
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("{0} Exception caught.", ex);
			}
		}
	}
}

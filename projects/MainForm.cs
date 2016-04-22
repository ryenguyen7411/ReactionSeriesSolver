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
			ReactionList = Processor.LoadReactionFromFile("reactions.pro");
		}

		private void btn_solve_reaction_Click(object sender, EventArgs e)
		{
			string _reactionStr = txt_reaction.Text;
			Pair<List<string>, List<string>> _reaction = Processor.AnalyzeReaction(_reactionStr);

			Pair<List<string>, List<string>> _reactionTarget = Processor.FindReaction(ReactionList, _reaction);

			if(_reactionTarget != null)
			{
				List<int> _coefficients = Processor.BalanceReaction(_reactionTarget);
				txt_result_reaction.Text = Processor.GenerateReaction(_reactionTarget, _coefficients);
			}
			else
			{
				txt_result_reaction.Text = "Could not find reaction!!!";
			}
		}

		private void btn_solve_reactionSeries_Click(object sender, EventArgs e)
		{

		}
	}
}

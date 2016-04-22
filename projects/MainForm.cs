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
		List<string> m_elementsLeft;
		public List<string> ElementLeft
		{
			get { return m_elementsLeft; }
			set { m_elementsLeft = value; }
		}

		List<string> m_elementsRight;
		public List<string> ElementRight
		{
			get { return m_elementsRight; }
			set { m_elementsRight = value; }
		}

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
			string _reaction = txt_reaction.Text;
			Processor.AnalyzeReaction(ElementLeft, ElementRight, _reaction);
		}

		private void btn_solve_reactionSeries_Click(object sender, EventArgs e)
		{

		}
	}
}

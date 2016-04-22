namespace ReactionSeriesSolver
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.tab_pageControl = new System.Windows.Forms.TabControl();
			this.tab_reaction = new System.Windows.Forms.TabPage();
			this.txt_result_reaction = new LollipopTextBox();
			this.btn_solve_reaction = new LollipopButton();
			this.txt_reaction = new LollipopTextBox();
			this.tab_reactionSeries = new System.Windows.Forms.TabPage();
			this.txt_result_reactionSeries = new LollipopTextBox();
			this.btn_solve_reactionSeries = new LollipopButton();
			this.txt_reactionSeries = new LollipopTextBox();
			this.tab_pageControl.SuspendLayout();
			this.tab_reaction.SuspendLayout();
			this.tab_reactionSeries.SuspendLayout();
			this.SuspendLayout();
			// 
			// tab_pageControl
			// 
			this.tab_pageControl.Controls.Add(this.tab_reaction);
			this.tab_pageControl.Controls.Add(this.tab_reactionSeries);
			this.tab_pageControl.Location = new System.Drawing.Point(12, 12);
			this.tab_pageControl.Name = "tab_pageControl";
			this.tab_pageControl.SelectedIndex = 0;
			this.tab_pageControl.Size = new System.Drawing.Size(760, 417);
			this.tab_pageControl.TabIndex = 0;
			// 
			// tab_reaction
			// 
			this.tab_reaction.Controls.Add(this.txt_result_reaction);
			this.tab_reaction.Controls.Add(this.btn_solve_reaction);
			this.tab_reaction.Controls.Add(this.txt_reaction);
			this.tab_reaction.Location = new System.Drawing.Point(4, 22);
			this.tab_reaction.Name = "tab_reaction";
			this.tab_reaction.Padding = new System.Windows.Forms.Padding(3);
			this.tab_reaction.Size = new System.Drawing.Size(752, 391);
			this.tab_reaction.TabIndex = 0;
			this.tab_reaction.Text = "Reaction";
			this.tab_reaction.UseVisualStyleBackColor = true;
			// 
			// txt_result_reaction
			// 
			this.txt_result_reaction.FocusedColor = "#508ef5";
			this.txt_result_reaction.FontColor = "#999999";
			this.txt_result_reaction.IsEnabled = true;
			this.txt_result_reaction.Location = new System.Drawing.Point(158, 148);
			this.txt_result_reaction.MaxLength = 32767;
			this.txt_result_reaction.Multiline = true;
			this.txt_result_reaction.Name = "txt_result_reaction";
			this.txt_result_reaction.ReadOnly = false;
			this.txt_result_reaction.Size = new System.Drawing.Size(430, 164);
			this.txt_result_reaction.TabIndex = 2;
			this.txt_result_reaction.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.txt_result_reaction.UseSystemPasswordChar = false;
			// 
			// btn_solve_reaction
			// 
			this.btn_solve_reaction.BackColor = System.Drawing.Color.Transparent;
			this.btn_solve_reaction.BGColor = "#508ef5";
			this.btn_solve_reaction.FontColor = "#ffffff";
			this.btn_solve_reaction.Location = new System.Drawing.Point(445, 85);
			this.btn_solve_reaction.Name = "btn_solve_reaction";
			this.btn_solve_reaction.Size = new System.Drawing.Size(143, 41);
			this.btn_solve_reaction.TabIndex = 1;
			this.btn_solve_reaction.Text = "SOLVE";
			this.btn_solve_reaction.Click += new System.EventHandler(this.btn_solve_reaction_Click);
			// 
			// txt_reaction
			// 
			this.txt_reaction.FocusedColor = "#508ef5";
			this.txt_reaction.FontColor = "#999999";
			this.txt_reaction.IsEnabled = true;
			this.txt_reaction.Location = new System.Drawing.Point(158, 102);
			this.txt_reaction.MaxLength = 32767;
			this.txt_reaction.Multiline = false;
			this.txt_reaction.Name = "txt_reaction";
			this.txt_reaction.ReadOnly = false;
			this.txt_reaction.Size = new System.Drawing.Size(281, 24);
			this.txt_reaction.TabIndex = 0;
			this.txt_reaction.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.txt_reaction.UseSystemPasswordChar = false;
			// 
			// tab_reactionSeries
			// 
			this.tab_reactionSeries.Controls.Add(this.txt_result_reactionSeries);
			this.tab_reactionSeries.Controls.Add(this.btn_solve_reactionSeries);
			this.tab_reactionSeries.Controls.Add(this.txt_reactionSeries);
			this.tab_reactionSeries.Location = new System.Drawing.Point(4, 22);
			this.tab_reactionSeries.Name = "tab_reactionSeries";
			this.tab_reactionSeries.Padding = new System.Windows.Forms.Padding(3);
			this.tab_reactionSeries.Size = new System.Drawing.Size(752, 391);
			this.tab_reactionSeries.TabIndex = 1;
			this.tab_reactionSeries.Text = "Reaction Series";
			this.tab_reactionSeries.UseVisualStyleBackColor = true;
			// 
			// txt_result_reactionSeries
			// 
			this.txt_result_reactionSeries.FocusedColor = "#508ef5";
			this.txt_result_reactionSeries.FontColor = "#999999";
			this.txt_result_reactionSeries.IsEnabled = true;
			this.txt_result_reactionSeries.Location = new System.Drawing.Point(161, 145);
			this.txt_result_reactionSeries.MaxLength = 32767;
			this.txt_result_reactionSeries.Multiline = true;
			this.txt_result_reactionSeries.Name = "txt_result_reactionSeries";
			this.txt_result_reactionSeries.ReadOnly = false;
			this.txt_result_reactionSeries.Size = new System.Drawing.Size(430, 164);
			this.txt_result_reactionSeries.TabIndex = 5;
			this.txt_result_reactionSeries.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.txt_result_reactionSeries.UseSystemPasswordChar = false;
			// 
			// btn_solve_reactionSeries
			// 
			this.btn_solve_reactionSeries.BackColor = System.Drawing.Color.Transparent;
			this.btn_solve_reactionSeries.BGColor = "#508ef5";
			this.btn_solve_reactionSeries.FontColor = "#ffffff";
			this.btn_solve_reactionSeries.Location = new System.Drawing.Point(448, 82);
			this.btn_solve_reactionSeries.Name = "btn_solve_reactionSeries";
			this.btn_solve_reactionSeries.Size = new System.Drawing.Size(143, 41);
			this.btn_solve_reactionSeries.TabIndex = 4;
			this.btn_solve_reactionSeries.Text = "SOLVE";
			this.btn_solve_reactionSeries.Click += new System.EventHandler(this.btn_solve_reactionSeries_Click);
			// 
			// txt_reactionSeries
			// 
			this.txt_reactionSeries.FocusedColor = "#508ef5";
			this.txt_reactionSeries.FontColor = "#999999";
			this.txt_reactionSeries.IsEnabled = true;
			this.txt_reactionSeries.Location = new System.Drawing.Point(161, 99);
			this.txt_reactionSeries.MaxLength = 32767;
			this.txt_reactionSeries.Multiline = false;
			this.txt_reactionSeries.Name = "txt_reactionSeries";
			this.txt_reactionSeries.ReadOnly = false;
			this.txt_reactionSeries.Size = new System.Drawing.Size(281, 24);
			this.txt_reactionSeries.TabIndex = 3;
			this.txt_reactionSeries.TextAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.txt_reactionSeries.UseSystemPasswordChar = false;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(784, 441);
			this.Controls.Add(this.tab_pageControl);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Reaction Series Solver";
			this.tab_pageControl.ResumeLayout(false);
			this.tab_reaction.ResumeLayout(false);
			this.tab_reactionSeries.ResumeLayout(false);
			this.ResumeLayout(false);

        }



		#endregion

		private System.Windows.Forms.TabControl tab_pageControl;
		private System.Windows.Forms.TabPage tab_reaction;
		private System.Windows.Forms.TabPage tab_reactionSeries;
		private LollipopTextBox txt_result_reaction;
		private LollipopButton btn_solve_reaction;
		private LollipopTextBox txt_reaction;
		private LollipopTextBox txt_result_reactionSeries;
		private LollipopButton btn_solve_reactionSeries;
		private LollipopTextBox txt_reactionSeries;
	}
}


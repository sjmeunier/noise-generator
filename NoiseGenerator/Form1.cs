using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Microsoft.DirectX.DirectSound;

namespace NoiseGenerator
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button butGenerate;
		private System.Windows.Forms.ComboBox cboWaveType;
		private System.Windows.Forms.TextBox txtFreq;
		private System.Windows.Forms.ComboBox cboNotes;
		private System.Windows.Forms.Button butStop;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		
		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.butGenerate = new System.Windows.Forms.Button();
			this.cboWaveType = new System.Windows.Forms.ComboBox();
			this.txtFreq = new System.Windows.Forms.TextBox();
			this.cboNotes = new System.Windows.Forms.ComboBox();
			this.butStop = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// butGenerate
			// 
			this.butGenerate.Location = new System.Drawing.Point(344, 16);
			this.butGenerate.Name = "butGenerate";
			this.butGenerate.Size = new System.Drawing.Size(104, 24);
			this.butGenerate.TabIndex = 0;
			this.butGenerate.Text = "&Generate";
			this.butGenerate.Click += new System.EventHandler(this.butGenerate_Click);
			// 
			// cboWaveType
			// 
			this.cboWaveType.Items.AddRange(new object[] {
															 "White Noise",
															 "Sine Wave",
															 "Square Wave",
															 "Sawtooth Wave"});
			this.cboWaveType.Location = new System.Drawing.Point(40, 16);
			this.cboWaveType.Name = "cboWaveType";
			this.cboWaveType.Size = new System.Drawing.Size(128, 21);
			this.cboWaveType.TabIndex = 1;
			this.cboWaveType.Text = "White Noise";
			// 
			// txtFreq
			// 
			this.txtFreq.Location = new System.Drawing.Point(40, 48);
			this.txtFreq.Name = "txtFreq";
			this.txtFreq.Size = new System.Drawing.Size(88, 20);
			this.txtFreq.TabIndex = 2;
			this.txtFreq.Text = "440";
			// 
			// cboNotes
			// 
			this.cboNotes.Location = new System.Drawing.Point(136, 48);
			this.cboNotes.Name = "cboNotes";
			this.cboNotes.Size = new System.Drawing.Size(152, 21);
			this.cboNotes.TabIndex = 3;
			this.cboNotes.SelectedIndexChanged += new System.EventHandler(this.cboNotes_SelectedIndexChanged);
			// 
			// butStop
			// 
			this.butStop.Location = new System.Drawing.Point(344, 56);
			this.butStop.Name = "butStop";
			this.butStop.Size = new System.Drawing.Size(104, 24);
			this.butStop.TabIndex = 4;
			this.butStop.Text = "&Stop";
			this.butStop.Click += new System.EventHandler(this.butStop_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 271);
			this.Controls.Add(this.butStop);
			this.Controls.Add(this.cboNotes);
			this.Controls.Add(this.txtFreq);
			this.Controls.Add(this.cboWaveType);
			this.Controls.Add(this.butGenerate);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}
		private NoiseGen oGen;

		private void butGenerate_Click(object sender, System.EventArgs e)
		{
			double Freq = Convert.ToDouble(txtFreq.Text);
			oGen.Generate(cboWaveType.Text, Freq);
		

		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			oGen = new NoiseGen(this.Handle);

			cboNotes.Items.Clear();
			for (int i = 0; i < 100; i++)
			{
				cboNotes.Items.Add(Notes.NoteName[i] + " (" + Notes.NoteFreq[i].ToString() + ")");
			}
			cboNotes.SelectedIndex = 48;
		}

		private void cboNotes_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			txtFreq.Text = Notes.NoteFreq[cboNotes.SelectedIndex].ToString();
		}

		private void butStop_Click(object sender, System.EventArgs e)
		{
			oGen.Stop();
		}



	}
}

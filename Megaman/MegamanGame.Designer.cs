namespace Megaman
{
    partial class MegamanGame
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
            this.components = new System.ComponentModel.Container();
            this._panelGame = new System.Windows.Forms.Panel();
            this._gameTime = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // _panelGame
            // 
            this._panelGame.BackColor = System.Drawing.Color.Aqua;
            this._panelGame.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelGame.Location = new System.Drawing.Point(0, 0);
            this._panelGame.Name = "_panelGame";
            this._panelGame.Size = new System.Drawing.Size(1773, 800);
            this._panelGame.TabIndex = 0;
            // 
            // _gameTime
            // 
            this._gameTime.Interval = 20;
            this._gameTime.Tick += new System.EventHandler(this._gameTime_Tick);
            // 
            // MegamanGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1773, 800);
            this.Controls.Add(this._panelGame);
            this.Name = "MegamanGame";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelGame;
        private System.Windows.Forms.Timer _gameTime;
    }
}


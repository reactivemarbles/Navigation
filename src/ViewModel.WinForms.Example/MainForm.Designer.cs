﻿namespace ViewModel.WinForms.Example;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        NavBack = new Button();
        SuspendLayout();
        // 
        // NavBack
        // 
        NavBack.Dock = DockStyle.Top;
        NavBack.Location = new Point(0, 0);
        NavBack.Name = "NavBack";
        NavBack.Size = new Size(800, 34);
        NavBack.TabIndex = 1;
        NavBack.Text = "Back";
        NavBack.UseVisualStyleBackColor = true;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(10F, 25F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(800, 450);
        Controls.Add(NavBack);
        Name = "MainForm";
        Text = "Form1";
        ResumeLayout(false);
    }

    #endregion

    private Button NavBack;
}

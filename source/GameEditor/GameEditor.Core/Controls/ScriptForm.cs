using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FastColoredTextBoxNS;
using System.Text.RegularExpressions;
using Microsoft.Xna.Framework;
using ForceField.Interfaces;

namespace GameEditor.Core.Controls
{
    public partial class ScriptForm : Form
    {
        public ScriptForm(Game game)
        {

            scriptService = game.Services.GetService(typeof(IScriptService)) as IScriptService;
            InitializeComponent();
        }

        private void ColoredTextBoxTextChange(object sender, TextChangedEventArgs e)
        {
            #region Устанавливает ключевые слова
            e.ChangedRange.SetStyle(System.Drawing.Color.Green, System.Drawing.Color.Transparent, FontStyle.Italic, @"//.*$", RegexOptions.Multiline);
            e.ChangedRange.SetStyle(System.Drawing.Color.Blue, @"\b(abstract|as|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|do|double|else|enum|event|explicit|extern|false|finally|fixed|float|for|foreach|goto|if|implicit|in|int|interface|internal|is|lock|long|namespace|new|null|object|operator|out|override|params|private|protected|public|readonly|ref|return|sbyte|sealed|short|sizeof|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|ulong|unchecked|unsafe|ushort|using|virtual|void|volatile|while|add|alias|ascending|descending|dynamic|from|get|global|group|into|join|let|orderby|partial|remove|select|set|value|var|where|yield)\b");
            e.ChangedRange.SetStyle(System.Drawing.FontStyle.Bold, @"\b(class|struct|enum)\s+(?<range>[\w_]+?)\b");
            e.ChangedRange.SetStyle(System.Drawing.Color.Magenta, @"\b\d+[\.]?\d*([eE]\-?\d+)?[lLdDfF]?\b|\b0x[a-fA-F\d]+\b");
            #endregion
        }

        private void ScriptForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] errors = new string[1];

            scriptService.Compile(coloredTextBox.Text, out errors);

            if (errors[0] != "Complete")
            {
                //вывести ошибки
            }
            else
            {
                ResultCompile.Text = "Результат проверки:";
                ResultCompile.Text += "OK";
            }

        }

        IScriptService scriptService;
    }
}

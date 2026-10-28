using System;
using System.Drawing;
using System.Windows.Forms;

namespace PasswordAlphabetHighlighter
{
    internal sealed class PasswordColorForm : Form
    {
        private readonly string _password;
        private readonly RichTextBox _output;
        private readonly Button _toggleButton;
        private bool _visible = true;

        public PasswordColorForm(string password)
        {
            _password = password ?? string.Empty;

            Text = "Password Alphabet Highlighter";
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(560, 160);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;

            _output = new RichTextBox
            {
                Location = new Point(12, 12),
                Size = new Size(536, 78),
                ReadOnly = true,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(35, 35, 35),
                ForeColor = Color.White,
                Font = new Font("Consolas", 15f),
                WordWrap = false,
                DetectUrls = false,
                HideSelection = false,
                ScrollBars = RichTextBoxScrollBars.Horizontal
            };

            _toggleButton = new Button
            {
                Text = "Скрыть пароль",
                Location = new Point(12, 105),
                Size = new Size(150, 32)
            };
            _toggleButton.Click += ToggleVisibility;

            Controls.Add(_output);
            Controls.Add(_toggleButton);
            RenderPassword();
        }

        private static Color GetCharacterColor(char c)
        {
            if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'))
                return Color.FromArgb(255, 80, 80);

            if ((c >= '\u0410' && c <= '\u044F') ||
                c == '\u0401' || c == '\u0451')
                return Color.FromArgb(80, 220, 120);

            if (c >= '0' && c <= '9')
                return Color.White;

            return Color.LightGray;
        }

        private void RenderPassword()
        {
            _output.Clear();
            foreach (char c in _password)
            {
                _output.SelectionStart = _output.TextLength;
                _output.SelectionLength = 0;
                _output.SelectionColor = _visible
                    ? GetCharacterColor(c)
                    : Color.FromArgb(35, 35, 35);
                _output.AppendText(_visible ? c.ToString() : "\u2022");
            }

            _output.SelectionStart = 0;
            _output.SelectionLength = 0;
        }

        private void ToggleVisibility(object sender, EventArgs e)
        {
            _visible = !_visible;
            _toggleButton.Text = _visible ? "Скрыть пароль" : "Показать пароль";
            RenderPassword();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _toggleButton.Click -= ToggleVisibility;
                _output.Dispose();
                _toggleButton.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

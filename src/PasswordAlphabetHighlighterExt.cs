using System;
using System.Windows.Forms;
using KeePass.Plugins;
using KeePassLib;

namespace PasswordAlphabetHighlighter
{
    public sealed class PasswordAlphabetHighlighterExt : Plugin
    {
        private IPluginHost _host;
        private ToolStripMenuItem _menuItem;

        public override bool Initialize(IPluginHost host)
        {
            if (host == null) return false;

            _host = host;
            _menuItem = new ToolStripMenuItem("Подсветить алфавиты пароля...");
            _menuItem.Click += OnClick;
            _host.MainWindow.ToolsMenu.DropDownItems.Add(_menuItem);
            return true;
        }

        private void OnClick(object sender, EventArgs e)
        {
            PwEntry entry = _host.MainWindow.GetSelectedEntry(false);
            if (entry == null)
            {
                MessageBox.Show(
                    _host.MainWindow,
                    "Сначала выбери запись KeePass.",
                    "Password Alphabet Highlighter",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            string password = entry.Strings.ReadSafe("Password");
            using (PasswordColorForm form = new PasswordColorForm(password))
            {
                form.ShowDialog(_host.MainWindow);
            }
        }

        public override void Terminate()
        {
            if (_menuItem != null)
            {
                _menuItem.Click -= OnClick;
                if (_host != null)
                    _host.MainWindow.ToolsMenu.DropDownItems.Remove(_menuItem);
                _menuItem.Dispose();
                _menuItem = null;
            }
            _host = null;
        }
    }
}

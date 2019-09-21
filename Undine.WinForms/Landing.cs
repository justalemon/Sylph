using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Undine.Formats;

namespace Undine.WinForms
{
    public partial class Landing : Form
    {
        private static readonly About AboutForm = new About();

        public Landing()
        {
            InitializeComponent();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Open the search file dialog
            DialogResult Result = OpenFile.ShowDialog();
            // If the user cancelled the operation, return
            if (Result != DialogResult.OK)
            {
                return;
            }

            // Then, open the file stream
            using (Stream FileStream = OpenFile.OpenFile())
            using (BinaryReader Reader = new BinaryReader(FileStream))
            {
                // Detect and get the correct type of game
                Format Type = Format.Detect(Reader);

                // If there is no game, notify the user and return
                if (Type == null)
                {
                    MessageBox.Show("We checked the file that you provided and is not a valid rom.\nMake sure that the rom is valid and works.");
                    return;
                }

                // Otherwise, fill the spaces for the basic info
                NameLabel.Text = Type.Title;
                DeveloperLabel.Text = Type.Developer;
                IdentifierLabel.Text = Type.Identifier;
                ConsoleLabel.Text = Type.Console;
                RegionLabel.Text = Type.Region;

                // And the advanced info
                // But first clear the existing items
                AdvancedListView.Items.Clear();
                foreach (KeyValuePair<string, object> prop in Type.GetAdvancedInformation())
                {
                    ListViewItem item = AdvancedListView.Items.Add(prop.Key);
                    item.SubItems.Add(prop.Value.ToString());
                }
            }
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.ShowDialog();
        }
    }
}

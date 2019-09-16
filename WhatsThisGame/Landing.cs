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
using WhatsThisGame.Formats;

namespace WhatsThisGame
{
    public partial class Landing : Form
    {
        public Landing()
        {
            InitializeComponent();
        }

        private void FileButton_Click(object sender, EventArgs e)
        {
            // Open the search file dialog
            DialogResult Result = OpenFile.ShowDialog();
            // If the user cancelled the operation, return
            if (Result != DialogResult.OK)
            {
                return;
            }
            // Save the filename on the text box
            FileTextBox.Text = OpenFile.FileName;

            // Then, open the file stream
            using (Stream FileStream = OpenFile.OpenFile())
            {
                // Detect and get the correct type of game
                Format Type = Detection.Detect(FileStream);

                // If there is no game, notify the user and return
                if (Type == null)
                {
                    MessageBox.Show("We checked the file that you provided and is not a valid rom.\nMake sure that the rom is valid and works.");
                    return;
                }

                // Otherwise, fill the spaces
                NameLabel.Text = Type.Title;
                DeveloperLabel.Text = Type.Developer;
                IdentifierLabel.Text = Type.Identifier;
                ConsoleLabel.Text = Type.Console;
                RegionLabel.Text = Type.Region;
            }
        }
    }
}

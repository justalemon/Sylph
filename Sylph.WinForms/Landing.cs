﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sylph.Formats;

namespace Sylph.WinForms
{
    public partial class Landing : Form
    {
        private static readonly About AboutForm = new About();

        public Landing()
        {
            InitializeComponent();
        }

        private void Landing_Load(object sender, EventArgs e)
        {
            // Reset the fields after loading the form
            ResetFields();
        }

        private void ResetFields()
        {
            // Reset the fields with basic information
            NameLabel.Text = string.Empty;
            DeveloperLabel.Text = string.Empty;
            IdentifierLabel.Text = string.Empty;
            ConsoleLabel.Text = string.Empty;
            RegionLabel.Text = string.Empty;

            // And empty the items with the extended/advanced information and localized titles
            AdvancedListView.Items.Clear();
            LocalizedListView.Items.Clear();
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
                ResetFields();
                NameLabel.Text = Type.Title;
                DeveloperLabel.Text = Type.Developer;
                IdentifierLabel.Text = Type.Identifier;
                ConsoleLabel.Text = Type.Console;
                RegionLabel.Text = Type.Region;

                // The localized titles
                foreach (KeyValuePair<string, string> locale in Type.LocalizedTitles)
                {
                    ListViewItem item = LocalizedListView.Items.Add(locale.Key);
                    item.SubItems.Add(locale.Value.ToString());
                }

                // And the advanced info
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

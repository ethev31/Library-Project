using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementSystem
{
    public partial class AddBookForm: Form
    {
        public AddBookForm()
        {
            InitializeComponent();
        }

        public string BookTitle => txtTitle.Text;
        public string BookAuthor => txtAuthor.Text;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text) || string.IsNullOrWhiteSpace(txtAuthor.Text))
            {
                MessageBox.Show("Please enter both a title and an author.");
                return;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}

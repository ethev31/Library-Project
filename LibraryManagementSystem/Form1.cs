using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryManagementBL;

namespace LibraryManagementSystem
{
    public partial class Form1: Form
    {
        private List<Book> books = new List<Book>();
        private List<User> users = new List<User>();
        private Dictionary<int, List<Book>> borrowedBooksByUser = new Dictionary<int, List<Book>>();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            users.Add(new Student { Name = "Jane", UserId = 1, Role = "Student" });
            users.Add(new Teacher { Name = "Joe", UserId = 2, Role = "Teacher" });
            users.Add(new Librarian { Name = "Bob", UserId = 3, Role = "Librarian" });

            cboUsers.DataSource = users;
            cboUsers.DisplayMember = "Role";

            books.Add(new Book { Title = "The Hobbit", Author = "J.R.R Tolkien" });
            books.Add(new Book { Title = "The Da Vinci Code", Author = "Dan Brown" });
            books.Add(new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" });
            books.Add(new Book { Title = "1984", Author = "George Orwell" });
            books.Add(new Book { Title = "Lord of the Flies", Author = "William Golding" });
            books.Add(new Book { Title = "In Cold Blood", Author = "Truman Capote" });
            books.Add(new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee" });
            books.Add(new Book { Title = "The Grapes of Wrath", Author = "John Steinbeck" });
            books.Add(new Book { Title = "The Catcher in the Rye", Author = "J.D. Salinger" });
            books.Add(new Book { Title = "Adventures of Huckleberry Finn", Author = "Mark Twain" });
            dgvBooks.DataSource = new BindingSource { DataSource = books };

            btnAddBook.Enabled = false;
        }

        private void cboUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var user = cboUsers.SelectedItem as User;
            btnAddBook.Enabled = user is Librarian;
        }

        private void btnBorrow_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                var book = (Book)dgvBooks.SelectedRows[0].DataBoundItem;
                var user = (User)cboUsers.SelectedItem;

                if (!book.IsAvailable)
                {
                    MessageBox.Show($"{book.Title} is already borrowed.");
                    return;
                }

                if (!(user is Librarian))
                {
                    if (!borrowedBooksByUser.ContainsKey(user.UserId))
                        borrowedBooksByUser[user.UserId] = new List<Book>();

                    var borrowedBooks = borrowedBooksByUser[user.UserId];
                    int limit = 0;

                    if (user is Student s)
                        limit = s.BorrowLimit;
                    else if (user is Teacher t)
                        limit = t.BorrowLimit;

                    if (borrowedBooks.Count >= limit)
                    {
                        MessageBox.Show($"{user.Name} has reached their borrowing limit.");
                        return;
                    }

                    borrowedBooks.Add(book);
                }

                book.Borrow(user);
                dgvBooks.Refresh();
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count > 0)
            {
                var book = (Book)dgvBooks.SelectedRows[0].DataBoundItem;
                var user = (User)cboUsers.SelectedItem;

                book.Return(user);

                if (!(user is Librarian) && borrowedBooksByUser.ContainsKey(user.UserId))
                {
                    borrowedBooksByUser[user.UserId].Remove(book);
                }

                dgvBooks.Refresh();
            }
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {
            AddBookForm form = new AddBookForm();

            if (form.ShowDialog() == DialogResult.OK)
            {
                Book newBook = new Book
                {
                    Title = form.BookTitle,
                    Author = form.BookAuthor
                };

                books.Add(newBook);
                dgvBooks.DataSource = null;
                dgvBooks.DataSource = books;

                MessageBox.Show($"{newBook.Title} by {newBook.Author} has been added successfully.");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagementBL
{
    public class Book : IBorrowable
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; } = true;
        public override string ToString()
        {
            return $"{Title} by {Author}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Book other = (Book)obj;
            return Title == other.Title && Author == other.Author;
        }
        public override int GetHashCode()
        {
            return (Title + Author).GetHashCode();
        }

        public void Borrow(User user)
        {
            IsAvailable = false;
            MessageBox.Show($"{user.Name} has borrowed {Title}");
        }
        public void Return(User user)
        {
            IsAvailable = true;
            MessageBox.Show($"{user.Name} has returned {Title}");
        }

        public static bool operator ==(Book book1, Book book2)
        {
            if (ReferenceEquals(book1, book2))
                return true;
            if (book1 is null || book2 is null)
                return false;
            return book1.Equals(book2);
        }

        public static bool operator !=(Book book1, Book book2)
        {
            return !(book1 == book2);
        }
    }
}

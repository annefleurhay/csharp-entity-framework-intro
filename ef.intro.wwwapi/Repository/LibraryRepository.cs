using ef.intro.wwwapi.Context;
using ef.intro.wwwapi.Models;
using Microsoft.EntityFrameworkCore;

namespace ef.intro.wwwapi.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        public bool AddAuthor(Author author)
        {
            using(var db = new LibraryContext())
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return true;
            }

            return false;
        }
        public bool AddBook(Book book)
        {
            using (var db = new LibraryContext())
            {
                db.Books.Add(book);
                db.SaveChanges();
                return true;
            };
            return false;
        }


        public bool AddPublisher(Publisher publisher)
        {
            using (var db = new LibraryContext())
            {
                db.Publishers.Add(publisher);
                db.SaveChanges();
                return true;
            };
            return false;
        }

        public bool DeleteAuthor(int id)
        {
            using (var db = new LibraryContext())
            {
                var target = db.Authors.FirstOrDefault(a => a.Id == id);
                if (target != null)
                {
                    db.Remove(target);
                    return true;
                }
            };
            return false;
        }
        public bool DeleteBook(int id)
        {
            using (var db = new LibraryContext())
            {
                var target = db.Books.FirstOrDefault(b => b.Id == id);
                if (target != null)
                {
                    db.Books.Remove(target);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public bool DeletePublisher(int id)
        {
            using (var db = new LibraryContext())
            {
                var target = db.Publishers.FirstOrDefault(p => p.Id == id);
                if (target != null)
                {
                    db.Publishers.Remove(target);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }
        public IEnumerable<Author> GetAllAuthors()
        {
            using (var db = new LibraryContext())
            {
                return db.Authors.Include(a => a.Books).ThenInclude(b => b.Publisher).ToList();
            }
            return null;
        }
        public IEnumerable<Book> GetAllBooks()
        {
            using (var db = new LibraryContext())
            {
                return db.Books.Include(b => b.Publisher).ToList();
            }
            return null;
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            using (var db = new LibraryContext())
            {
                return db.Publishers.ToList();
            }
            return null;
        }
        public Author GetAuthor(int id)
        {
            Author result;
            using (var db = new LibraryContext())
            {
                result = db.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
                return result;                
            };
            return result;

        }
        public Publisher GetPublisher(int id)
        {
            Publisher result;
            using(var db = new LibraryContext())
            {
                result = db.Publishers.FirstOrDefault(p => p.Id == id);
                return result;
            };

        }
        public Book GetBook(int id)
        {
            Book result;
            using (var db = new LibraryContext())
            {
                result = db.Books.Include(p => p.Publisher).FirstOrDefault(b => b.Id == id);
                return result;         
            };
            return result;
        }
        public bool UpdateAuthor(Author author)
        {
            using (var db = new LibraryContext())
            {
                var existingAuthor = db.Authors.FirstOrDefault(a => a.Id == author.Id);
                if (existingAuthor != null)
                {
                    
                    existingAuthor.FirstName = author.FirstName;
                    existingAuthor.LastName = author.LastName;
                    existingAuthor.Email = author.Email;

                    db.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool UpdatePublisher(Publisher publisher)
        {
            using (var db = new LibraryContext())
            {
                var existingPublisher = db.Publishers.FirstOrDefault(p => p.Id==publisher.Id);
                if (existingPublisher != null)
                {
                    existingPublisher.Name = publisher.Name;
                    db.SaveChanges(true);
                    return true;
                }
            }
            return false;
        }
        public bool UpdateBook(Book book)
        {
            using (var db = new LibraryContext())
            {
                var existingBook = db.Books.FirstOrDefault(b => b.Id == book.Id);
                if (existingBook != null)
                {
                   
                    existingBook.Title = book.Title;
                    existingBook.AuthorId = book.AuthorId; 

                    db.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}

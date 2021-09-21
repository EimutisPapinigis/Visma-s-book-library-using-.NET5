using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ConsoleApp1
{
    public class Book
    {
        public string Name { get; set; }
        public string Author { get; set; }
        public string Category { get; set; }
        public string Language { get; set; }
        public DateTime Publication_Date { get; set; }
        public string ISBN { get; set; }
        public Book (string name, string author, string category, string language, DateTime publication_Date, string isbn)
        {
            Name = name;
            Author = author;
            Category = category;
            Language = language;
            Publication_Date = publication_Date;
            ISBN = isbn;
        }
        public Book()
        {

        }
        public override string ToString()
        {
            return string.Format(Name + " " + Author + " " + Category + " " + Language + " " + Publication_Date.Year + " " + ISBN);
        }
        public string ToString2()
        {
            return string.Format(Name + ";" + Author + ";" + Category + ";" + Language + ";" + Publication_Date.Year + ";" + ISBN);
        }
    }
    public class Borrow
    {
        public string ISBN { get; set; }//book code
        public string Person_Name { get; set; }//who is borroving
        public DateTime ReturnDate { get; set; }//until when to return
        public Borrow (string isbn , string person_name, DateTime returnDate)
        {
            ISBN = isbn;
            Person_Name = person_name;
            ReturnDate = returnDate;
        }
        public Borrow()
        {

        }
        public override string ToString()
        {
            return string.Format(ISBN + " " + Person_Name + " " + ReturnDate);
        }
        public string ToString2()
        {
            return string.Format(ISBN + ";" + Person_Name + ";" + ReturnDate);
        }
    }
    class Program
    {
        public static List<Book> AllBooks = ReadFromJsonFile<List<Book>>(@"SaveAllBooks.JSON");
        public static List<Borrow> AllBorrows = ReadFromJsonFile<List<Borrow>>(@"SaveAllBorrows.JSON");
        static void Main(string[] args)
        {
            Console.WriteLine("Commands:");
            Console.WriteLine("AddBook");
            Console.WriteLine("BorrowBook");
            Console.WriteLine("ReturnBook");
            Console.WriteLine("ListBooks");
            Console.WriteLine("DeleteBooks");
            Console.WriteLine("Save");
            Console.WriteLine("Commands");
            Console.WriteLine("Exit");
            string text = "";
            while (text != "Exit")
            {
                text = Console.ReadLine();
                if (text == "AddBook")
                { 
                    AddBook();
                    Console.WriteLine("Task Completed. Awaiting next command.");
                }
                if (text == "ListBooks") {
                    ListBooks();
                    Console.WriteLine("Task Completed. Awaiting next command.");
                }
                if (text == "BorrowBook")
                {
                    BorrowBook();
                    Console.WriteLine("Task Completed. Awaiting next command.");
                }
                if (text == "ReturnBook") 
                {
                    ReturnBook();
                    Console.WriteLine("Task Completed. Awaiting next command.");
                }
                if (text == "DeleteBooks") 
                {
                    DeleteBooks();
                    Console.WriteLine("Task Completed. Awaiting next command.");
                }
                if (text == "Commands") 
                {
                    Console.WriteLine("Commands:");
                    Console.WriteLine("AddBook");
                    Console.WriteLine("ReturnBook");
                    Console.WriteLine("ListBooks");
                    Console.WriteLine("DeleteBooks");
                    Console.WriteLine("Commands");
                    Console.WriteLine("Exit");
                    Console.WriteLine("Task Completed. Awaiting next command.");
                }
                if (text == "Save")
                {
                    WriteToJsonFile(@"SaveAllBooks.JSON", AllBooks, false);
                    WriteToJsonFile(@"SaveAllBorrows.JSON", AllBorrows, false);
                    Console.WriteLine("Task Completed. Awaiting next command.");
                }
            }
        }
        /// <summary>
        /// Used to add books to AllBooks list
        /// </summary>
        public static void AddBook()
        {
            try
            {
                Console.WriteLine("Name:");
                string s1 = Console.ReadLine();
                Console.WriteLine("Author:");
                string s2 = Console.ReadLine();
                Console.WriteLine("Category:");
                string s3 = Console.ReadLine();
                Console.WriteLine("Language:");
                string s4 = Console.ReadLine();
                Console.WriteLine("Publication_Date:");
                string s5 = Console.ReadLine();
                Console.WriteLine("ISBN:");
                string s6 = Console.ReadLine();
                DateTime time = DateTime.Parse(s5);

                Console.WriteLine("Is the data correct? Yes/No");
                string ask = "Name: " + s1 + " Author: " + s2 + " Category: " + s3 + " Language: " + s4 + " Publication_Date: " + s5 + " ISBN: " + s6;
                Console.WriteLine(ask);
                string test = Console.ReadLine();
                if (test == "Yes")
                {
                    int ISBNtest = 0;
                    for (int i = 0; i < AllBooks.Count; i++)
                    {
                        if(AllBooks[i].ISBN==s6)
                        {
                            ISBNtest = 1;
                        }
                        
                    }
                    if(ISBNtest==1)
                    {
                        Console.WriteLine("This ISBN is in use already");
                        Console.WriteLine("Try again to add the book");
                    }
                    else
                    {
                        AllBooks.Add(new Book(s1, s2, s3, s4, time, s6));
                    }
                    
                }
                else
                {
                    Console.WriteLine("Try again? Yes/No");
                    string test2 = Console.ReadLine();
                    if (test2 == "Yes")
                    {
                        AddBook();
                    }
                }
            }
            catch { Console.WriteLine("Please try again. There was something wrong with given data"); }
        }
        /// <summary>
        /// List all books and lets you filter them
        /// </summary>
        public static void ListBooks()
        {
            Console.WriteLine("Name: Author: Category: Language: Publication year: ISBN:");
            AllBooks.ForEach(B => Console.WriteLine(B.ToString()));

            List<Book> filtered = new List<Book>(); 
            Console.WriteLine("Filter By:");
            Console.WriteLine("Author");
            Console.WriteLine("Category");
            Console.WriteLine("Language");
            Console.WriteLine("ISBN");
            Console.WriteLine("Name");
            Console.WriteLine("Availability");
            string text = "";
            
                text = Console.ReadLine();
            
                if (text == "Author") 
                {
                Console.WriteLine("Author:");
                text = Console.ReadLine();
                filtered=AllBooks.FindAll(x => x.Author == text);
                
                }

                if (text == "Category")
                {
                Console.WriteLine("Category:");
                text = Console.ReadLine();
                filtered = AllBooks.FindAll(x => x.Category == text);
                }

                if (text == "Language")
                {
                Console.WriteLine("Language:");
                text = Console.ReadLine();
                filtered = AllBooks.FindAll(x => x.Language == text);
                }

                if (text == "ISBN")
                {
                Console.WriteLine("ISBN:");
                text = Console.ReadLine();
                filtered = AllBooks.FindAll(x => x.ISBN == text);
                }

                if (text == "Name")
                {
                Console.WriteLine("Name:");
                text = Console.ReadLine();
                filtered = AllBooks.FindAll(x => x.Name == text);
                }

            if (text == "Availability")
            {
                Console.WriteLine("Available books");
                for (int i = 0; i < AllBooks.Count; i++)
                {
                    int test = 0;
                    for (int j = 0; j < AllBorrows.Count; j++)
                    {
                        if (AllBooks[i].ISBN == AllBorrows[j].ISBN)
                        {
                            test = 1;
                            filtered.Add(AllBooks[i]);
                        }
                    }
                    if (test == 0) 
                    {
                        Console.WriteLine(AllBooks[i].ToString());
                    }
                }
                Console.WriteLine("Not available books");
            }




            filtered.ForEach(B => Console.WriteLine(B.ToString()));
        }
        /// <summary>
        /// Borrows a book and puts the name of who borroed it in AllBorrows list
        /// </summary>
        public static void BorrowBook()
        {
            try
            {
                Console.WriteLine("Name and surname: ");
                string Name = Console.ReadLine();
                Console.WriteLine("Book's ISBN: ");
                string ISBN = Console.ReadLine();
                Console.WriteLine("Date of return: ");
                string DOR = Console.ReadLine();

                Console.WriteLine("Is the data correct? Yes/No");
                string ask = "Name and surname: " + Name + " Book's ISBN: " + ISBN + " Date of return: " + DOR;
                Console.WriteLine(ask);
                string test = Console.ReadLine();
                if (test == "Yes")
                {
                    int BorrowedBooksCount = 0;
                    for (int i = 0; i < AllBorrows.Count; i++)
                    {
                        string line = AllBorrows[i].ToString2();
                        string[] parts = line.Split(';');
                        if (parts[1].ToString() == Name)
                        {
                            BorrowedBooksCount++;
                        }
                    }
                    if (BorrowedBooksCount < 2)
                    {
                        DateTime dateTime = DateTime.UtcNow.Date;
                        TimeSpan period = DateTime.Parse(DOR).Subtract(dateTime);
                        TimeSpan Two = new TimeSpan(61, 0, 0, 0);
                        if (period < Two)
                            AllBorrows.Add(new Borrow(ISBN, Name, DateTime.Parse(DOR)));
                        else
                        {
                            Console.WriteLine("Please try again. You can only borrow for two months");
                        }
                    }
                    else { Console.WriteLine("Please try again. You can only borrow 3 books"); }
                }
                else
                {
                    Console.WriteLine("Try again? Yes/No");
                    string test2 = Console.ReadLine();
                    if (test2 == "Yes")
                    {
                        BorrowBook();
                    }
                }
            }
            catch { Console.WriteLine("Please try again. There was something wrong with given data"); }
        }
        /// <summary>
        /// Returns book by removing it from AllBorrows
        /// </summary>
        public static void ReturnBook()
        {
            try
            {
                Console.WriteLine("Name and surname: ");
                string Name = Console.ReadLine();
                Console.WriteLine("Book's ISBN: ");
                string ISBN = Console.ReadLine();
                int temp=0;//Borrower's index
                bool found_it = false;
                for (int i = 0; i < AllBorrows.Count; i++)
                {
                    string line = AllBorrows[i].ToString2();
                    string[] parts = line.Split(';');
                    if ((parts[0].ToString() == ISBN)&&(parts[1].ToString() == Name))
                    {
                        DateTime dateTime = DateTime.UtcNow.Date;
                        TimeSpan period = DateTime.Parse(dateTime.ToString()).Subtract(DateTime.Parse(parts[2].ToString()));
                        TimeSpan Two = new TimeSpan(61, 0, 0, 0);
                        if (period > Two)
                        {
                            Console.WriteLine("Thank you for finaly returning the book :D");
                            Console.WriteLine(period.ToString());
                        }
                        else { Console.WriteLine("Thank you for returning the book"); }
                        found_it = true;
                        temp = i;
                        break;
                    }
                }
                if (found_it == true)
                {
                    AllBorrows.RemoveAt(temp);
                }   
            }
            catch
            {
                Console.WriteLine("Please try again. There was something wrong with given data");
            }
        }
        /// <summary>
        /// Deletes a book from AllBooks list
        /// </summary>
        public static void DeleteBooks()
        {
            try
            {
                
                Console.WriteLine("Which book would you want to delete (Give ISBN)");
                string ISBN = Console.ReadLine();
                int temp=0;//Book's index
                bool found_it =false;
                for (int i = 0; i < AllBooks.Count; i++)
                {
                    string line = AllBooks[i].ToString2();
                    string[] parts = line.Split(';');
                    if (parts[5].ToString() == ISBN)
                    {
                        temp = i;
                        found_it = true;
                        break;
                    }
                }
                if (found_it == true)
                {
                    AllBooks.RemoveAt(temp);
                }
            }
            catch
            {
                Console.WriteLine("Please try again. There was something wrong with given data");
            }
        }
        /// <summary>
        /// Writes a list to a .JSON file
        /// </summary>
        /// <typeparam name="T">class of the object list</typeparam>
        /// <param name="filePath"></param>
        /// <param name="objectToWrite"></param>
        /// <param name="append"></param>
        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = Newtonsoft.Json.JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
        /// <summary>
        /// Reads from JSON file and returns object list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T ReadFromJsonFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                reader = new StreamReader(filePath);
                var fileContents = reader.ReadToEnd();
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(fileContents);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}

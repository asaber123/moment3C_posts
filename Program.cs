using System;
using System.Collections.Generic;
using System.IO;

using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace Posts
{
    public class GuestBook
    {
        private List<Post> posts = new List<Post>();
        public GuestBook()
        {
            if (File.Exists(@"guestbook.data") == true)
            {
                IFormatter formatter = new BinaryFormatter();
                Stream stream = new FileStream(@"guestbook.data", FileMode.Open, FileAccess.Read);

                while (stream.Position < stream.Length)
                {
                    Post objnew = (Post)formatter.Deserialize(stream);
                    posts.Add(objnew);
                }
                stream.Close();
            }
        }
        public Post addPost(Post post)
        {
            posts.Add(post);
            marshall();
            return post;
        }

        public int delPost(int index)
        {
            posts.RemoveAt(index);
            marshall();
            return index;
        }

        public List<Post> getPosts()
        {
            return posts;
        }

        private void marshall()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream(@"guestbook.data", FileMode.Create, FileAccess.Write);
            foreach (Post obj in posts)
            {
                formatter.Serialize(stream, obj);
            }
            stream.Close();
        }
    }

    [Serializable]
    public class Post
    {
        private string author;
        private string text;
        public string Author
        {
            set
            {
                this.author = value;

            }
            get { return this.author; }
        }
        public string Text
        {
            set { this.text = value; }
            get { return this.text; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            GuestBook guestbook = new GuestBook();
            int i = 0;

            while (true)
            {
                Console.Clear(); Console.CursorVisible = false;
                Console.WriteLine("Gästbok\n\n");

                Console.WriteLine("1. Lägg till inlägg");
                Console.WriteLine("2. Ta bort inlägg\n");
                Console.WriteLine("X. Avsluta\n");

                i = 0;
                foreach (Post post in guestbook.getPosts())
                {
                    Console.WriteLine("[" + i++ + "] " + post.Author + "\n" + post.Text + "\n\n");
                }

                string inp = Console.ReadLine().ToLower();
                switch (inp)
                {
                    case "1":

                        Console.CursorVisible = true;
                        Post obj = new Post();
                        string author;
                        string text;
                        do
                        {
                            Console.Write("Ange namn: ");
                            author = Console.ReadLine();
                            if (String.IsNullOrEmpty(author))
                            {
                                Console.Write("Du måste ange ett namn\n");

                            }

                        } while (String.IsNullOrEmpty(author));
                        obj.Author = author;
                        do
                        {

                            Console.Write("Skriv inlägg: ");
                            text = Console.ReadLine();
                            if (String.IsNullOrEmpty(text))
                            {
                                Console.Write("Du måste skriva någon text för ditt inlägg\n");

                            }
                        }while (String.IsNullOrEmpty(text));
                        obj.Text = text;
                        guestbook.addPost(obj);

                        break;
                    case "2":
                        Console.CursorVisible = true;
                        Console.Write("Ange index att radera: ");
                        string index = Console.ReadLine();
                        guestbook.delPost(Convert.ToInt32(index));
                        break;
                    case "x":
                        Environment.Exit(0);
                        break;
                }

            }

        }
    }
}

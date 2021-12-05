using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Formatters.Binary;


namespace Posts
{
    public class Post
    {
        private string author;
        public string Author
        {
            set { this.author = value; }
            get { return this.author; }
        }
        private string text;
        public string Text
        {
            set { this.text = value; }
            get { return this.text; }
        }
    }
    //Making a class for the posts in the gustbook. 
    public class GuestBook
    {   //Making a list to stora data

        private List<Post> posts = new List<Post>();
        private string filename = @"guestbook.json";
        //Constructor 
        public GuestBook(){
            //Checking if file exist
            if (File.Exists(@"guestbook.json") == true)
            {
                //Serialise and desirialise data to json format. 
                string jsonString = File.ReadAllText(filename);
                posts= JsonSerializer.Deserialize<List<Post>>(jsonString);
            }
        }
        //Function to add a new post
        public Post addPost(string auth, string text)
        {
            Post obj = new Post();
            obj.Author= auth;
            obj.Text = text;
            posts.Add(obj);
            //Using marshall to serilaise the data into json format. 
            marshall();
            return obj;
        }
        //Function to delete a post with an index
        public int delPost(int index)
        {
            posts.RemoveAt(index);
            marshall();
            return index;
        }
        //Function to get all posts through calling the list. 
        public List<Post> getPosts()
        {
            return posts;
        }
        //Function marshall which serialisase the list of posts
        private void marshall()
        {
            //Serialising the post to json format and save it into the list
            var jsonString = JsonSerializer.Serialize(posts);
            File.WriteAllText(filename,jsonString);

        }
    }
    //Main program
    class Program
    {
        static void Main(string[] args)
        {
            //Making an instance of the guestbook where al public methods can be used
            GuestBook guestbook = new GuestBook();
            int i = 0;

            while (true)
            {
                //clear the console and shut down cursor
                Console.Clear(); Console.CursorVisible = false;
                Console.WriteLine("Gästbok\n\n");

                //Menu option
                Console.WriteLine("1. Lägg till inlägg");
                Console.WriteLine("2. Ta bort inlägg\n");
                Console.WriteLine("3. Avsluta\n");

                //Printing out all posts and there index in the guestbook
                i = 0;
                foreach (Post post in guestbook.getPosts())
                {
                    Console.WriteLine("[" + i++ + "] " + post.Author + 
                    // "\n" + post.Text + 
                    "\n\n");
                }
                //Reading the users input. The function readkey reacs without user pressing enter. 
                int input = (int) Console.ReadKey(true).Key;
                switch (input)
                {
                    case '1':
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
                        do
                        {

                            Console.Write("Skriv inlägg: ");
                            text = Console.ReadLine();
                            if (String.IsNullOrEmpty(text))
                            {
                                Console.Write("Du måste skriva någon text för ditt inlägg\n");

                            }
                        } while (String.IsNullOrEmpty(text));
                        guestbook.addPost(author, text);
                        break;
                    case 2:
                        Console.CursorVisible = true;
                        Console.Write("Ange index att radera: ");
                        string index = Console.ReadLine();
                        guestbook.delPost(Convert.ToInt32(index));
                        break;
                    case 3:
                        Environment.Exit(0);
                        break;
                }

            }

        }
    }
}

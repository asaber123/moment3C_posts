using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
namespace Posts
{
    //Making a class  for the posts with setters and getters
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
        private string filename = @"postguestbook.json";
        //Constructor 
        public GuestBook(){
            //Checking if file exist
            if (File.Exists(@"postguestbook.json") == true)
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
                    Console.WriteLine("[" + i++ + "] " + post.Author + "\n" + post.Text + "\n\n");
                }
                //Reading the users input. The function readkey reacs without user pressing enter. 
                int input = (int) Console.ReadKey(true).Key;
                switch (input)
                {
                    case '1': //Case one, a new post is set
                        Console.CursorVisible = true;
                        Post obj = new Post();
                        string author;
                        string text;
                        do //Making a do while loop that runns until the user have set an input with name
                        {
                            Console.Write("Ange namn: ");
                            author = Console.ReadLine();
                            if (String.IsNullOrEmpty(author))
                            {
                                Console.Write("Du måste ange ett namn\n");

                            }

                        } while (String.IsNullOrEmpty(author));
                        do//Making a do while loop that runns until the user have set an input with a text
                        {

                            Console.Write("Skriv inlägg: ");
                            text = Console.ReadLine();
                            if (String.IsNullOrEmpty(text))
                            {
                                Console.Write("Du måste skriva någon text för ditt inlägg\n");

                            }
                        } while (String.IsNullOrEmpty(text));
                        //Then sending data into the function addPost
                        guestbook.addPost(author, text);
                        break;
                    case '2':
                        //Case 2, user choose an index of the post to delete
                        Console.CursorVisible = true;
                        Console.Write("Ange index att radera: ");
                        string index = Console.ReadLine();
                        //Converting users inout uínto an interger and then send it into the function delete post
                        guestbook.delPost(Convert.ToInt32(index));
                        break;
                    case '3':
                    //Exit the terminal
                        Environment.Exit(0);
                        break;
                }

            }

        }
    }
}

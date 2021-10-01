using System;
using System.IO;
using NLog.Web;

namespace MovieLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory() + "\\nlog.config";
            var logger = NLog.Web.NLogBuilder.ConfigureNLog(path).GetCurrentClassLogger();

            logger.Info("Program started");

            string file = "movies.csv";
            string choice;

            Console.WriteLine("Please make a selection:");
            Console.WriteLine("1. View movies");
            Console.WriteLine("2. Add movie");
            Console.WriteLine("Any other key to EXIT.");
            choice = Console.ReadLine();

            if(choice == "1")
            {
                if(File.Exists(file))
                {
                    StreamReader sr = new StreamReader(file);
                    while(!sr.EndOfStream)
                    {
                        string movie = sr.ReadLine();
                        Console.WriteLine(movie);
                    }
                    sr.Close();
                }
                else
                {
                    logger.Error("File does not exist.");
                }
            }
            else if(choice == "2")
            {
                //used to get last movie ID in file so a new one can auto generate
                StreamReader sr = new StreamReader(file);
                string lastMovie = "";
                string[] lastSplit;
                int lastID;
                int newID;
                
                while(!sr.EndOfStream)
                {
                    lastMovie = sr.ReadLine();
                }
                
                lastSplit = lastMovie.Split(',');
                if(int.TryParse(lastSplit[0], out lastID))
                {
                        //adds 1 to previous ID in file for new movie being added
                        newID = lastID + 1;
                }
                else
                {
                    logger.Error("File has unknown value.");
                }

                StreamWriter sw = new StreamWriter(file, true);

                Console.WriteLine("Please enter the movie title:");
                string moveTitle = Console.ReadLine();

                sr.Close();
                sw.Close();
            }

            logger.Info("Program ended");
        }
    }
}

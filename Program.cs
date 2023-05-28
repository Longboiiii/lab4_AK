using System.Linq;

namespace lab4_AK_Semidotskyi
{
  using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
  
  class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to the File Counter program!");

        string input = string.Empty;

        while (true)
        {
            List<string> patterns = new List<string>();
            int counter = 0;

            Console.WriteLine("\nEnter 'help' for instructions");
            Console.Write("Enter a directory path: ");
            input = Console.ReadLine();

            if (input.ToLower() == "help")
            {
                PrintHelp();
                continue;
            }

            if (Directory.Exists(input))
            {
                Console.Write("Enter file extension(s) to search (separated by comma): ");
                string extensionsInput = Console.ReadLine();
                foreach (string elem in extensionsInput.Split(','))
                {
                    patterns.Add(elem);
                }
                foreach (string pattern in patterns)
                {
                    CountFiles(input , pattern , ref counter );
                }
                Console.WriteLine($"\nTotal number of files: {counter}\n");
                break;
            }
            Console.WriteLine("Directory does not exist!");
            break;
        }

        Console.WriteLine("Program ended with code 1. Press any key to exit...");
        Console.ReadKey();
    }

    static void CountFiles(string directory, string searchPattern, ref int counter )
    {
        foreach (string dir in Directory.GetDirectories(directory))
        {
            foreach (string file in Directory.GetFiles(dir,$"*{searchPattern}"))
            {
                    FileAttributes fileAttr = File.GetAttributes(file);
                    if ((fileAttr & FileAttributes.Hidden) != 0
                        //|| (fileAttr & FileAttributes.Archive) != 0
                        || (fileAttr & FileAttributes.ReadOnly) != 0)
                    {
                        continue;
                    }
                    string name = Path.GetFileName(file);
                    Console.WriteLine(name);
                    counter++;
            }

            CountFiles(dir, searchPattern ,ref counter);
        }
    }

    static void PrintHelp()
    {
        Console.WriteLine("\nFile Counter Help:");
        Console.WriteLine("- Enter a valid directory path to count files in its subdirectories.");
        Console.WriteLine("- Specify file extensions to search for ('.txt', '.docx').");
        Console.WriteLine("- Multiple extensions can be provided, separated by commas.");
        Console.WriteLine("- Hidden, read-only files will be ignored.");
    }
}
  
}
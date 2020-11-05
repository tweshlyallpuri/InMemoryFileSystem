using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace InMemoryFileSystem
{
    class Program
    {
        static Directory _currentDirectory = null;
        static void Main(string[] args)
        {
            bool exitflag = false;
            Directory root = new Directory() { Name = "root" };//parentdirectory null
            _currentDirectory = root;
            while (!exitflag)
            {
                Console.Write(_currentDirectory.GetPath() + ">>");
                string input = Console.ReadLine();
                input = input.Trim();
                string[] inputArray = input.Split(' ');
                string command = inputArray[0].ToLower();
                switch (command)
                {
                    case "help":
                        PrintHelpStatement();
                        break;
                    case "exit":
                        exitflag = true;
                        break;
                    case "md":
                        if (inputArray.Count() > 1)
                        {
                            if (_currentDirectory.AddChildDirectory(inputArray[1]))
                                PrintOnConsoleForCurrentDirectory("Directory Created");
                            else
                                PrintOnConsoleForCurrentDirectory("Directory could not be created");
                        }
                        else
                        {
                            Console.WriteLine("Error : DirectoryName not provided");
                        }
                        break;
                    case "cd":
                        if (inputArray.Count() > 1)
                        {
                            if (!inputArray[1].Contains(".") && _currentDirectory.ChildrenDirectories != null && _currentDirectory.ChildrenDirectories.Any(x => x != null && x.Name == inputArray[1]))
                            {
                                _currentDirectory = _currentDirectory.ChildrenDirectories.First(x => x != null && x.Name == inputArray[1]);
                            }
                            else
                                PrintOnConsoleForCurrentDirectory("Could not change direcotry");
                        }
                        break;
                    case "cd..":
                        if (_currentDirectory.ParentDirectory != null)
                        {
                            _currentDirectory = _currentDirectory.ParentDirectory;
                        }
                        break;
                    case "mf":
                        if (inputArray.Count() > 1)
                        {
                            if (_currentDirectory.AddFile(inputArray[1]))
                                PrintOnConsoleForCurrentDirectory("File added!");
                            else
                                PrintOnConsoleForCurrentDirectory("File could not be added!");
                        }
                        break;
                    case "dir":
                        if (inputArray.Count() > 1 && inputArray[1] == "/s")
                        {
                            var currentDir = _currentDirectory;
                            if(currentDir.ChildrenDirectories!=null)
                            {
                                for(int i=0; i< currentDir.ChildrenDirectories.Length; i++)
                                {
                                    if(currentDir.ChildrenDirectories[i]!=null)
                                    {
                                        PrintContentsOfDir(currentDir.ChildrenDirectories[i]);
                                    }
                                }
                            }
                        }
                        else
                        {
                            var dirs = _currentDirectory.ChildrenDirectories?.Where(x => x != null).Select(x => x.Name);
                            if (dirs != null)
                            {
                                foreach (var dir in dirs)
                                {
                                    Console.WriteLine(dir);
                                }
                            }
                            if (_currentDirectory.Files != null)
                            {
                                foreach (var file in _currentDirectory.Files)
                                {
                                    Console.WriteLine(file);
                                }
                            }
                        }
                        break;
                }
            }

        }

        private static void PrintHelpStatement()
        {
            String helpStatement = "=====================================\nFollowing are the commands that can be used :\nhelp : Prints help menu" + "\nmd [directory name] Creates a directory, For example: md dir1" + "\ncd .. Changes the current directory to parent directory" + "\nmf [file name] Creates a file, for example: mf file.txt" + "\ndir Displays list of files and subdirectories in current directory" + "\ndir /s Displays files in specified directory and all subdirectories" + "\nexit Quits the program" + "\n=====================================";
            Console.WriteLine(helpStatement);
        }
        private static void PrintOnConsoleForCurrentDirectory(string statement)
        {
            Console.WriteLine(_currentDirectory.GetPath() + ">>" + statement);
        }
        private static void PrintContentsOfDir(Directory currentDirectory)
        {
            var dirs = currentDirectory.ChildrenDirectories?.Where(x => x != null).Select(x => x.Name);
            if (dirs != null)
            {
                foreach (var dir in dirs)
                {
                    Console.WriteLine(dir);
                }
            }
            if (_currentDirectory.Files != null)
            {
                foreach (var file in _currentDirectory.Files)
                {
                    Console.WriteLine(file);
                }
            }
        }
    }
}

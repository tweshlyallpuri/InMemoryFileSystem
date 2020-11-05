using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InMemoryFileSystem
{
    class Directory
    {
        public string Name { get; set; }
        public Directory ParentDirectory { get; set; }
        public Directory[] ChildrenDirectories { get; set; }
        public string[] Files;
        private int _numberOfChildDirectories = 0;
        private int _incrementalFactor = 2;
        private int _numberOfFiles = 0;

        public bool AddChildDirectory(string directoryName)
        {
            if (ChildrenDirectories == null)
                ChildrenDirectories = new Directory[_incrementalFactor];
            foreach(var dir in ChildrenDirectories)
            {
                if (dir?.Name == directoryName)//special character or other limit validation?
                    return false;
            }
            if(_numberOfChildDirectories < ChildrenDirectories.Length)
            {
                ChildrenDirectories.SetValue(new Directory() { Name = directoryName, ParentDirectory = this },_numberOfChildDirectories);
                _numberOfChildDirectories++;
            }
            else
            {
                var temp = ChildrenDirectories;
                ChildrenDirectories = new Directory[ChildrenDirectories.Count() + _incrementalFactor];
                temp.CopyTo(ChildrenDirectories, 0);
                ChildrenDirectories.SetValue(new Directory() { Name = directoryName, ParentDirectory = this }, _numberOfChildDirectories);
                _numberOfChildDirectories++;
            }
            return true;

        }
        public string GetPath()
        {
            string path = Name;
            var curDir = this;
            while(curDir.ParentDirectory!=null)
            {
                curDir = this.ParentDirectory;
                path = curDir.Name + "\\" + path;
            }
            return path;
        }
        public bool AddFile(string fileName)
        {
            if (Files == null)
                Files = new string[_incrementalFactor];

            if (_numberOfFiles < Files.Length)
            {
                Files.SetValue(fileName, _numberOfFiles);
                _numberOfFiles++;
            }
            else
            {
                var temp = Files;
                Files = new string[Files.Count() + _incrementalFactor];
                temp.CopyTo(Files, 0);
                Files.SetValue(fileName, _numberOfFiles);
                _numberOfFiles++;
            }
            return true;

        }
    }
}

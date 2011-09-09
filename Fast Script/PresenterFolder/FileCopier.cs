using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.PresenterFolder
{
    class FileCopier
    {
        private string _targetDir;
        private string _fileName;
        private string _sourceFile;
        private string _destFile;

        public FileCopier(string filePath, string targetDir, string targetName)
        {
            _sourceFile = filePath;
            _targetDir = targetDir;
            _fileName = System.IO.Path.GetFileName(filePath);
            _destFile = System.IO.Path.Combine(targetDir, targetName);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(_targetDir))
            {
                System.IO.Directory.CreateDirectory(_targetDir);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(_sourceFile, _destFile, true);
        }
    }
}

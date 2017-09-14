using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fast_Script.PresenterFolder
{
    class FileCopier
    {
        public FileCopier(string filePath, string targetDir, string targetName)
        {
            var fileName = System.IO.Path.GetFileName(filePath);
            var destFile = System.IO.Path.Combine(targetDir, targetName);

            // To copy a folder's contents to a new location:
            // Create a new target folder, if necessary.
            if (!System.IO.Directory.Exists(targetDir))
            {
                System.IO.Directory.CreateDirectory(targetDir);
            }

            // To copy a file to another location and 
            // overwrite the destination file if it already exists.
            System.IO.File.Copy(filePath, destFile, true);
        }
    }
}

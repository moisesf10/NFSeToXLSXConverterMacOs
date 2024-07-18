using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSeToXLSXConverterMacOs.Domain
{
    public interface IFileImport
    {
        public  Task<(bool, string)> loadFile(Stream stream);
        public IDictionary<string, string> readFile(int index = 0);
        public IEnumerable<IDictionary<string, string>> readFiles();
    }
}

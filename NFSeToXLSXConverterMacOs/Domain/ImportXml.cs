using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSeToXLSXConverterMacOs.Domain
{
    public class ImportXml : IFileImport
    {
        private string content = string.Empty;
        public async Task<(bool, string)> loadFile(Stream? stream)
        {
            

            try
            {
                content = stream?.ToString();
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }

            return (true, "");
        }

        public IDictionary<string, string> readFile(int index = 0)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, string?> readFile(string? filePath = null)
        {
            if (filePath != null)
            {
                content = System.IO.File.ReadAllText(filePath);
            }

            if (content == null)
            {
                throw new Exception("Nenhum arquivo XML foi carregado");
            }

            Dictionary<string, string?> dictData = ParseXml.parse(content);

            return dictData;
        }

        public IEnumerable<IDictionary<string, string>> readFiles()
        {
            throw new NotImplementedException();
        }
    }
}

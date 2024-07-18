using ClosedXML.Excel;
using CommunityToolkit.Maui.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSeToXLSXConverterMacOs.Domain
{
    public class Excel
    {
        private XLWorkbook workbook;
        private IXLWorksheet worksheet;
        private int index = 2;

        public Excel()
        {
           
            workbook = new XLWorkbook();
            worksheet = workbook.Worksheets.Add("Planilha 1");

        }

        public void genereteHeader(IDictionary<string, string> header)
        {
            int col = 1;
            foreach (var item in header)
            {
                worksheet.Cell(1, col).Value = item.Key;
                col++;
            }
            index = 2;
        }

        public void addRow(IDictionary<string, string?> row)
        {
            var col = 1;
            foreach (var item in row)
            {
                worksheet.Cell(index, col).Value = item.Value;
                col++;
            }
            index++;
        }

        public Stream GetStream() {
            var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        public void save(string pathFile)
        {
            workbook.SaveAs(pathFile);
        }
        public void dispose()
        {
            workbook.Dispose();
        }


    }
}

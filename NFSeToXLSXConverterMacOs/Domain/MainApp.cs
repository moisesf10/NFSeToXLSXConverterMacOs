using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Storage;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace NFSeToXLSXConverterMacOs.Domain
{
	public class MainApp
	{
        private readonly IFileSaver fileSaver;
        private readonly Excel excel;

        public MainApp(IFileSaver fileSaver, Excel excel)
        {
            this.fileSaver = fileSaver;
            this.excel = excel;
        }

        public async Task<bool> ProcessXmlFile(DateTime? inicioEmissao, DateTime? fimEmissao, 
			DateTime? inicioCompetencia, DateTime? fimCompetencia, IBrowserFile? file)
		{
            var stream = file?.OpenReadStream();

            ImportXml importXml = new ImportXml();

            ImportXml importerXml = new ImportXml();
            var row = importerXml.readFile(stream);
            excel.genereteHeader(row);
            excel.addRow(row);

            var fileSaverResult = await SaveFile(excel);
            fileSaverResult.EnsureSuccess();
            excel.dispose();
            return fileSaverResult.IsSuccessful;
        }

        public async Task<bool> ProcessZipFile(DateTime? inicioEmissao, DateTime? fimEmissao,
            DateTime? inicioCompetencia, DateTime? fimCompetencia, IBrowserFile? file)
        {

            
            var stream = file?.OpenReadStream();

            ImportZip importerZip = new ImportZip();
            await importerZip.loadFile(stream);
            var i = 0;
            foreach (var dictionary in importerZip.readFiles())
            {

                if (i == 0)
                {
                    excel.genereteHeader(dictionary);
                }
                if (inicioEmissao != null && fimEmissao != null)
                {
                    // Especifica o formato da data e hora
                    string format = "dd/MM/yyyy HH:mm:ss";
                    // Converte a string para DateTime usando ParseExact
                    DateTime dateValue = DateTime.ParseExact(dictionary["DtEmissao"], format, CultureInfo.InvariantCulture);
                    if (dateValue < inicioEmissao.Value || dateValue > fimEmissao.Value)
                    {
                        continue;
                    }
                }

                if (inicioCompetencia != null && fimCompetencia != null)
                {
                    string format = "dd/MM/yyyy";
                    // Converte a string para DateTime usando ParseExact
                    DateTime dateValue = DateTime.ParseExact(dictionary["Competencia"], format, CultureInfo.InvariantCulture);
                    if (dateValue < inicioCompetencia.Value || dateValue > fimCompetencia.Value)
                    {
                        continue;
                    }
                }

                excel.addRow(dictionary);
                i++;
            }
            importerZip.dispose();

            var fileSaverResult = await SaveFile(excel);
            fileSaverResult.EnsureSuccess();
            excel.dispose();
            return fileSaverResult.IsSuccessful;

        }

        private async Task<FileSaverResult> SaveFile(Excel excel)
        {

            Stream workbook = excel.GetStream();
            var fileSaverResult = await fileSaver.SaveAsync("Arquivo Convertido.xlsx", workbook);
            return fileSaverResult;


        }
    }
}

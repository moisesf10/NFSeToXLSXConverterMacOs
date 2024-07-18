using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFSeToXLSXConverterMacOs.Domain
{
	public static class Validate
	{
		public static (bool, DateTime?) validateDate(string date)
		{

			if (String.IsNullOrEmpty(date?.Replace("/", "")?.Trim()))
			{
				return (true, null);
			}

			try
			{
				DateTime newDate = DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
				return (true, newDate);
			}
			catch (Exception ex) { }

			return (false, null);
		}

		public static (bool, DateTime?) validateCompetencia(string competencia)
		{

			if (String.IsNullOrEmpty(competencia?.Replace("/", "")?.Trim()))
			{
				return (true, null);
			}

			try
			{

				DateTime newDate = DateTime.ParseExact(competencia, "MM/yyyy", CultureInfo.InvariantCulture);
				return (true, newDate);
			}
			catch (Exception ex) { }

			return (false, null);
		}
	}
}

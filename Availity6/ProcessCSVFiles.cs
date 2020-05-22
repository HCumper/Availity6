using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.FileIO;

namespace Availity6
{
    public class ProcessCSVFiles
    {
        private static void Main() { }
        public IList<CSVRow> ParseCSV(String filename)
        {
            IList<CSVRow> fileRows = new List<CSVRow>();
            using (TextFieldParser parser = new TextFieldParser(filename))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = false;
                parser.TrimWhiteSpace = true;

                try
                {
                    int id = 0;
                    while (parser.PeekChars(1) != null)
                    {
                        string[] elements = parser.ReadFields();
                        CSVRow newRow = new CSVRow() { UniqueId = id++, UserId = elements[0], FullName = elements[1], Version = Int32.Parse(elements[2]), InsuranceCompany = (elements.Length == 4) ? elements[3] : "" };
                        fileRows.Add(newRow);
                    }
                }
                catch
                {
                    // Bad file format handle somehow
                }
            }
            return fileRows;
        }

        public void BreakUpFeed(IList<CSVRow> rows)
        {
            // Case in names treated as significant
            var insuranceCompanies = from row in rows
                                     group row by row.InsuranceCompany into companyName
                                     select companyName;

            foreach (var company in insuranceCompanies)
            {
                // extract enrollees for each company
                var enrollees = from person in rows
                                where person.InsuranceCompany == company.Key
                                && person.Version == (
                                    (from subPerson in rows
                                     where subPerson.InsuranceCompany == company.Key
                                    && subPerson.UserId == person.UserId
                                     select subPerson.Version).Max())
                                orderby person.FullName.Split(' ')[1], person.FullName.Split(' ')[0] // assuming exactly 2 names
                                select $"{person.UserId},{person.FullName},{person.Version}";

                System.IO.File.WriteAllLines($@"..\..\..\{company.Key}.csv", enrollees); // warning: blows away any existing files
            }

        }
    }
}

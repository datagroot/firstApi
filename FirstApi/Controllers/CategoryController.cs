using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        // GET: Categories
        [HttpGet]
        public List<List<string>> GetCategories()
        {
            List<List<string>> entries = new List<List<string>>();
            using (var reader = new StreamReader("categories.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var record = new Category();
                var records = csv.EnumerateRecords(record);
                foreach (var r in records)
                {
                    var entry = new List<string>() { r.CategoryID.ToString(), r.CategoryName, r.Description };
                    entries.Add(entry);
                }
            }
            return entries;
        }

        // POST: Category/Create
        [HttpPost]
        public string PostCategory(Category categoryItem)
        {
            try
            {
                var records = new List<Category>
                {
                    new Category { CategoryID = categoryItem.CategoryID, CategoryName = categoryItem.CategoryName, Description = categoryItem.Description },
                };

                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    // Don't write the header again.
                    HasHeaderRecord = false,
                };
                using (var stream = System.IO.File.Open("categories.csv", FileMode.Append))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, config))
                {
                    csv.WriteRecords(records);
                }

                using (var writer = new StreamWriter("categories.csv"))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(records);
                }
                return "done";
            }
            catch
            {
                return "not done";
            }
        }
    }
}

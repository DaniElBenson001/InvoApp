using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoApp.Models.DtoModels
{
    public class SearchResultDTO<T>
    {
        public int Totalcount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber {  get; set; }
        public IEnumerable<T> Items { get; set; } = new List<T>();
    }
}

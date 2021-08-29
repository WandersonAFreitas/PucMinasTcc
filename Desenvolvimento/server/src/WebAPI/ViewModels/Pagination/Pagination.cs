using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.ViewModels.Pagination
{
    public class Pagination<T>
    {
        public Pagination()
        {
        }

        public Pagination(IEnumerable<T> content, int number, int size, int totalElements)
        {
            Content = content;
            Number = number;
            Size = size;
            TotalElements = totalElements;
        }

        public IEnumerable<T> Content { get; set; }
        public int Number { get; set; } = 1;
        public int Size { get; set; } = 10;
        public int TotalElements { get; set; } = 0;
        public int TotalPages
        {
            get { return Convert.ToDouble(TotalElements % Size) == 0 ? TotalElements / Size : (TotalElements / Size) + 1; ; }
        }

        //public int totalPages { get; set; }
    }
}

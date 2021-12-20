using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AngularCrudAPI.Models
{
    public class Paging<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public string OrderBy { get; set; }

        public bool OrderByDesc { get; set; }

        public string filter { get; set; }

        public List<T> Result { get; set; }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WorkSupply.Core.DTOs.Pagination
{
    public class PaginatedListDto<T>
    {
        public int PageIndex { get; private set; }

        public int TotalPages { get; private set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }

        public List<T> Items { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Order.Application.Common
{
    public class PaginatedListEx<T>
    {
        public PaginatedListEx(IReadOnlyCollection<T> items, int totalCount, int pageSize, int pageNumber)
        {
            Items = items;
            TotalCount = totalCount;
            TotalPages = (int)Math.Ceiling(totalCount/(double) pageSize);
            PageNumber = pageNumber;
        }

        public IReadOnlyCollection<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }

        public async Task<PaginatedList<T>> Create(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellation)
        {
            var count =  await source.CountAsync<T>(cancellation);
            var items = await source
                .Skip((pageSize *1) - pageSize)
                .Take(pageSize)
                .ToListAsync<T>(cancellation);

            return new PaginatedList<T>(items, count, pageNumber, pageSize);

        }

    }
}

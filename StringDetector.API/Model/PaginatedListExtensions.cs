using StringDetector.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiDoodle.Net.Http.Client.Model;

namespace StringDetector.API.Model
{
    internal static  class PaginatedListExtensions
    {
        internal static PaginatedDto<TDto> ToPaginatedDto<TDto, TEntity>(
            this PaginatedList<TEntity> source,
            IEnumerable<TDto> items) where TDto : IDto
        {

            return new PaginatedDto<TDto>
            {
                PageIndex = source.PageIndex,
                PageSize = source.PageSize,
                TotalCount = source.TotalCount,
                TotalPageCount = source.TotalPageCount,
                HasNextPage = source.HasNextPage,
                HasPreviousPage = source.HasPreviousPage,
                Items = items
            };
        }
    }
}

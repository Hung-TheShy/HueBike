﻿using Core.Extensions;
using System.Linq;

namespace Core.SeedWork
{
    public class BaseSortingService
    {
        /// <summary>
        /// Paging and sorting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="searchRequest"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        protected IQueryable<T> PagingAndSorting<T>(PagingQuery searchRequest, IQueryable<T> query)
        {
            if (searchRequest == null)
            {
                return query;
            }
            if (!string.IsNullOrEmpty(searchRequest.OrderBy) && searchRequest.GetFieldMapping().ContainsKey(searchRequest.OrderBy.ToLower()))
            {
                string sortField = searchRequest.GetFieldMapping()[searchRequest.OrderBy.ToLower()];
                query = query.OrderBy(sortField);
            }
            else if (!string.IsNullOrEmpty(searchRequest.OrderByDesc) && searchRequest.GetFieldMapping().ContainsKey(searchRequest.OrderByDesc.ToLower()))
            {
                string sortField = searchRequest.GetFieldMapping()[searchRequest.OrderByDesc.ToLower()];
                query = query.OrderByDescending(sortField);
            }
            if (searchRequest.PageSize > 0)
            {
                return query
                    .Skip((searchRequest.PageIndex - 1) * searchRequest.PageSize)
                    .Take(searchRequest.PageSize);
            }

            return query;
        }
    }
}

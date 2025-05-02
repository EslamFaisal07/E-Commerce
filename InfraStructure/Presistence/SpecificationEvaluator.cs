using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistence
{
    static class SpecificationEvaluator

    {


        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specifications) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;
            if (specifications.Criteria != null)
            {
                query = query.Where(specifications.Criteria);
            }

            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }


            if (specifications.OrderByDescending is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDescending);
            }









            if (specifications.IncludeExpressions is not null && specifications.IncludeExpressions.Count >0)
            {
              

                query= specifications.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));

            }



            if (specifications.IsPaginated)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }


            return query;
        }


    }
}

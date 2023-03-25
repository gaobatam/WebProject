using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
namespace Core.Specifications
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable <TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query= inputQuery;
            if (specification.Criteria !=null)
            {
                query = query.Where(specification.Criteria);
            }
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            return query;
        }
    }
}
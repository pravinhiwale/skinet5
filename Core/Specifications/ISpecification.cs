using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public interface ISpecification<T>
    {
         Expression<Func<T,bool>> Criteria {get;set;} //this is our where clause
         List<Expression<Func<T,object>>> Includes {get;set;} //this will be in include 
         Expression<Func<T,object>> OrderBy {get;}
         Expression<Func<T,object>> OrderByDescending {get;set;}
         int Take{get;}
         int Skip {get;}
         bool IsPagingEnabled{get;}
    }
}
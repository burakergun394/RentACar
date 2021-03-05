using RentACar.Core.Entities.Abstract;
using RentACar.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Core.Utilities.Business
{
    public static class BusinessRules 
    {
        public static IResult Run(params IResult[] results)
        {
            foreach (var result in results)
            {
                if (!result.IsSuccees)
                    return result;
            }

            return null;
        }

        public static IDataResult<T> Run<T>(params IDataResult<T>[] results) 
        {
          
            for (int i = 0; i < results.Length; i++)
            {
                if (!results[i].IsSuccees)
                    return results[i];

                if (i == results.Length - 1)
                    return results[i];   
            }

            return null;
        }
    }
}

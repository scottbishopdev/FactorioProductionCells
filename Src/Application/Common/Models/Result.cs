using System;
using System.Collections.Generic;
using System.Linq;

namespace FactorioProductionCells.Application.Common.Models
{
    // We'll use this class as a structured way of letting callers know whether or not a request was successful, along with any errors that occurred during the attempt to perform the request.
    public class Result
    {
        internal Result(Boolean succeeded, IEnumerable<String> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        public Boolean Succeeded { get; set; }

        public String[] Errors { get; set; }

        public static Result Success()
        {
            return new Result(true, new String[]{});
        }

        public static Result Failure(IEnumerable<String> errors)
        {
            return new Result(false, errors);
        }
    }
}

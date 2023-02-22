using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSH.Learn.Application.Services.ImportSheet;
public static class ValidateExtensions
{
    /// <summary>
    ///     Determines whether the specified object is valid using DataAnnotations.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="results"></param>
    /// <param name="serviceProvider"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static bool Validate(
        this object obj,
        out List<ValidationResult> results,
        IServiceProvider serviceProvider = null,
        IDictionary<object, object> items = null)
    {
        if (obj == null)
            throw new ArgumentNullException(nameof(obj));
        var context = new ValidationContext(obj, serviceProvider, items);
        results = new List<ValidationResult>();
        return Validator.TryValidateObject(obj, context, results, true);
    }
}

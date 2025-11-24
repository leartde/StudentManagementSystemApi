using System.Linq.Expressions;
using System.Reflection;
using API.Enums;
using API.Models;

namespace API.Services.QueryExtensions;

public static class SubjectExtensions
{
  public static IQueryable<Subject> Search(this IQueryable<Subject> subjects, string? searchTerm)
  {
    if (string.IsNullOrEmpty(searchTerm)) return subjects;
    var lowerSearch = searchTerm.ToLower();
    subjects = subjects.Where(s => s.Title.ToLower().Contains(lowerSearch) || s.Code.ToLower().Contains(lowerSearch));
    return subjects;
  }

  public static IQueryable<Subject> Filter(this IQueryable<Subject> subjects, int? professorId,
    FieldOfStudy? fieldOfStudy)
  {
    if (professorId != null)
    {
      subjects = subjects.Where(s => s.ProfessorId == professorId);
    }

    if (fieldOfStudy != null)
    {
      subjects = subjects.Where(s => s.FieldOfStudy == fieldOfStudy);
    }

    return subjects;
  }

  public static IQueryable<Subject> Sort(this IQueryable<Subject> subjects, string? orderByQueryString)
  {
    if (string.IsNullOrWhiteSpace(orderByQueryString)) return subjects.OrderByDescending(s => s.CreatedAt);

    string[] orderParams = orderByQueryString.Trim().Split(' ');
    string propertyName = orderParams[0];
    bool isDescending = orderByQueryString.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);

    switch (propertyName.ToLower())
    {
      case "ects":
        return isDescending
          ? subjects.OrderByDescending(s => s.ECTS)
          : subjects.OrderBy(s => s.ECTS);
      case "professor":
        return isDescending
          ? subjects.OrderByDescending(s => s.Professor)
          : subjects.OrderBy(s => s.Professor);
      default:
        var propertyInfos = typeof(Subject).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var objectProperty = propertyInfos.FirstOrDefault(pi =>
          pi.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

        if (objectProperty == null)
          throw new ArgumentException($"Invalid property name '{propertyName}'");

        var parameter = Expression.Parameter(typeof(Subject), "x");
        var propertyAccess = Expression.Property(parameter, objectProperty);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);

        string method = isDescending ? "OrderByDescending" : "OrderBy";
        var orderByCall = Expression.Call(
          typeof(Queryable),
          method,
          new[] { typeof(Subject), objectProperty.PropertyType },
          subjects.Expression,
          Expression.Quote(orderByExp));

        return subjects.Provider.CreateQuery<Subject>(orderByCall);
    }
  }
}

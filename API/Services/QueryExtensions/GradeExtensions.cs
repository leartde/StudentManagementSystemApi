using System.Linq.Expressions;
using System.Reflection;
using API.Models;

namespace API.Services.QueryExtensions;

public static class GradeExtensions
{
  public static IQueryable<Grade> Filter(this IQueryable<Grade> grades, int? studentId, int? subjectId)
  {
    if (studentId != null) grades = grades.Where(g => g.StudentId == studentId);

    if (subjectId != null) grades = grades.Where(g => g.SubjectId == subjectId);

    return grades;
  }

  public static IQueryable<Grade> Sort(this IQueryable<Grade> grades, string? orderByQueryString)
  {
    if (string.IsNullOrWhiteSpace(orderByQueryString)) return grades.OrderByDescending(s => s.CreatedAt);

    string[] orderParams = orderByQueryString.Trim().Split(' ');
    string propertyName = orderParams[0];
    bool isDescending = orderByQueryString.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);
    switch (propertyName.ToLower())
    {
      case "mark":

        return isDescending
          ? grades.OrderByDescending(g => g.Mark)
          : grades.OrderBy(g => g.Mark);
      case "student":
        return isDescending
          ? grades.OrderByDescending(g => g.Student)
          : grades.OrderBy(g => g.Student);
      case "subject":
        return isDescending
          ? grades.OrderByDescending(g => g.Subject)
          : grades.OrderBy(g => g.Subject);
      default:
        var propertyInfos = typeof(Grade).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var objectProperty = propertyInfos.FirstOrDefault(pi =>
          pi.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

        if (objectProperty == null)
          throw new ArgumentException($"Invalid property name '{propertyName}'");

        var parameter = Expression.Parameter(typeof(Grade), "x");
        var propertyAccess = Expression.Property(parameter, objectProperty);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);

        string method = isDescending ? "OrderByDescending" : "OrderBy";
        var orderByCall = Expression.Call(
          typeof(Queryable),
          method,
          new[] { typeof(Student), objectProperty.PropertyType },
          grades.Expression,
          Expression.Quote(orderByExp));

        return grades.Provider.CreateQuery<Grade>(orderByCall);
    }
  }
}

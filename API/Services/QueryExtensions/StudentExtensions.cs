using System.Linq.Expressions;
using System.Reflection;
using API.Models;

namespace API.Services.QueryExtensions;

public static class StudentExtensions
{
  public static IQueryable<Student> Filter(this IQueryable<Student> students, double? minAvgGrade)
  {
    if (minAvgGrade != null)
    {
      students = students.Where(s => s.Grades != null && s.Grades.Any() && s.Grades.Average(g => g.Mark) > minAvgGrade);
    }

    return students;
  }

  public static IQueryable<Student> Search(this IQueryable<Student> students, string? searchTerm)
  {
    if (!string.IsNullOrEmpty(searchTerm))
    {
      var lowerSearchTerm = searchTerm.ToLower();
      students = students.Where(s =>
        s.FirstName.ToLower().Contains(lowerSearchTerm) ||
        s.LastName.ToLower().Contains(lowerSearchTerm));
    }

    return students;
  }

  public static IQueryable<Student> Sort(this IQueryable<Student> students, string? orderByQueryString)
  {
    if (string.IsNullOrWhiteSpace(orderByQueryString))
    {
      return students.OrderByDescending(s => s.PublicId);
    }

    string[] orderParams = orderByQueryString.Trim().Split(' ');
    string propertyName = orderParams[0];
    bool isDescending = orderByQueryString.EndsWith(" desc", StringComparison.OrdinalIgnoreCase);

    switch (propertyName.ToLower())
    {
      case "averageGrade":
        return isDescending
          ? students.OrderByDescending(s => (s.Grades ?? Array.Empty<Grade>()).Average(g => g.Mark))
          : students.OrderBy(s => (s.Grades ?? Array.Empty<Grade>()).Average(g => g.Mark));
      case "firstName":
        return isDescending
          ? students.OrderByDescending(s => s.FirstName)
          : students.OrderBy(s => s.FirstName);
      case "lastName":
        return isDescending
          ? students.OrderByDescending(s => s.LastName)
          : students.OrderBy(s => s.LastName);
      case "registrationDate":
        return isDescending
          ? students.OrderByDescending(s => s.CreatedAt)
          : students.OrderBy(s => s.CreatedAt);
      default:
        PropertyInfo[] propertyInfos = typeof(Student).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        PropertyInfo? objectProperty = propertyInfos.FirstOrDefault(pi =>
          pi.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

        if (objectProperty == null)
          throw new ArgumentException($"Invalid property name '{propertyName}'");

        ParameterExpression parameter = Expression.Parameter(typeof(Student), "x");
        MemberExpression propertyAccess = Expression.Property(parameter, objectProperty);
        LambdaExpression orderByExp = Expression.Lambda(propertyAccess, parameter);

        string method = isDescending ? "OrderByDescending" : "OrderBy";
        MethodCallExpression orderByCall = Expression.Call(
          typeof(Queryable),
          method,
          new[] { typeof(Student), objectProperty.PropertyType },
          students.Expression,
          Expression.Quote(orderByExp));

        return students.Provider.CreateQuery<Student>(orderByCall);
    }
  }
}

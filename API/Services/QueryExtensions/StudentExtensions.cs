using System.Linq.Expressions;
using System.Reflection;
using API.Enums;
using API.Models;

namespace API.Services.QueryExtensions;

public static class StudentExtensions
{
  public static IQueryable<Student> Filter(this IQueryable<Student> students, double? minAvgGrade,
    FieldOfStudy? fieldOfStudy)
  {
    if (minAvgGrade != null)
      students = students.Where(s => s.Grades != null && s.Grades.Any() && s.Grades.Average(g => g.Mark) >= minAvgGrade);

    if (fieldOfStudy != null) students = students.Where(s => s.FieldOfStudy == fieldOfStudy);

    return students;
  }

  public static IQueryable<Student> Search(this IQueryable<Student> students, string? searchTerm)
  {
    if (string.IsNullOrEmpty(searchTerm)) return students;
    string lowerSearchTerm = searchTerm.ToLower();
    students = students.Where(s =>
      s.FirstName.ToLower().Contains(lowerSearchTerm) ||
      s.LastName.ToLower().Contains(lowerSearchTerm));

    return students;
  }

  public static IQueryable<Student> Sort(this IQueryable<Student> students, string? orderByQueryString)
  {
    if (string.IsNullOrWhiteSpace(orderByQueryString)) return students.OrderByDescending(s => s.PublicId);

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
        var propertyInfos = typeof(Student).GetProperties(BindingFlags.Public | BindingFlags.Instance);
        var objectProperty = propertyInfos.FirstOrDefault(pi =>
          pi.Name.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));

        if (objectProperty == null)
          throw new ArgumentException($"Invalid property name '{propertyName}'");

        var parameter = Expression.Parameter(typeof(Student), "x");
        var propertyAccess = Expression.Property(parameter, objectProperty);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);

        string method = isDescending ? "OrderByDescending" : "OrderBy";
        var orderByCall = Expression.Call(
          typeof(Queryable),
          method,
          new[] { typeof(Student), objectProperty.PropertyType },
          students.Expression,
          Expression.Quote(orderByExp));

        return students.Provider.CreateQuery<Student>(orderByCall);
    }
  }
}

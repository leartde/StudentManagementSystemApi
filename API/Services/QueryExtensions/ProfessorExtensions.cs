using API.Models;

namespace API.Services.QueryExtensions;

public static class ProfessorExtensions
{
  public static IQueryable<Professor> Search(this IQueryable<Professor> professors, string? searchTerm)
  {
    if (string.IsNullOrEmpty(searchTerm)) return professors;
    string lowerSearch = searchTerm.ToLower();
    professors = professors.Where(p =>
      p.FirstName.ToLower().Contains(lowerSearch) || p.LastName.ToLower().Contains(lowerSearch));
    return professors;
  }
}

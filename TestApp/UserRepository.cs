using TestApp.Data;
using TestApp.Data.Models;


namespace TestApp;

public class UserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context;
    }

    public User? GetUserById(Guid userId)
    {
        return _context.Users.FirstOrDefault(u => u.UserId == userId);
    }

    public IEnumerable<User> GetUsersByDomain(string domain, int page, int pageSize)
    {
        return _context.Users
            .Where(u => u.Domain == domain)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public List<User?> GetUsersByTagAndDomain(Guid tagId, string domain)
    {
        return _context.TagsToUsers
            .Where(ut => ut.User != null && ut.TagId == tagId && ut.User.Domain == domain)
            .Select(ut => ut.User)
            .Distinct()
            .ToList();
    }
}

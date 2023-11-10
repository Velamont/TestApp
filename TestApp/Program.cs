// See https://aka.ms/new-console-template for more information

using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TestApp;
using TestApp.Data;
using TestApp.Data.Models;


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();

var connectionString = configuration.GetConnectionString("DefaultConnection");

var serviceProvider = new ServiceCollection()
    .AddTransient<UserRepository>()
    .AddDbContext<UserContext>(options => options.UseSqlServer(connectionString))
    .BuildServiceProvider();

var userRepository = serviceProvider.GetService<UserRepository>();

SeedData(userRepository);

var testData = Encoding.UTF8.GetBytes("Message1\x01Message2\x01Message3");
using (var memoryStream = new MemoryStream(testData))
{
    var messageReader = new MessageReader(memoryStream, 0x01);

    var message1 = messageReader.ReadMessage();
    Console.WriteLine($"Message 1: {message1}");

    var message2 = messageReader.ReadMessage();
    Console.WriteLine($"Message 2: {message2}");

    var message3 = messageReader.ReadMessage();
    Console.WriteLine($"Message 3: {message3}");
}

var user = userRepository.GetUserById(Guid.NewGuid());
Console.WriteLine($"User: {user!.Name}");

var users = userRepository.GetUsersByDomain("example.com", 1, 10);
foreach (var u in users)
{
    Console.WriteLine($"User: {u!.Name}");
}

var usersWithTag = userRepository.GetUsersByTagAndDomain(Guid.NewGuid(), "example.com");
foreach (var u in usersWithTag)
{
    Console.WriteLine($"User: {u!.Name}");
}

static void SeedData(UserRepository userRepository)
{
    var user1 = new User { UserId = Guid.NewGuid(), Name = "User1", Domain = "example.com" };
    var user2 = new User { UserId = Guid.NewGuid(), Name = "User2", Domain = "example.com" };
    var user3 = new User { UserId = Guid.NewGuid(), Name = "User3", Domain = "example.com" };

    var tag1 = new Tag { TagId = Guid.NewGuid(), Value = "Tag1", Domain = "example.com" };
    var tag2 = new Tag { TagId = Guid.NewGuid(), Value = "Tag2", Domain = "example.com" };
    var tag3 = new Tag { TagId = Guid.NewGuid(), Value = "Tag3", Domain = "example.com" };

    var tagToUser1 = new TagToUser { UserId = user1.UserId, TagId = tag1.TagId };
    var tagToUser2 = new TagToUser { UserId = user2.UserId, TagId = tag2.TagId };
    var tagToUser3 = new TagToUser { UserId = user3.UserId, TagId = tag3.TagId };

    using (var context = new UserContext(new DbContextOptions<UserContext>()))
    {
        context.Database.EnsureCreated();

        if (!context.Users.Any())
        {
            context.Users.AddRange(user1, user2, user3);
            context.Tags.AddRange(tag1, tag2, tag3);
            context.TagsToUsers.AddRange(tagToUser1, tagToUser2, tagToUser3);

            context.SaveChanges();
        }
    }
}
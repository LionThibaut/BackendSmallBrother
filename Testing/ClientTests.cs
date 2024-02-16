using Domain;

namespace Testing;

public class ClientTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestEquals()
    {
        Client c1 = new Client
        {
            IdClient = 1
        };
        Client c2 = new Client
        {
            IdClient = 1
        };
        Assert.True(c1.Equals(c2));
    }
    
    [Test]
    public void TestNotEquals()
    {
        Client c1 = new Client
        {
            IdClient = 1
        };
        Client c2 = new Client
        {
            IdClient = 2
        };
        Assert.False(c1.Equals(c2));
    }

    [Test]
    public void TestEqualsClone()
    {
        Client c1 = new Client
        {
            IdClient = 1,
            FirstName = "FirstName",
            LastName = "LastName",
            Gender = "M",
            Mail = "test@gmail.com",
            HashedPassword = "HashedPassword",
            PhoneNumber = "0476051414",
            Town = "Town",
            RoleClient = "U",
            UrlImage = "UrlImage"
        };
        Client c2 = c1.Clone();
        Assert.True(c1.IdClient == c2.IdClient);
        Assert.True(c1.FirstName == c2.FirstName);
        Assert.True(c1.LastName == c2.LastName);
        Assert.True(c1.Gender == c2.Gender);
        Assert.True(c1.Mail == c2.Mail);
        Assert.True(c1.HashedPassword == c2.HashedPassword);
        Assert.True(c1.PhoneNumber == c2.PhoneNumber);
        Assert.True(c1.Town == c2.Town);
        Assert.True(c1.RoleClient == c2.RoleClient);
        Assert.True(c1.UrlImage == c2.UrlImage);
        Assert.False(ReferenceEquals(c1,c2));
    }
}
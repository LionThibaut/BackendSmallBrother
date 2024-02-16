using Domain;

namespace Testing;

public class PostTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestEquals()
    {
        Post p1 = new Post
        {
            IdPost = 1
        };
        Post p2 = new Post
        {
            IdPost = 1
        };
        Assert.True(p1.Equals(p2));
    }
    
    [Test]
    public void TestNotEquals()
    {
        Post p1 = new Post
        {
            IdPost = 1
        };
        Post p2 = new Post
        {
            IdPost = 2
        };
        Assert.False(p1.Equals(p2));
    }
    
    [Test]
    public void TestEqualsClone()
    {
        Post p1 = new Post
        {
            IdPost = 1,
            DatePost = "2022-01-01",
            NbAlert = 0,
            TownDisparition = "TownDisparition",
            DescriptionPost = "DescriptionPost",
            UrlImage = "UrlImage",
            AnimalPost = new Animal
            {
                ClientAnimal = new Client()
            }
        };
        Post p2 = p1.Clone();
        Assert.True(p1.IdPost == p2.IdPost);
        Assert.True(p1.DatePost == p2.DatePost);
        Assert.True(p1.NbAlert == p2.NbAlert);
        Assert.True(p1.TownDisparition == p2.TownDisparition);
        Assert.True(p1.DescriptionPost == p2.DescriptionPost);
        Assert.True(p1.UrlImage == p2.UrlImage);
        Assert.False(ReferenceEquals(p1, p2));
    }
}
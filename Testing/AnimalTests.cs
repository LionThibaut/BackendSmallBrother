using Domain;

namespace Testing;

public class AnimalTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void TestEquals()
    {
        Animal a1 = new Animal
        {
            IdAnimal = 1
        };
        Animal a2 = new Animal
        {
            IdAnimal = 1
        };
        Assert.True(a1.Equals(a2));
    }
    
    [Test]
    public void TestNotEquals()
    {
        Animal a1 = new Animal
        {
            IdAnimal = 1
        };
        Animal a2 = new Animal
        {
            IdAnimal = 2
        };
        Assert.False(a1.Equals(a2));
    }
    
    [Test]
    public void TestEqualsClone()
    {
        Animal a1 = new Animal
        {
            IdAnimal = 1,
            NameAnimal = "NameAnimal",
            Breed = "Breed",
            Tag = true,
            Birthdate = "2020-02-02",
            DescriptionAnimal = "DescriptionAnimal",
            Height = "30-50",
            Gender = "F",
            TypeAnimal = "TypeAnimal",
            StatutAnimal = "P",
            UrlImage = "UrlImage",
            ClientAnimal = new Client()
        };
        Animal a2 = a1.Clone();
        Assert.True(a1.IdAnimal == a2.IdAnimal);
        Assert.True(a1.NameAnimal == a2.NameAnimal);
        Assert.True(a1.Breed == a2.Breed);
        Assert.True(a1.Tag == a2.Tag);
        Assert.True(a1.Birthdate == a2.Birthdate);
        Assert.True(a1.DescriptionAnimal == a2.DescriptionAnimal);
        Assert.True(a1.Height == a2.Height);
        Assert.True(a1.Gender == a2.Gender);
        Assert.True(a1.TypeAnimal == a2.TypeAnimal);
        Assert.True(a1.StatutAnimal == a2.StatutAnimal);
        Assert.True(a1.UrlImage == a2.UrlImage);
        Assert.False(ReferenceEquals(a1, a2));
    }
}
namespace Domain;

public class Post
{
    public int IdPost { get; set; }
    public string DatePost { get; set; }
    public int NbAlert { get; set; }
    public string TownDisparition { get; set; }
    public string DescriptionPost { get; set; }
    public string? UrlImage { get; set; }
    
    public Animal AnimalPost { get; set; }
    
    public bool Equals(Post post)
    {
        return IdPost == post.IdPost;
    }

    public Post Clone()
    {
        return new Post
        {
            IdPost = IdPost,
            DatePost = DatePost,
            NbAlert = NbAlert,
            TownDisparition = TownDisparition,
            DescriptionPost = DescriptionPost,
            UrlImage = UrlImage,
            AnimalPost = AnimalPost.Clone()
        };
    }
}
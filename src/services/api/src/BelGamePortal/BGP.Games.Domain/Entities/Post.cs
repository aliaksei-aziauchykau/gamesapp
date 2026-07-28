



using System.ComponentModel.DataAnnotations;
using BGP.Shared.Libs.Domain.Entities;

namespace BGP.Games.Domain.Entities;

public class Post : Entity<Guid>
{
    [Key]
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public Guid? ParentPostId { get; set; }

    public Post? ParentPost { get; set; }

    public ICollection<Post> ChildPosts { get; set; } = new List<Post>();
}

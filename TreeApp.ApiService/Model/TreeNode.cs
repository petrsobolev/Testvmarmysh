using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TreeApp.ApiService.Model;

public class TreeNode
{
    public TreeNode(string name
        , TreeNode parent
        , TreeNode root)
    {
        Name = name;
        Parent = parent;
        Root = root;
        _children = new List<TreeNode>();
    }
    
    public TreeNode(string name)
    {
        Name = name;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; }

    public string Name { get; set; }

    public TreeNode? Parent { get; init; }

    public TreeNode? Root { get; }

    public ICollection<TreeNode> Children => _children;

    private List<TreeNode> _children;
}

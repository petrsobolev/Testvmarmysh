using System.Text.Json.Serialization;

namespace TreeApp.ApiService.Model.Dto;

public class NodeDto
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("children")]
    public ICollection<TreeNode> Children { get; set; }
}
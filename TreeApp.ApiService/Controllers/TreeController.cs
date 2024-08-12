using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TreeApp.ApiService.Model;
using TreeApp.ApiService.Model.Dto;
using TreeApp.ApiService.Services.Tree;

namespace TreeApp.ApiService.Controllers;

[ApiController]
public class TreeController : Controller
{
    private readonly ITreeService _treeService;

    public TreeController(ITreeService treeService)
    {
        _treeService = treeService;
    }

    [HttpPost("api.user.tree.get")]
    public async Task<ActionResult<NodeDto>> GetTree([Required]string treeName, CancellationToken cancellationToken)
    {
        var tree = await _treeService.GetTreeAsync(treeName, cancellationToken);

        return Ok(new NodeDto
        {
            Id = tree.Id,
            Name = tree.Name,
            Children = tree.Children?.Count > 0 ? tree.Children : new List<TreeNode>()
        });
    }

    [HttpPost("api.user.tree.node.create")]
    public async Task<ActionResult<NodeDto>> CreateNode(
        [Required]string treeName
        , [Required]int parentNodeId
        , [Required]string nodeName
        , [Required]CancellationToken cancellationToken)
    {
        var newNode = await _treeService.CreateNodeAsync(treeName, parentNodeId, nodeName, cancellationToken);
        return Ok(new NodeDto()
        {
            Id = newNode.Id,
            Name = newNode.Name,
            Children = newNode.Children?.Count > 0 ? newNode.Children : new List<TreeNode>()
        });
    }

    [HttpPost("api.user.tree.node.delete")]
    public async Task<ActionResult> DeleteNodeAsync(
        [Required]string treeName
        , [Required]int nodeId
        , CancellationToken cancellationToken)
    {
        await _treeService.DeleteNodeAsync(treeName, nodeId, cancellationToken);
        return Ok();
    }

    [HttpPost("api.user.tree.node.rename")]
    public async Task<ActionResult> RenameNodeAsync(
        [Required]string treeName
        , [Required]int nodeId
        , [Required]string newNodeName
        , CancellationToken cancellationToken)
    {
        await _treeService.RenameNodeAsync(treeName, nodeId, newNodeName, cancellationToken);
        return Ok();
    }
    
    
}
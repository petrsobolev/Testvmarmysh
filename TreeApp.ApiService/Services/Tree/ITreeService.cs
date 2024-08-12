using TreeApp.ApiService.Model;

namespace TreeApp.ApiService.Services.Tree;

public interface ITreeService
{
    Task<TreeNode> GetTreeAsync(string name, CancellationToken cancellationToken);
    Task<TreeNode> CreateNodeAsync(string treeName, int parentNodeId, string nodeName, CancellationToken cancellationToken);
    Task DeleteNodeAsync(string treeName, int nodeId, CancellationToken cancellationToken);
    Task RenameNodeAsync(string treeName, int nodeId, string newNodeName, CancellationToken cancellationToken);
}
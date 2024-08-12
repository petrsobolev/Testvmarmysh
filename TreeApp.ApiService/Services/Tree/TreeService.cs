using System.Data;
using Microsoft.EntityFrameworkCore;
using TreeApp.ApiService.Exceptions;
using TreeApp.ApiService.Extensions;
using TreeApp.ApiService.Infrastructure;
using TreeApp.ApiService.Model;

namespace TreeApp.ApiService.Services.Tree;

public class TreeService : ITreeService
{
    private readonly TreeAppContext _context;

    public TreeService(TreeAppContext context)
    {
        _context = context;
    }

    public async Task<TreeNode> GetTreeAsync(string name, CancellationToken cancellationToken)
    {
        var root = await _context.TreeNodes
            .AsNoTracking<TreeNode>()
            .FirstOrDefaultAsync(f => f.Name == name && f.Parent == null, cancellationToken: cancellationToken);

        if (root is null)
        {
            var newTreeRootNode = new TreeNode(name);
            await _context.TreeNodes.AddAsync(newTreeRootNode, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
//todo: add try catch 
            return newTreeRootNode;
        }

        return root;
    }

    public async Task<TreeNode> CreateNodeAsync(string treeName, int parentNodeId, string nodeName, CancellationToken cancellationToken)
    {
        //parent could be root
        var rootNode = await _context.TreeNodes
            .AsNoTracking<TreeNode>()
            .FirstOrDefaultAsync(f => f.Root == null && f.Name == treeName, cancellationToken);
        
        if (rootNode is null)
            throw new RootNodeNameDoesNotExistException(ErrorMessages.GetRootNameDoesNotExistErrorMessage(treeName));

        var parentNode = await _context.TreeNodes
            .Include(treeNode => treeNode.Children)
            .Include(treeNode => treeNode.Root)
            .FirstOrDefaultAsync(f => f.Id == parentNodeId, cancellationToken);
        
        if (parentNode is null || parentNode.Root.Name != treeName)
            throw new NotFoundNodeException(ErrorMessages.GetNotFoundNodeByIdErrorMessage(parentNodeId));

        //if parent is root must cheeck root for null
        if (parentNode.Root is null ?  parentNode.Id != rootNode.Id : parentNode.Root.Id != rootNode.Id)
            throw new NotFoundNodeException(ErrorMessages.GetNodeWithWrongRootErrorMessage);

        if (parentNode.Children.Any(a => a.Name == nodeName))
            throw new DuplicateNameException(ErrorMessages.GetDuplicateChildErrorMessage(nodeName));

        var newNode = new TreeNode(nodeName, parentNode, rootNode);
        await _context.TreeNodes.AddAsync(newNode, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return newNode;
    }
    
    public async Task DeleteNodeAsync(string treeName, int nodeId, CancellationToken cancellationToken)
    {
        var node = await _context.TreeNodes
            .Include(treeNode => treeNode.Root)
            .FirstOrDefaultAsync(f => f.Id == nodeId, cancellationToken);

        if (node is null || node.Root?.Name != treeName)
        {
            throw new NotFoundNodeException(ErrorMessages.GetNotFoundNodeByIdErrorMessage(nodeId));
        }

        _context.TreeNodes.Remove(node);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RenameNodeAsync(string treeName, int nodeId, string newNodeName, CancellationToken cancellationToken)
    {
        var node = await _context.TreeNodes
            .Include(treeNode => treeNode.Root)
            .Include(treeNode => treeNode.Parent)
            .FirstOrDefaultAsync(f => f.Id == nodeId, cancellationToken);

        if (node is null || node.Root?.Name != treeName)
        {
            throw new NotFoundNodeException(ErrorMessages.GetNotFoundNodeByIdErrorMessage(nodeId));
        }

        if (node.Parent.Children.Any(a => a.Name == newNodeName))
        {
            throw new DuplicateNameException(ErrorMessages.GetDuplicateChildErrorMessage(newNodeName));
        }

        node.Name = newNodeName;
        _context.TreeNodes.Update(node);
        await _context.SaveChangesAsync(cancellationToken);
    }

}
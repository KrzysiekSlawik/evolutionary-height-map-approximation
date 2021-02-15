using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeStructure
{
    public float reward;
    public static float[] safe = { 0, 0, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };
    public HMapGen root;
    /// <summary>
    /// 'a' won't be modified
    /// </summary>
    /// <param name="a">won't be modified</param>
    /// <returns>new mutated version of TreeStructure 'a'</returns>
    public static TreeStructure Mutate(TreeStructure a)
    {
        TreeStructure mutatedTree = new TreeStructure();
        mutatedTree.root = a.root.Mutate();
        return mutatedTree;
    }
    /// <summary>
    /// 'a' and 'b' won't be modified NOT FINISHED YET
    /// </summary>
    /// <param name="a">won't be modified</param>
    /// <param name="b">won't be modified</param>
    /// <returns>random combination of two trees</returns>
    public static TreeStructure CrossTrees(TreeStructure a, TreeStructure b)
    {
        TreeStructure crossedTree = new TreeStructure();
        crossedTree.root = a.root.Copy();
        HMapGen pasteHere = crossedTree.root;
        HMapGen pasteParent = null;
        HMapGen cutFrom = b.root;
        float rand;
        //choose paste target
        while(!IsLeaf(pasteHere))
        {
            rand = Random.Range(0f, 1f);
            //go A
            if(rand < 0.33f)
            {
                pasteParent = pasteHere;
                (pasteHere as BinOpHMap).a = (pasteHere as BinOpHMap).a.Copy();
                pasteHere = (pasteHere as BinOpHMap).a;
                continue;
            }
            //go B
            if(rand < 0.66f)
            {
                pasteParent = pasteHere;
                (pasteHere as BinOpHMap).b = (pasteHere as BinOpHMap).b.Copy();
                pasteHere = (pasteHere as BinOpHMap).b;
                continue;
            }
            //stay here
            break;
        }
        //choose sub tree of b to copy from
        while (!IsLeaf(cutFrom))
        {
            rand = Random.Range(0f, 1f);
            //go A
            if (rand < 0.33f)
            {
                cutFrom = (cutFrom as BinOpHMap).a;
                continue;
            }
            //go B
            if (rand < 0.66f)
            {
                cutFrom = (cutFrom as BinOpHMap).b;
                continue;
            }
            //stay here
            break;
        }
        //perform paste
        //leaf case
        if(IsLeaf(pasteHere))
        {
            if(pasteParent == null)
            {
                crossedTree.root = cutFrom;
                return crossedTree;
            }
            pasteHere = pasteParent;
        }
        rand = Random.Range(0f, 1f);
        if(rand > 0.5f)
        {
            (pasteHere as BinOpHMap).a = cutFrom;
            return crossedTree;
        }
        (pasteHere as BinOpHMap).b = cutFrom;
        return crossedTree;
    }
    private static bool IsLeaf(HMapGen node)
    {
        return !typeof(BinOpHMap).IsAssignableFrom(node.GetType());
    }
    public static int TreeDepth(HMapGen node)
    {
        if (IsLeaf(node)) return 1;
        BinOpHMap op = node as BinOpHMap;
        return 1 + TreeDepth(op.a) + TreeDepth(op.b);
    }

    public static HMapGen MakeRandomNode(int depth)
    {
        if(depth <= 1)
        {
            return MakeRandomLeaf();//leaf
        }
        return MakeRandomBinOp(MakeRandomNode(Random.Range(1, depth)),
                               MakeRandomNode(Random.Range(1, depth)));
    }
    public static HMapGen MakeRandomLeaf()
    {
        HMapGen leaf;
        float rand = Random.Range(0f, 1f);
        if (rand < 0.2f)
        {
            leaf = new ConstHMap();
        }
        else if (rand < 0.4f)
        {
            leaf = new ElipseHMap();
        }
        else if (rand < 0.6f)
        {
            leaf = new PerlinHMap();
        }
        else if (rand < 0.8f)
        {
            leaf = new XParamHMap();
        }
        else
        {
            leaf = new YParamHMap();
        }
        leaf.SetRandomGenes();
        return leaf;
    }
    public static HMapGen MakeRandomBinOp(HMapGen a, HMapGen b)
    {
        BinOpHMap op;
        float rand = Random.Range(0f, 1f);
        if(rand < 0.25f)
        {
            op = new SumHMap();
        }
        else if(rand < 0.5f)
        {
            op = new ProductHMap();
        }
        else if(rand < 0.75f)
        {
            op = new MinHMap();
        }
        else
        {
            op = new MaxHMap();
        }
        op.a = a;
        op.b = b;
        op.SetRandomGenes();
        return op;
    }
    
    public static TreeStructure MakeRandomTree(int depth) {
        TreeStructure tree = new TreeStructure();
        tree.root = MakeRandomNode(depth);
        return tree;
    }

    public override string ToString() {
        return root.ToString();
    }
}

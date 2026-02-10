using System.Collections.Generic;

using TreeEditor;

using UnityEngine;

public class TreeCreator : MonoBehaviour
{
    public GameObject TreePrefab;
    public GameObject TreeHolder;

    public int TreeCount;

    public GameObject TerrainHolder;

    private List<Terrain> Terrains;

    private TreeData TreeData;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //foreach (GameObject child in TerrainHolder.transform)
        //{
        //    if (child.GetComponent<Terrain>() != null)
        //    {
        //        Terrains.Add(child.GetComponent<Terrain>());
        //    }
        //}

        for (var i = 0; i < TreeCount; i++)
        {
            var treePosition = GetTerrainPosition();
            CreateTree(treePosition);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private Vector3 GetTerrainPosition()
    {
        var x = Random.Range(0, 500);
        var z = Random.Range(0, 500);
        var treePos = new Vector3(x, 0.0f, z);
        var height = Terrain.activeTerrain.SampleHeight(treePos);
        treePos.y = height;
        return treePos;
    }

    private void CreateTree(Vector3 treePosition)
    {
        //var treeObject = Instantiate(TreePrefab, TreeHolder.transform);
        var treeObject = new GameObject();

        treeObject.transform.position = treePosition;

        treeObject.AddComponent<Tree>();

        var tree = treeObject.GetComponent<Tree>();
        TreeData = tree.data as TreeData;

        TreeData.root.seed = Random.Range(10, 500000);
        TreeData.root.rootSpread = 5;

        var branch1 = TreeData.AddGroup(TreeData.GetGroup(TreeData.root.uniqueID), typeof(TreeGroupBranch));
        
        var branch2 = TreeData.AddGroup(TreeData.GetGroup(branch1.uniqueID), typeof(TreeGroupBranch));
        branch2.distributionFrequency = 20;
        branch2.distributionMode = TreeGroup.DistributionMode.Whorled;

        var leaves = TreeData.AddGroup(TreeData.GetGroup(branch2.uniqueID), typeof(TreeGroupLeaf));
        leaves.distributionFrequency = 30;

        TreeData.UpdateFrequency(TreeData.branchGroups[0].uniqueID);
        TreeData.UpdateFrequency(TreeData.branchGroups[1].uniqueID);
        TreeData.UpdateFrequency(TreeData.leafGroups[0].uniqueID);

        TreeData.UpdateMesh(tree.transform.worldToLocalMatrix, out var materials);

        //foreach (var mat in materials)
        //{
        //    mat.shader = Shader.Find("Universal Render Pipeline/Nature/SpeedTree8");
        //}
    }
}

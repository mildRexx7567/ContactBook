namespace ContactBook.Domain.Services;

public class DisjointSet
{
    private Dictionary<int, int> parent = new();

    public int Find(int x)
    {
        if (!parent.ContainsKey(x))
            parent[x] = x;

        if (parent[x] != x)
            parent[x] = Find(parent[x]);

        return parent[x];
    }

    public void Union(int a, int b)
    {
        int rootA = Find(a);
        int rootB = Find(b);

        if (rootA != rootB)
            parent[rootA] = rootB;
    }
}
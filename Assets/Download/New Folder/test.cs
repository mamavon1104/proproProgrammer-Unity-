using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    void Start()
    {
        int[][] getArray = TransformSparseMatrix(new int[,]
        {
            {3,0,0,0,0},
            {0,2,2,0,0},
            {0,0,0,1,3},
            {0,0,0,2,0},
            {0,0,0,0,1},
        });
        #region o—Íˆ—
        for (int i = 0; i < getArray.Length; i++)
        {
            char name = i switch
            {
                0 => 'a',
                1 => 'b',
                2 => 'c',
            };

            Debug.Log(name + " : " + string.Join(", ", getArray[i]));
        }
        #endregion
    }

    public static int[][] TransformSparseMatrix(int[,] matrix)
    {
        //––”ö‚©‚ç’Ç‰Á‚Ìˆ×list‚ğg—p
        List<int> a = new List<int>();
        List<int> b = new List<int>();
        List<int> c = new List<int>();
        for (int i = 0; i < matrix.GetLength(0); i++)
        {
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] != 0)
                {
                    a.Add(i + 1); // 1‚©‚ç‘‰Á‚Æ‚©‚¢‚¤‚«‚à‚¢•¶Í‚É‘Î‰‚·‚é‚½‚ß‚É + 1‚µ‚Ä‚²‚Ü‚©‚µ
                    b.Add(j + 1);
                    c.Add(matrix[i, j]);
                }
            }
        }
        return new int[][]{
            a.ToArray(),
            b.ToArray(),
            c.ToArray()
        };
    }
}

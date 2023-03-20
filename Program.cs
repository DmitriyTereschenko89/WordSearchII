namespace WordSearchII
{
    internal class Program
    {
        public class WordSearchII
        {
            private class TrieNode
            {
                public Dictionary<char, TrieNode> children = new();
                public string word;
            }

            private class Cell
            {
                public int row;
                public int column;

                public Cell(int row, int column)
                {
                    this.row = row;
                    this.column = column;
                }
            }

            private class SuffixTrie
            {
                public TrieNode root = new TrieNode();
                public void Push(string word)
                {
                    TrieNode node = root;
                    foreach (char letter in word)
                    {
                        if (!node.children.ContainsKey(letter))
                        {
                            node.children.Add(letter, new TrieNode());
                        }
                        node = node.children[letter];
                    }
                    node.children.Add('*', null);
                    node.word = word;
                }
            }

            private List<Cell> GetNeighbors(char[][] board, int row, int col)
            {
                List<Cell> neighbors = new List<Cell>();
                if (row > 0)
                {
                    neighbors.Add(new Cell(row - 1, col));
                }
                if (row < board.Length - 1)
                {
                    neighbors.Add(new Cell(row + 1, col));
                }
                if (col > 0)
                {
                    neighbors.Add(new Cell(row, col - 1));
                }
                if (col < board[row].Length - 1)
                {
                    neighbors.Add(new Cell(row, col + 1));
                }
                return neighbors;
            }

            private void FindWords(TrieNode node, HashSet<string> findWords, char[][] board, bool[,] visited, int row, int col)
            {
                if (visited[row, col])
                {
                    return;
                }
                if (!node.children.ContainsKey(board[row][col]))
                {
                    return;
                }
                visited[row, col] = true;
                node = node.children[board[row][col]];
                if (node.children.ContainsKey('*'))
                {
                    findWords.Add(node.word);
                }
                List<Cell> neighbors = GetNeighbors(board, row, col);
                foreach (Cell neighbor in neighbors)
                {
                    FindWords(node, findWords, board, visited, neighbor.row, neighbor.column);
                }
                visited[row, col] = false;
            }

            public IList<string> FindWords(char[][] board, string[] words)
            {
                HashSet<string> findWords = new();
                SuffixTrie suffixTrie = new();
                foreach (string word in words)
                {
                    suffixTrie.Push(word);
                }
                bool[,] visited = new bool[board.Length, board[0].Length];
                for (int row = 0; row < board.Length; row++)
                {
                    for (int col = 0; col < board[row].Length; col++)
                    {
                        FindWords(suffixTrie.root, findWords, board, visited, row, col);
                    }
                }
                return findWords.ToArray();
            }
        }
        static void Main(string[] args)
        {
            WordSearchII wordSearchII = new();
            wordSearchII.FindWords(new char[][]
            {
                new char[] { 'o', 'a', 'a', 'n' },
                new char[] { 'e', 't', 'a', 'e' },
                new char[] { 'i', 'h', 'k', 'r' },
                new char[] { 'i', 'f', 'l', 'v' }
            }, new string[] { "oath", "pea", "eat", "rain" });
            wordSearchII.FindWords(new char[][]
            {
                new char[] { 'a', 'b' }, new char[] { 'c', 'd' }
            }, new string[] { "abcd" });
        }
    }
}
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;


namespace XML_Editor
{
    /* Class Description:
     * 
     * 
     * 
     * 
     */
    internal class HuffmanTreeNode
    {
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public HuffmanTreeNode Right { get; set; }
        public HuffmanTreeNode Left { get; set; }

        public List<bool> Traverse(char symbol, List<bool> data)
        {
            // Leaf
            if (Right == null && Left == null)
            {
                if (symbol.Equals(this.Symbol))
                {
                    return data;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (Left != null)
                {
                    List<bool> leftPath = new List<bool>();
                    leftPath.AddRange(data);
                    leftPath.Add(false);

                    left = Left.Traverse(symbol, leftPath);
                }

                if (Right != null)
                {
                    List<bool> rightPath = new List<bool>();
                    rightPath.AddRange(data);
                    rightPath.Add(true);
                    right = Right.Traverse(symbol, rightPath);
                }

                if (left != null)
                {
                    return left;
                }
                else
                {
                    return right;
                }
            }
        }
    }

    internal class HuffmanTree
    {
        private List<HuffmanTreeNode> nodes = new List<HuffmanTreeNode>();
        public HuffmanTreeNode Root { get; set; }
        public Dictionary<char, int> Frequencies = new Dictionary<char, int>();

        public void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {

                if (!Frequencies.ContainsKey(source[i])) // if read the char the fisrt time, add it to the dictionary
                {
                    Frequencies.Add(source[i], 0);
                }

                Frequencies[source[i]]++; /* Advace the frequncy of the char*/
            }

            foreach (KeyValuePair<char, int> element in Frequencies)
            {
                HuffmanTreeNode n = new HuffmanTreeNode() { Symbol = element.Key, Frequency = element.Value };
                nodes.Add(n); // Create nodes that contain the symbol and its frequency
            }

            while (nodes.Count > 1)
            {
                List<HuffmanTreeNode> orderedNodes = nodes.OrderBy(node => node.Frequency).ToList<HuffmanTreeNode>();

                if (orderedNodes.Count >= 2)
                {
                    // Take first two items
                    List<HuffmanTreeNode> pair = orderedNodes.Take(2).ToList<HuffmanTreeNode>();

                    // Create a parent node by combining the frequencies
                    HuffmanTreeNode parent = new HuffmanTreeNode()
                    {
                        Symbol = '*',
                        Frequency = pair[0].Frequency + pair[1].Frequency,
                        Left = pair[0],
                        Right = pair[1]
                    };

                    nodes.Remove(pair[0]);
                    nodes.Remove(pair[1]);
                    nodes.Add(parent);
                }

                this.Root = nodes[0];

            }

        }

        public BitArray Encode(string source)
        {
            List<bool> totalEncoded = new List<bool>();

            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                totalEncoded.AddRange(encodedSymbol); // Add symbol encoded to total encoded code
            }

            BitArray bits = new BitArray(totalEncoded.ToArray());

            return bits;
        }

        public string Decode(BitArray bits)
        {
            HuffmanTreeNode current = this.Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded += current.Symbol;
                    current = this.Root;
                }
            }

            return decoded;
        }

        public bool IsLeaf(HuffmanTreeNode node)
        {
            return (node.Left == null && node.Right == null);
        }
    }
}

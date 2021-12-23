using System;
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
    internal class Node
    {
        // Data members
        private string tag_name;
        private string? tag_data;
        private Node? parent;
        private List<Node> children;
        private int depth;

        // Constructor
        public Node(string tag_name, string? tag_data)
        {
            this.tag_name = tag_name;
            this.tag_data = tag_data;
            this.parent = null;
            this.children = new List<Node>();
            this.depth = 0;
        }

        // Setters
        public void setTagName(string tag_name)
        {
            this.tag_name = tag_name;
        }
        public void setTagData(string? tag_data)
        {
            this.tag_data = tag_data;
        }
        public void setParent(Node? parent)
        {
            this.parent = parent;
        }
        public void setChildren(List<Node> children)
        {
            this.children = children;
        }
        public void setDepth(int depth)
        {
            this.depth = depth;
        }

        // Getters
        public string getTagName()
        {
            return tag_name;
        }
        public string? getTagData()
        {
            return tag_data;
        }
        public Node? getParent()
        {
            return parent;
        }
        public List<Node> getChildren()
        {
            return children;
        }
        public int getDepth()
        {
            return depth;
        }

        // Attributes
        public void addChild(Node child)
        {
            // assign the parent of the child
            child.parent = this;
            // calculate the depth of the child to be more than parent depth by 1
            child.depth = this.depth + 1;
            // add the child in the children List<>
            this.children.Add(child);
        }
        public bool isLeaf()
        {
            return this.children.Count == 0;
        }
    }
    internal class TreeParsing
    {
        /* function input:
         * function output: 
         * function description:
         * 
         * 
         * 
         */
        public static Node ParseToTree(string XML)
        {
            string corrected_and_formatted = '\n' + Formatting.format(consistency.correct(XML));
            string[] str = corrected_and_formatted.Split("\n");
            int i;
            // skip the first line if it contains ! or ?
            if (corrected_and_formatted[1] == '!' && corrected_and_formatted[1] == '?')
                i = 1;
            else
                i = 0;
            // extracting the root tag from first line
            string root_tag_line = str[i], root_tag_name = "";
            for (int k = 0; k < root_tag_line.Length; k++)
            {
                if (root_tag_line[k] == '<')
                {
                    k++;
                    for (; k < root_tag_line.Length; k++)
                    {
                        if (root_tag_line[k] != '>')
                            root_tag_name += root_tag_line[k];
                        else
                            break;
                    }
                }
            }
            Node root = new Node(root_tag_name, null);
            Node current = root;
            string tag_name = "", tag_data = "";
            i++;
            // extracting the chidren from the rest of the file
            string line;
            for (; i < str.Length; i++)
            {
                line = str[i];
                for (int j = 0; j < line.Length; j++)
                {
                    if (line[j] == '\t' || line[j] == '\n' || line[j] == ' ')
                        continue;
                    else if (line[j] == '<')
                    {
                        // closing tag
                        if (line[j + 1] == '/')
                        {
                            if (current.getParent() == null)
                            {
                                // when reaching the root which doesn't have a parent
                                break;
                            }
                            else
                            {
                                // go back in the tree
                                current = current.getParent();
                            }
                        }
                        // opening tag
                        else
                        {
                            j++;
                            for (; j < line.Length; j++)
                            {
                                if (line[j] != '>')
                                    tag_name += line[j];
                                else
                                    break;
                            }
                            Node new_child = new Node(tag_name, null);
                            new_child.setParent(current);
                            current.addChild(new_child);
                            current = new_child;
                            tag_name = "";
                        }
                    }
                    // data
                    else
                    {
                        for (; j < line.Length; j++)
                        {
                            if (line[j] == '\t' || line[j] == '\r' || line[j] == '\n')
                                continue;
                            tag_data += line[j];
                            // problem: lw el data kaza line
                        }
                        current.setTagData(tag_data);
                        tag_data = "";
                    }
                }
            }
            
            return root;
        }
    }
}

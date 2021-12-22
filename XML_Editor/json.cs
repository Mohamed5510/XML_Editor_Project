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
    internal class json
    {
        public static string XMLToJson(int level = 0, Node parent = null)
        {
            string space = null;
            string output = "";
            if (parent == null)
            {
                parent = null;
                level = 0;
            }
            for (int i = 0; i < parent.getChildren().Count; ++i)
            {
                Node node = parent.getChildren()[i]; // creating poienters to children of the parent node
                string Spacing = null;
                for (int j = 0; j < level; ++j) // lvl1 4 spacing , lvl2 8 spacing ,...
                {
                    Spacing += "\t";
                }
                output += Spacing + "\"" + node.getTagName() + "\":"; //"class":



                if (!(node.isLeaf()))
                {
                    output = output + XMLToJson(level + 1, node);
                    // to know if this node is the last node in its level
                    // from 119 to 127 each child

                    if (i == (parent.getChildren().Count - 1))
                    {
                        output += "\t"; // no action
                    }
                    else
                    {
                        output += ",\n";
                    }
                    //}
                }
                else
                { // if it is leaf node
                    if (i == parent.getChildren().Count - 1)
                    { // if last node in its level
                        while (node.getTagData()[node.getTagData().Length - 1] == '\n' || node.getTagData()[node.getTagData().Length - 1] == '\t' || node.getTagData()[node.getTagData().Length - 1] == ' ')
                        {
                            string s = node.getTagData().Remove(node.getTagData().Length - 1);
                            node.setTagData(s);
                        }
                        output += "\"" + node.getTagData() + "\"";
                    }
                    else
                    {
                        while (node.getTagData()[node.getTagData().Length - 1] == '\n' || node.getTagData()[node.getTagData().Length - 1] == '\t' || node.getTagData()[node.getTagData().Length - 1] == ' ')
                        {
                            string s = node.getTagData().Remove(node.getTagData().Length - 1);
                            node.setTagData(s);
                        }
                        output += "\"" + node.getTagData() + "\""; //"1"
                        output += ",\n";
                    }
                }
                space = Spacing;
            }
            string x = "\t";
            return "{\n" + output + "\n" + kk + "}";
        }

    }
}

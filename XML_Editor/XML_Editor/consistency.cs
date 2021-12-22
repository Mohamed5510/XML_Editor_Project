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
    internal class consistency
    {
        public static bool checkConsistency(string stringFile)
        {
            //split the XML file to array of strings to get line by line
            string[] xml = stringFile.Split("\n");
            Stack<String> tag = new Stack<string>();
            for (int i = 0; i < xml.Length; i++)
            {
                String line = xml[i];
                for (int k = 0; k < line.Length; k++)
                {
                    if (line[k] == '<')  //starting a tag
                    {
                        if (line[k + 1] == '/') // if true that mean the tag is closing tag
                        {
                            if (!(tag.Count == 0))
                            {
                                string ppp = "";    //ppp is temp string to check with the top of the stack
                                int t = k + 1;
                                while (line[t++] != '>')
                                {
                                    ppp += line[t];
                                }

                                if (ppp != (tag.Peek()))
                                {
                                    // unmatched closing and opening tags
                                    return false;
                                }
                                else
                                {
                                    tag.Pop();
                                }
                            }
                            else
                            {
                                // closing tag without opening tag
                                return false;
                            }
                        }
                        else if (line[k + 1] != '!' && line[k + 1] != '?')
                        {
                            String temp = "";   // temp is used to get the name of the openning tag to push it in the stack
                            for (int j = k + 1; j < line.Length; j++)
                            {
                                if (line[j] != '>')
                                {
                                    temp += line[j];
                                }
                                else
                                {
                                    temp += '>';
                                    break;
                                }
                            }
                            tag.Push(temp);
                        }
                    }
                }
            }
            return ((tag.Count == 0));
        }



        public static string correct(string stringFile)
        {
            if (checkConsistency(stringFile))   // check if the file is correct return the file
                return stringFile;



            string corrected = "";      // new string to store the corrected string
            string[] xml = stringFile.Split("\n");  // split the XML file to array of strings to get line by line
            Stack<String> tag = new Stack<string>();
            for (int i = 0; i < xml.Length; i++)
            {
                String line = xml[i];
                for (int k = 0; k < line.Length; k++)
                {
                    corrected += line[k];
                    if (line[k] == '<')
                    {
                        if (line[k + 1] == '/')
                        {
                            if (!(tag.Count == 0))
                            {
                                string ppp = line.Substring(k);
                                if (ppp != ("</" + tag.Peek()))
                                {
                                    // unmatched closing and opening tags
                                    // close the opened tag with the correct close
                                    corrected = corrected + "/" + tag.Peek();
                                    tag.Pop();
                                    break;
                                }
                                else
                                {
                                    tag.Pop();
                                }
                            }
                            else
                            {
                                // closing tag without opening tag
                                // remove the closing tag
                                if (corrected[corrected.Length - 1] == '<')
                                    corrected = corrected.Remove(corrected.Length - 1);
                                break;
                            }
                        }
                        else if (line[k + 1] != '!' && line[k + 1] != '?') // first line of XML may have the name by of the file in <?> form
                        {
                            int e = corrected.Length - 1;
                            if (e > 2)
                            {
                                while (e-- > 0)  // if we open a tag and forget to close it we need to know if the open tag have a data or followed by another openning tag 
                                                 // to close it in the right place
                                {
                                    if (corrected[e] == '\n' || corrected[e] == ' ' || corrected[e] == '\r')
                                        continue;
                                    else if (corrected[e] == '>')
                                        break;
                                    else if (!(tag.Count == 0))
                                    {
                                        corrected = corrected + "/" + tag.Peek() + "\n";
                                        corrected += '<';
                                        tag.Pop();
                                        break;
                                    }
                                    else
                                        break;
                                }
                            }
                            String temp = ""; // pushing the tag name in the stack
                            for (int j = k + 1; j < line.Length; j++)
                            {
                                if (line[j] != '>')
                                {
                                    temp += line[j];
                                }
                                else
                                {
                                    temp += '>';
                                    break;
                                }
                            }
                            tag.Push(temp);
                        }
                    }
                }
                corrected += "\n";
            }
            while (!(tag.Count == 0))
            {
                corrected = corrected + "</" + tag.Peek() + "\n";
                tag.Pop();
            }
            return (corrected);
        }
    }
}
